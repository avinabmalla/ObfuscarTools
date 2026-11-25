using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace ObfuscarTools
{
	/// <summary>
	/// Reads and edits the Obfuscar settings stored in a project file: the ObfuscarEnabledFor property
	/// and the import of Obfuscar.targets. Also migrates the Exec commands written by version 1.x.
	/// Has no Visual Studio dependencies.
	/// </summary>
	internal class ObfuscarProjectFile
	{
		public const string TargetsPath = @"_Obfuscar\Obfuscar.targets";
		private const string EnabledProperty = "ObfuscarEnabledFor";

		// Version 1.x started each of its commands with "echo <Configuration>@@<Platform>".
		private static readonly Regex LegacyCommand = new Regex(@"^echo (?<config>[^@\r\n]+)@@(?<platform>[^\r\n]+?)\s*[\r\n]");

		private readonly string path;

		public ObfuscarProjectFile(string path)
		{
			this.path = path;
		}

		/// <summary>Configuration and platform in the form MSBuild uses, e.g. "Release|AnyCPU".</summary>
		public static string Key(string configuration, string platform)
		{
			if (platform == "Any CPU") platform = "AnyCPU";
			return configuration + "|" + platform;
		}

		public HashSet<string> GetEnabled()
		{
			var root = Load().Root;
			var enabled = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (var property in EnabledProperties(root))
			{
				foreach (var key in property.Value.Split(';'))
				{
					if (string.IsNullOrWhiteSpace(key) == false) enabled.Add(key.Trim());
				}
			}

			foreach (var exec in LegacyCommands(root))
			{
				var match = LegacyCommand.Match((string)exec.Attribute("Command"));
				enabled.Add(Key(match.Groups["config"].Value, match.Groups["platform"].Value));
			}

			return enabled;
		}

		/// <summary>Replaces the list of obfuscated configurations. An empty list removes obfuscation.</summary>
		public void SetEnabled(IEnumerable<string> keys)
		{
			var list = keys.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
			var doc = Load();
			var root = doc.Root;
			var ns = root.Name.Namespace;
			var before = doc.ToString(SaveOptions.DisableFormatting);

			foreach (var exec in LegacyCommands(root).ToList())
			{
				var target = exec.Parent;
				RemoveWithIndent(exec);
				if (target.HasElements == false) RemoveWithIndent(target);
			}

			foreach (var property in EnabledProperties(root).ToList())
			{
				var group = property.Parent;
				RemoveWithIndent(property);
				if (group.HasElements == false && group.HasAttributes == false) RemoveWithIndent(group);
			}

			foreach (var import in root.Elements(ns + "Import").Where(IsObfuscarImport).ToList())
			{
				RemoveWithIndent(import);
			}

			if (list.Count > 0)
			{
				// Both go at the end of the project: the targets file must be imported after the .NET targets,
				// and it reads the property while it is being imported.
				Append(root,
					new XElement(ns + "PropertyGroup",
						new XText("\n    "),
						new XElement(ns + EnabledProperty, ";" + string.Join(";", list) + ";"),
						new XText("\n  ")));

				Append(root,
					new XElement(ns + "Import",
						new XAttribute("Project", TargetsPath),
						new XAttribute("Condition", $"Exists('{TargetsPath}')")));
			}

			if (doc.ToString(SaveOptions.DisableFormatting) != before) Save(doc);
		}

		public bool UsesWpf()
		{
			var root = Load().Root;
			return root.Descendants().Any(e =>
				(e.Name.LocalName == "UseWPF" && e.Value.Trim().Equals("true", StringComparison.OrdinalIgnoreCase)) ||
				(e.Name.LocalName == "Reference" && ((string)e.Attribute("Include") ?? "").StartsWith("PresentationFramework", StringComparison.OrdinalIgnoreCase)));
		}

		private static IEnumerable<XElement> EnabledProperties(XElement root)
		{
			var ns = root.Name.Namespace;
			return root.Elements(ns + "PropertyGroup").Elements(ns + EnabledProperty);
		}

		private static IEnumerable<XElement> LegacyCommands(XElement root)
		{
			var ns = root.Name.Namespace;
			return root.Elements(ns + "Target")
				.Where(t => (string)t.Attribute("Name") == "AfterCompile" || (string)t.Attribute("Name") == "PostBuild")
				.Elements(ns + "Exec")
				.Where(e =>
				{
					var command = (string)e.Attribute("Command") ?? "";
					return command.Contains("_Obfuscar") && LegacyCommand.IsMatch(command);
				});
		}

		private static bool IsObfuscarImport(XElement import)
		{
			var project = ((string)import.Attribute("Project") ?? "").Replace('/', '\\');
			return project.Equals(TargetsPath, StringComparison.OrdinalIgnoreCase);
		}

		private static void RemoveWithIndent(XElement element)
		{
			if (element.PreviousNode is XText indent && string.IsNullOrWhiteSpace(indent.Value)) indent.Remove();
			element.Remove();
		}

		private static void Append(XElement root, XElement element)
		{
			if (root.LastNode is XText tail && string.IsNullOrWhiteSpace(tail.Value))
			{
				tail.AddBeforeSelf(new XText("\n  "), element);
			}
			else
			{
				root.Add(new XText("\n  "), element, new XText("\n"));
			}
		}

		private XDocument Load()
		{
			return XDocument.Load(path, LoadOptions.PreserveWhitespace);
		}

		/// <summary>Saves while keeping the file's byte order mark, XML declaration and line endings.</summary>
		private void Save(XDocument doc)
		{
			var bytes = File.ReadAllBytes(path);
			bool hasBom = bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
			bool usesCrLf = Encoding.UTF8.GetString(bytes).Contains("\r\n");

			var settings = new XmlWriterSettings
			{
				Encoding = new UTF8Encoding(hasBom),
				OmitXmlDeclaration = doc.Declaration == null,
				NewLineChars = usesCrLf ? "\r\n" : "\n",
			};

			using (var writer = XmlWriter.Create(path, settings))
			{
				doc.Save(writer);
			}
		}
	}
}
