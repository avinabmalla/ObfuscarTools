using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

		public string ProjectDir => new FileInfo(FileName).Directory.FullName;
		public string ObfuscarDir => ProjectDir + "\\_Obfuscar";
		public string ObfuscarBaseConfigFile => Path.Combine(ObfuscarDir, "obfuscar.xml");

		public List<Configuration> GetAllConfigurations()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			var rowNames = Project.Project.ConfigurationManager.ConfigurationRowNames as object[];

			var configurations = new List<Configuration>();
			foreach (var row in rowNames)
			{
				var configRow = Project.Project.ConfigurationManager.ConfigurationRow(row.ToString());
				var en = configRow.GetEnumerator();

				while (en.MoveNext())
				{
					Configuration item = en.Current as Configuration;
					configurations.Add(item);
				}
			}
			return configurations;
		}

		public bool IsObfuscated(string buildConfig, string platform)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return new ObfuscarProjectFile(FileName).GetEnabled().Contains(ObfuscarProjectFile.Key(buildConfig, platform));
		}

		public Configuration ActiveBuildConfig
		{
			get
			{
				try
				{
					ThreadHelper.ThrowIfNotOnUIThread();
					return Project.Project.ConfigurationManager.ActiveConfiguration;
				}
				catch
				{
					return null;
				}
			}
		}

		public DotNetProject(ObfuscarToolsPackage pkg, VSProject proj)
		{
			Project = proj;
			package = pkg;
		}

		/// <summary>Copies Obfuscar and Obfuscar.targets into the project's _Obfuscar folder.</summary>
		public void CopyObfuscar()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			string srcDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "files");

			Directory.CreateDirectory(ObfuscarDir);

			foreach (var file in Directory.GetFiles(srcDir))
			{
				File.Copy(file, Path.Combine(ObfuscarDir, Path.GetFileName(file)), true);
			}
		}

		/// <summary>The project's settings, or the defaults for its type if it has none yet.</summary>
		public ObfuscarBaseConfig LoadBaseConfig()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (File.Exists(ObfuscarBaseConfigFile)) return ObfuscarBaseConfig.Read(ObfuscarBaseConfigFile);
			return ObfuscarBaseConfig.CreateDefault(new ObfuscarProjectFile(FileName).UsesWpf());
		}

		public void AddObfuscarBaseConfig()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			CopyObfuscar();
			LoadBaseConfig().Write(ObfuscarBaseConfigFile);
		}

		public void AddObfuscarCommand(string buildConfig, string platform)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			var enabled = new ObfuscarProjectFile(FileName).GetEnabled();
			enabled.Add(ObfuscarProjectFile.Key(buildConfig, platform));
			SetObfuscatedConfigurations(enabled);
		}

		public void RemoveObfuscarCommand(string buildConfig, string platform)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			var enabled = new ObfuscarProjectFile(FileName).GetEnabled();
			enabled.Remove(ObfuscarProjectFile.Key(buildConfig, platform));

			// Removing a version 1.x command can leave other configurations to migrate.
			if (enabled.Count > 0) AddObfuscarBaseConfig();
			SetObfuscatedConfigurations(enabled);
		}

		/// <param name="keys">Keys made with <see cref="ObfuscarProjectFile.Key"/>.</param>
		public void SetObfuscatedConfigurations(IEnumerable<string> keys)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			// Save pending changes first, so that editing the file does not discard them.
			if (Project.Project.Saved == false) Project.Project.Save();

			new ObfuscarProjectFile(FileName).SetEnabled(keys);
		}
	}
}
