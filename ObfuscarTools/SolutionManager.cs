using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscarTools
{
	internal static class SolutionManager
	{
		internal static Solution GetActiveSolution()
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			DTE dte = Package.GetGlobalService(typeof(SDTE)) as DTE;
			return dte.Solution;
		}


		internal static EnvDTE.Project GetActiveProject()
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			DTE dte = Package.GetGlobalService(typeof(SDTE)) as DTE;

			Project project = null;
			try
			{
				project = dte.ActiveDocument.ProjectItem.ContainingProject;
			}
			catch { }
			if (project != null) return project;

			Array activeSolutionProjects = dte.ActiveSolutionProjects as Array;
			if (activeSolutionProjects != null && activeSolutionProjects.Length > 0)
			{
				project = activeSolutionProjects.GetValue(0) as Project;
			}

			return project;
		}

	}
}
