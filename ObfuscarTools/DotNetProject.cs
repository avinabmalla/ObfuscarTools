using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VSLangProj;

namespace ObfuscarTools
{
	class DotNetProject
	{
		public VSProject Project { get; private set; }
		ObfuscarToolsPackage package;
		public string FileName
		{
			get
			{
				ThreadHelper.ThrowIfNotOnUIThread();
				return Project.Project.FullName;
			}
		}

		public string Name
		{
			get
			{
				ThreadHelper.ThrowIfNotOnUIThread();
				return Project.Project.Name;
			}
		}

		public string projectDir => new FileInfo(FileName).Directory.FullName;
		public string obfuscarDir => projectDir + "\\_Obfuscar";


		public bool IsNetFrameworkProject()
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			bool isFramework = false;
			foreach (Property prop in Project.Project.Properties)
			{
				if (prop.Name == "TargetFrameworkMoniker")
				{
					var frameworkVersion = prop.Value.ToString();
					isFramework = frameworkVersion.StartsWith(".NETFramework");
				}
			}
			return isFramework;
		}


		public string GetOutputPath()
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			var objPath = "";
			var binPath = "";

			foreach (Property prop in Project.Project.ConfigurationManager.ActiveConfiguration.Properties)
			{
				if (prop.Name == "IntermediatePath") objPath = prop.Value.ToString();
				if (prop.Name == "OutputPath") binPath = prop.Value.ToString();
			}

			if (IsNetFrameworkProject()) return objPath;
			return binPath;
		}


		public bool IsObfuscated(string configuration)
		{
			var mgr = new AfterCompileManager(Project, IsNetFrameworkProject());
			return mgr.HasAfterCompileTarget(configuration);
		}

		public string ConfigurationName
		{
			get
			{
				try
				{
					ThreadHelper.ThrowIfNotOnUIThread();
					return Project.Project.ConfigurationManager.ActiveConfiguration.ConfigurationName;
				}
				catch
				{
					return "";
				}
			}
		}

		public string PlatformName
		{
			get
			{
				ThreadHelper.ThrowIfNotOnUIThread();
				return Project.Project.ConfigurationManager.ActiveConfiguration.PlatformName;
			}
		}


		public DotNetProject(ObfuscarToolsPackage pkg, VSProject proj)
		{
			Project = proj;
			package = pkg;
		}

		public void CopyObfuscar()
		{
			var fileNames = new string[] { "Obfuscar.Console.exe" };
			string srcDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\files\\";

			Directory.CreateDirectory(obfuscarDir);

			foreach (var f in fileNames)
			{
				File.Copy(srcDir + f, obfuscarDir + "\\" + f, true);
			}
		}


		public List<string> GetProbePaths()
		{
			var ret = new List<string>();
			foreach (Reference r in Project.References)
			{
				var fileName = r.Path;
				if (fileName == null || fileName == "") continue;
				var folderName = new FileInfo(fileName).Directory.FullName;
				if (ret.Contains(folderName) == false) ret.Add(folderName);
			}
			return ret;
		}



		public void WriteConfig(string path)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			List<string> probePaths = GetProbePaths();
			var solutionDir = new FileInfo(SolutionManager.GetActiveSolution().FullName).Directory.FullName;

			ObfuscarConfig config = new ObfuscarConfig();
			if (File.Exists(path))
			{
				config = ObfuscarConfig.Read(path);
			}
			string moduleName = Project.Project.Properties.Item("OutputFileName").Value.ToString();

			config.InPath = GetOutputPath();
			config.OutPath = GetOutputPath() + "\\Out";
			config.Module = "$(InPath)\\" + moduleName;

			config.AssemblySearchPaths.Clear();
			foreach (var pp in probePaths)
			{
				config.AssemblySearchPaths.Add(pp);
			}

			config.Write(path);
		}

		string GetRelativePath(string filespec, string folder)
		{
			Uri pathUri = new Uri(filespec);
			// Folders must end in a slash
			if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
			{
				folder += Path.DirectorySeparatorChar;
			}
			Uri folderUri = new Uri(folder);
			return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
		}



	}
}
