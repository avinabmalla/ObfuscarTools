using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ObfuscarTools
{
	/// <summary>
	/// Obfuscar config common for all build configurations (_Obfuscar\obfuscar.xml).
	/// Obfuscar.targets includes it in a config generated for each build.
	/// </summary>
	internal class ObfuscarBaseConfig
	{
		// Both variables are defined by the generated config.
		private const string DefaultModule = "$(InPath)\\$(TargetFileName)";
		private const string AnonymousTypes = "*AnonymousType*";

		public List<string> NamespacesToSkip { get; set; } = new List<string>();

		public string KeyFile { get; set; }
		public bool RegenerateDebugInfo { get; set; } = false;
		public bool MarkedOnly { get; set; } = false;
		public bool RenameProperties { get; set; } = true;
		public bool RenameEvents { get; set; } = true;
		public bool RenameFields { get; set; } = true;
		public bool KeepPublicApi { get; set; } = true;
		public bool HidePrivateApi { get; set; } = true;
		public bool ReuseNames { get; set; } = false;
		public bool HideStrings { get; set; } = true;
		public bool UseUnicodeNames { get; set; } = false;
		public bool UseKoreanNames { get; set; } = true;
		public bool OptimizeMethods { get; set; } = true;
		public bool SuppressIldasm { get; set; } = true;
		public bool AnalyzeXaml { get; set; } = true;

		/// <summary>Settings for a project that has no config file yet.</summary>
		public static ObfuscarBaseConfig CreateDefault(bool isWpf)
		{
			var config = new ObfuscarBaseConfig();
			if (isWpf)
			{
				// Renamed properties break data bindings on non-public types.
				config.RenameProperties = false;
				config.SuppressIldasm = false;
			}
			return config;
		}

		/// <summary>Writes the settings into the file, keeping any other elements it already contains.</summary>
		public void Write(string path)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(path));
			var doc = File.Exists(path) ? XDocument.Load(path) : new XDocument(new XElement("Include"));
			var root = doc.Root;

			SetVar(root, "RegenerateDebugInfo", RegenerateDebugInfo);
			SetVar(root, "MarkedOnly", MarkedOnly);
			SetVar(root, "RenameProperties", RenameProperties);
			SetVar(root, "RenameEvents", RenameEvents);
			SetVar(root, "RenameFields", RenameFields);
			SetVar(root, "KeepPublicApi", KeepPublicApi);
			SetVar(root, "HidePrivateApi", HidePrivateApi);
			SetVar(root, "ReuseNames", ReuseNames);
			SetVar(root, "HideStrings", HideStrings);
			SetVar(root, "OptimizeMethods", OptimizeMethods);
			SetVar(root, "SuppressIldasm", SuppressIldasm);
			SetVar(root, "AnalyzeXaml", AnalyzeXaml);
			SetVar(root, "UseUnicodeNames", UseUnicodeNames);
			SetVar(root, "UseKoreanNames", UseKoreanNames);
			if (string.IsNullOrWhiteSpace(KeyFile) == false) SetVar(root, "KeyFile", KeyFile);

			var module = root.Element("Module");
			if (module == null)
			{
				module = new XElement("Module", new XAttribute("file", DefaultModule));
				root.Add(module);
			}
			else if (root.Elements("Module").Count() == 1 && (string)module.Attribute("file") != DefaultModule)
			{
				var file = (string)module.Attribute("file") ?? "";
				if (file.StartsWith("$(InPath)\\", StringComparison.Ordinal))
				{
					// Written by version 1.x, which also listed the search paths. The build now supplies them.
					module.SetAttributeValue("file", DefaultModule);
					root.Elements("AssemblySearchPath").Remove();
				}
			}

			module.Elements("SkipNamespace").Remove();
			module.Add(NamespacesToSkip
				.Where(ns => string.IsNullOrWhiteSpace(ns) == false)
				.Select(ns => new XElement("SkipNamespace", new XAttribute("name", ns.Trim()))));

			if (module.Elements("SkipType").Any(e => (string)e.Attribute("name") == AnonymousTypes) == false)
			{
				module.Add(new XElement("SkipType",
					new XAttribute("name", AnonymousTypes),
					new XAttribute("skipProperties", "true"),
					new XAttribute("skipMethods", "true"),
					new XAttribute("skipFields", "true"),
					new XAttribute("skipEvents", "true"),
					new XAttribute("skipStringHiding", "true")));
			}

			doc.Save(path);
		}

		private static void SetVar(XElement root, string name, bool value)
		{
			SetVar(root, name, value ? "true" : "false");
		}

		private static void SetVar(XElement root, string name, string value)
		{
			var element = root.Elements("Var").FirstOrDefault(e => (string)e.Attribute("name") == name);
			if (element != null)
			{
				element.SetAttributeValue("value", value);
				return;
			}

			element = new XElement("Var", new XAttribute("name", name), new XAttribute("value", value));
			var lastVar = root.Elements("Var").LastOrDefault();
			if (lastVar != null) lastVar.AddAfterSelf(element);
			else root.AddFirst(element);
		}

		public static ObfuscarBaseConfig Read(string configFile)
		{
			var ret = new ObfuscarBaseConfig();
			if (File.Exists(configFile) == false) return ret;
			var root = XDocument.Load(configFile).Root;

			foreach (var element in root.Elements("Var"))
			{
				var varName = (string)element.Attribute("name");
				var varValue = (string)element.Attribute("value") ?? "";
				var boolValue = varValue.ToLower() == "true";

				if (varName == "RegenerateDebugInfo") ret.RegenerateDebugInfo = boolValue;
				if (varName == "MarkedOnly") ret.MarkedOnly = boolValue;
				if (varName == "RenameProperties") ret.RenameProperties = boolValue;
				if (varName == "RenameEvents") ret.RenameEvents = boolValue;
				if (varName == "RenameFields") ret.RenameFields = boolValue;
				if (varName == "KeepPublicApi") ret.KeepPublicApi = boolValue;
				if (varName == "HidePrivateApi") ret.HidePrivateApi = boolValue;
				if (varName == "ReuseNames") ret.ReuseNames = boolValue;
				if (varName == "HideStrings") ret.HideStrings = boolValue;
				if (varName == "OptimizeMethods") ret.OptimizeMethods = boolValue;
				if (varName == "SuppressIldasm") ret.SuppressIldasm = boolValue;
				if (varName == "AnalyzeXaml") ret.AnalyzeXaml = boolValue;
				if (varName == "UseUnicodeNames") ret.UseUnicodeNames = boolValue;
				if (varName == "UseKoreanNames") ret.UseKoreanNames = boolValue;
				if (varName == "KeyFile") ret.KeyFile = varValue;
			}

			ret.NamespacesToSkip = root.Descendants("SkipNamespace")
				.Select(e => (string)e.Attribute("name"))
				.Where(name => string.IsNullOrWhiteSpace(name) == false)
				.ToList();

			return ret;
		}
	}
}
