using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ObfuscarTools
{
	public class ConfigWriter
	{
		public static void WriteVar(XmlWriter writer, string name, bool value)
		{
			writer.WriteStartElement("Var");
			writer.WriteAttributeString("name", name);
			writer.WriteAttributeString("value", value ? "true" : "false");
			writer.WriteEndElement();
		}

		public static void WriteVar(XmlWriter writer, string name, string value)
		{
			writer.WriteStartElement("Var");
			writer.WriteAttributeString("name", name);
			writer.WriteAttributeString("value", value);
			writer.WriteEndElement();
		}

	}

	/// <summary>
	/// Obfuscar config common for all build configurations
	/// </summary>
	internal class ObfuscarBaseConfig
	{
		public string Module { get; set; }
		public List<string> AssemblySearchPaths { get; set; } = new List<string>();
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


		public void Write(string path)
		{
			if (File.Exists(path)) File.Delete(path);
			XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
			Directory.CreateDirectory(Path.GetDirectoryName(path));

			var writer = XmlWriter.Create(path, settings);
			writer.WriteStartElement("Include");

			ConfigWriter.WriteVar(writer, "RegenerateDebugInfo", RegenerateDebugInfo);
			ConfigWriter.WriteVar(writer, "MarkedOnly", MarkedOnly);
			ConfigWriter.WriteVar(writer, "RenameProperties", RenameProperties);
			ConfigWriter.WriteVar(writer, "RenameEvents", RenameEvents);
			ConfigWriter.WriteVar(writer, "RenameFields", RenameFields);
			ConfigWriter.WriteVar(writer, "KeepPublicApi", KeepPublicApi);
			ConfigWriter.WriteVar(writer, "HidePrivateApi", HidePrivateApi);
			ConfigWriter.WriteVar(writer, "ReuseNames", ReuseNames);
			ConfigWriter.WriteVar(writer, "HideStrings", HideStrings);
			ConfigWriter.WriteVar(writer, "OptimizeMethods", OptimizeMethods);
			ConfigWriter.WriteVar(writer, "SuppressIldasm", SuppressIldasm);
			ConfigWriter.WriteVar(writer, "AnalyzeXaml", AnalyzeXaml);
			ConfigWriter.WriteVar(writer, "UseUnicodeNames", UseUnicodeNames);
			ConfigWriter.WriteVar(writer, "UseKoreanNames", UseKoreanNames);

			if (string.IsNullOrWhiteSpace(KeyFile) == false)
			{
				ConfigWriter.WriteVar(writer, "KeyFile", KeyFile);
			}


			writer.WriteStartElement("Module");
			writer.WriteAttributeString("file", Module);

			foreach (var ns in NamespacesToSkip)
			{
				if (string.IsNullOrWhiteSpace(ns)) continue;
				writer.WriteStartElement("SkipNamespace");
				writer.WriteAttributeString("name", ns);
				writer.WriteEndElement();
			}

			writer.WriteEndElement();


			AssemblySearchPaths.Sort();
			foreach (var assembly in AssemblySearchPaths)
			{
				writer.WriteStartElement("AssemblySearchPath");
				writer.WriteAttributeString("path", assembly + "\\");
				writer.WriteEndElement();
			}


			writer.WriteEndElement();
			writer.Close();

		}


		public static ObfuscarBaseConfig Read(string configFile)
		{
			var ret = new ObfuscarBaseConfig();
			if (File.Exists(configFile) == false) return ret;
			var reader = XmlReader.Create(configFile);
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.Element) continue;

				if (reader.Name == "Var")
				{
					var varName = reader.GetAttribute("name");
					var varValue = reader.GetAttribute("value");
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
					if (varName == "AnalyzeXaml") ret.AnalyzeXaml = boolValue;
					if (varName == "UseUnicodeNames") ret.UseUnicodeNames = boolValue;
					if (varName == "UseKoreanNames") ret.UseKoreanNames = boolValue;
				}
				else if (reader.Name == "Module")
				{
					ret.Module = reader.GetAttribute("file");
				}
				else if (reader.Name == "AssemblySearchPath")
				{
					ret.AssemblySearchPaths.Add(reader.GetAttribute("path"));
				}
				else if (reader.Name == "SkipNamespace")
				{
					var namespaceName = reader.GetAttribute("name");
					if (string.IsNullOrWhiteSpace(namespaceName) == false)
					{
						ret.NamespacesToSkip.Add(namespaceName);
					}
				}
			}
			reader.Close();
			return ret;
		}
	}

	internal class ObfuscarConfig
	{
		public string InPath { get; set; }
		public string OutPath { get; set; }
		public string BaseConfigPath { get; set; }
		public static ObfuscarConfig Read(string configFilePath)
		{
			var ret = new ObfuscarConfig();
			if (File.Exists(configFilePath) == false) return ret;
			var reader = XmlReader.Create(configFilePath);
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.Element) continue;

				if (reader.Name == "Var")
				{
					var varName = reader.GetAttribute("name");
					var varValue = reader.GetAttribute("value");

					if (varName == "InPath") ret.InPath = varValue;
					if (varName == "OutPath") ret.OutPath = varValue;
				}
				else if (reader.Name == "Include")
				{
					ret.BaseConfigPath = reader.GetAttribute("path");
				}
			}


			reader.Close();
			return ret;
		}



		public void Write(string path)
		{
			if (File.Exists(path)) File.Delete(path);
			XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
			var writer = XmlWriter.Create(path, settings);
			writer.WriteStartElement("Obfuscator");

			ConfigWriter.WriteVar(writer, "InPath", InPath);
			ConfigWriter.WriteVar(writer, "OutPath", OutPath);

			writer.WriteStartElement("Include");
			writer.WriteAttributeString("path", BaseConfigPath);
			writer.WriteEndElement();


			writer.WriteEndElement();
			writer.Close();

		}

	}
}
