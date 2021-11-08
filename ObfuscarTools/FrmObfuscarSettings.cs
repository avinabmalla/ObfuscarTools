using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObfuscarTools
{
	public partial class FrmObfuscarSettings : Form
	{
		public string ProjectName { get; set; }
		public string ConfigurationName { get; set; }
		public string ConfigurationPath{ get; set; }

		public FrmObfuscarSettings()
		{
			InitializeComponent();
		}

		private void FrmObfuscarSettings_Load(object sender, EventArgs e)
		{
			var cfg = ObfuscarConfig.Read(ConfigurationPath);
			lblConfigName.Text = ConfigurationName;
			lblProjectName.Text = ProjectName;

			chkRegenerateDebugInfo.Checked = cfg.RegenerateDebugInfo;
			chkMarkedOnly.Checked = cfg.MarkedOnly;
			chkRenameProperties.Checked = cfg.RenameProperties;
			chkRenameEvents.Checked = cfg.RenameEvents;	
			chkRenameFields.Checked = cfg.RenameFields;
			chkKeepPublicApi.Checked = cfg.KeepPublicApi;	
			chkHidePrivateApi.Checked = cfg.HidePrivateApi;	
			chkReuseNames.Checked= cfg.ReuseNames;
			chkHideStrings.Checked = cfg.HideStrings;
			chkOptimizeMethods.Checked = cfg.OptimizeMethods;	
			chkSuppressIldasm.Checked = cfg.SuppressIldasm;
			chkAnalyzeXaml.Checked = cfg.AnalyzeXaml;
			chkUseUnicodeNames.Checked = cfg.UseUnicodeNames;
			chkUseKoreanNames.Checked = cfg.UseKoreanNames;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			var cfg = ObfuscarConfig.Read(ConfigurationPath);

			cfg.RegenerateDebugInfo = chkRegenerateDebugInfo.Checked;
			cfg.MarkedOnly = chkMarkedOnly.Checked;
			cfg.RenameProperties = chkRenameProperties.Checked;
			cfg.RenameEvents = chkRenameEvents.Checked;
			cfg.RenameFields = chkRenameFields.Checked;
			cfg.KeepPublicApi = chkKeepPublicApi.Checked;
			cfg.HidePrivateApi = chkHidePrivateApi.Checked;
			cfg.ReuseNames = chkReuseNames.Checked;
			cfg.HideStrings = chkHideStrings.Checked;
			cfg.OptimizeMethods = chkOptimizeMethods.Checked;
			cfg.SuppressIldasm = chkSuppressIldasm.Checked;
			cfg.AnalyzeXaml = chkAnalyzeXaml.Checked;
			cfg.UseUnicodeNames = chkUseUnicodeNames.Checked;
			cfg.UseKoreanNames = chkUseKoreanNames.Checked;

			cfg.Write(ConfigurationPath);
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
