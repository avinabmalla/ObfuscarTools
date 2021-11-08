using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VSLangProj;

namespace ObfuscarTools
{
	public class AfterCompileManager
	{
		VSProject Project;
		DTE dte = Package.GetGlobalService(typeof(SDTE)) as DTE;

		public string TargetName { get; set; } = "AfterCompile";
		public string AfterTargets { get; set; } = "";

		public AfterCompileManager(VSProject proj, bool isFramework)
		{
			Project = proj;
			if (isFramework)
			{
				TargetName = "AfterCompile";
				AfterTargets = "";
			}
			else{
				TargetName = "PostBuild";
				AfterTargets = "PostBuildEvent";
			}
		}

		public bool HasAfterCompileTarget(string confName)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			var path = Project.Project.FullName;

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(path);
			XmlNode ProjectNode = (from XmlNode n in xmlDoc.ChildNodes where n.Name == "Project" select n).FirstOrDefault();
			XmlNode AfterCompileTarget = (from XmlNode n in ProjectNode.ChildNodes where n.Name == "Target" && n.Attributes["Name"]?.Value == TargetName select n).FirstOrDefault();
			if (AfterCompileTarget == null) return false;

			var execNode = GetNode(AfterCompileTarget, confName);
			if (execNode == null) return false;

			return true;
		}

		public void RemoveAfterCompileTarget(string confName)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			var path = Project.Project.FullName;

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(path);
			XmlNode ProjectNode = (from XmlNode n in xmlDoc.ChildNodes where n.Name == "Project" select n).FirstOrDefault();
			XmlNode AfterCompileTarget = (from XmlNode n in ProjectNode.ChildNodes where n.Name == "Target" && n.Attributes["Name"]?.Value == TargetName select n).FirstOrDefault();

			if (AfterCompileTarget == null) return;
			var execNode = GetNode(AfterCompileTarget, confName);

			if (execNode == null) return;
			AfterCompileTarget.RemoveChild(execNode);

			xmlDoc.Save(path);
			ReloadProject();
		}

		public void AddAfterCompileCommand(AfterCompileCommand cmd)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			var path = Project.Project.FullName;

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(path);
			XmlNode ProjectNode = (from XmlNode n in xmlDoc.ChildNodes where n.Name == "Project" select n).FirstOrDefault();
			XmlNode AfterCompileTarget = (from XmlNode n in ProjectNode.ChildNodes where n.Name == "Target" && n.Attributes["Name"]?.Value == TargetName select n).FirstOrDefault();

			if (AfterCompileTarget == null)
			{
				AfterCompileTarget = AddAfterCompileTargetNode(xmlDoc, ProjectNode);
			}

			var execNode = GetNode(AfterCompileTarget, cmd.Configuration);
			if (execNode == null)
			{
				execNode = xmlDoc.CreateElement("Exec", xmlDoc.DocumentElement.NamespaceURI);
				AfterCompileTarget.AppendChild(execNode);
			}


			XmlAttribute CommandAttribute = (from XmlAttribute a in execNode.Attributes where a.Name == "Command" select a).FirstOrDefault();
			if (CommandAttribute == null)
			{
				CommandAttribute = xmlDoc.CreateAttribute("Command");
				execNode.Attributes.Append(CommandAttribute);
			}
			CommandAttribute.Value = cmd.CommandString;

			xmlDoc.Save(path);
			ReloadProject();
		}


		private void ReloadProject()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			string solutionName = Path.GetFileNameWithoutExtension(dte.Solution.FullName);
			string projectName = Project.Project.Name;


			dte.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer).Activate();
			((DTE2)dte).ToolWindows.SolutionExplorer.GetItem(solutionName + @"\" + projectName).Select(vsUISelectionType.vsUISelectionTypeSelect);

			//dte.ExecuteCommand("Project.ReloadProject");
		}

		private XmlNode AddAfterCompileTargetNode(XmlDocument xmlDoc, XmlNode ProjectNode)
		{
			var AfterCompileTarget = xmlDoc.CreateElement("Target", xmlDoc.DocumentElement.NamespaceURI);
			ProjectNode.AppendChild(AfterCompileTarget);
			var NameAttr = xmlDoc.CreateAttribute("Name");
			NameAttr.Value = TargetName;
			AfterCompileTarget.Attributes.Append(NameAttr);

			if (string.IsNullOrWhiteSpace(AfterTargets) == false)
			{
				var AfterTargetsAttr = xmlDoc.CreateAttribute("AfterTargets");
				AfterTargetsAttr.Value = AfterTargets;
				AfterCompileTarget.Attributes.Append(AfterTargetsAttr);
			}



			return AfterCompileTarget;
		}

		private XmlNode GetNode(XmlNode AfterCompileTarget, string confName)
		{
			var execNodes = (from XmlNode n in AfterCompileTarget.ChildNodes where n.Name == "Exec" select n).ToList();
			foreach (var nd in execNodes)
			{
				var config = nd.Attributes["Command"];
				if (config == null) continue;
				if (config.Value.StartsWith("echo " + confName + "\r\n"))
				{
					return nd;
				}
			}
			return null;
		}
	}


	public class AfterCompileCommand
	{
		public string Configuration { get; set; }
		public string ObfuscarPath { get; set; } = @"$(ProjectDir)_Obfuscar\Obfuscar.Console.exe";
		public string ConfigXmlName { get; set; }
		public string IntermediatePath { get; set; }

		public string CommandString
		{
			get
			{
				var commands = new List<string>();
				commands.Add("echo " + Configuration);

				string cmd = $"if \"$(ConfigurationName)\" == \"{Configuration}\" (";
				cmd += $"\"{ObfuscarPath}\" \"$(ProjectDir)_Obfuscar\\{ConfigXmlName}\"\r\n";
				cmd += $"xcopy /e /v \"$(ProjectDir){IntermediatePath}\\Out\\\" \"$(ProjectDir){IntermediatePath}\" /Y";
				cmd += ")";

				commands.Add(cmd);



				return string.Join("\r\n", commands);
			}
		}
	}
}
