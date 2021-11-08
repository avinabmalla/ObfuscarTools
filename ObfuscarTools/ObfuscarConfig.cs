using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ObfuscarTools
{
	internal class ObfuscarConfig
	{
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

		public string InPath { get; set; }
		public string OutPath { get; set; }
		public List<string> AssemblySearchPaths { get; set; } = new List<string>();
		public string Module { get; set; }



		private static void WriteVar(XmlWriter writer, string name, bool value)
		{
			writer.WriteStartElement("Var");
			writer.WriteAttributeString("name", name);
			writer.WriteAttributeString("value", value ? "true" : "false");
			writer.WriteEndElement();
		}

		private static void WriteVar(XmlWriter writer, string name, string value)
		{
			writer.WriteStartElement("Var");
			writer.WriteAttributeString("name", name);
			writer.WriteAttributeString("value", value);
			writer.WriteEndElement();
		}


		public void Write(string path)
		{
			if (File.Exists(path)) File.Delete(path);
			XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
			var writer = XmlWriter.Create(path, settings);
			writer.WriteStartElement("Obfuscator");

			WriteVar(writer, "InPath", InPath);
			WriteVar(writer, "OutPath", OutPath);
			WriteVar(writer, "RegenerateDebugInfo", RegenerateDebugInfo);
			WriteVar(writer, "MarkedOnly", MarkedOnly);
			WriteVar(writer, "RenameProperties", RenameProperties);
			WriteVar(writer, "RenameEvents", RenameEvents);
			WriteVar(writer, "RenameFields", RenameFields);
			WriteVar(writer, "KeepPublicApi", KeepPublicApi);
			WriteVar(writer, "HidePrivateApi", HidePrivateApi);
			WriteVar(writer, "ReuseNames", ReuseNames);
			WriteVar(writer, "HideStrings", HideStrings);
			WriteVar(writer, "OptimizeMethods", OptimizeMethods);
			WriteVar(writer, "SuppressIldasm", SuppressIldasm);
			WriteVar(writer, "AnalyzeXaml", AnalyzeXaml);
			WriteVar(writer, "UseUnicodeNames", UseUnicodeNames);
			WriteVar(writer, "UseKoreanNames", UseKoreanNames);

			if (string.IsNullOrWhiteSpace(KeyFile) == false)
			{
				WriteVar(writer, "KeyFile", KeyFile);
			}


			writer.WriteStartElement("Module");
			writer.WriteAttributeString("file", Module);
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


		public static ObfuscarConfig Read(string configFile)
		{
			var ret = new ObfuscarConfig();
			var reader = XmlReader.Create(configFile);
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.Element) continue;

				if (reader.Name == "Var")
				{
					var varName = reader.GetAttribute("name");
					var varValue = reader.GetAttribute("value");
					var boolValue = varValue.ToLower() == "true";

					if (varName == "InPath") ret.InPath = varValue;
					if (varName == "OutPath") ret.OutPath = varValue;
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
			}
			reader.Close();
			return ret;
		}

	}
}
