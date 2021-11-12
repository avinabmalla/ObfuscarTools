namespace ObfuscarTools
{
	partial class FrmObfuscarSettings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.chkRegenerateDebugInfo = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.chkMarkedOnly = new System.Windows.Forms.CheckBox();
			this.chkRenameProperties = new System.Windows.Forms.CheckBox();
			this.chkRenameEvents = new System.Windows.Forms.CheckBox();
			this.chkRenameFields = new System.Windows.Forms.CheckBox();
			this.chkKeepPublicApi = new System.Windows.Forms.CheckBox();
			this.chkHidePrivateApi = new System.Windows.Forms.CheckBox();
			this.chkReuseNames = new System.Windows.Forms.CheckBox();
			this.chkHideStrings = new System.Windows.Forms.CheckBox();
			this.chkOptimizeMethods = new System.Windows.Forms.CheckBox();
			this.chkSuppressIldasm = new System.Windows.Forms.CheckBox();
			this.chkAnalyzeXaml = new System.Windows.Forms.CheckBox();
			this.chkUseUnicodeNames = new System.Windows.Forms.CheckBox();
			this.chkUseKoreanNames = new System.Windows.Forms.CheckBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblProjectName = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtNamespaces = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.dgvConfigurations = new System.Windows.Forms.DataGridView();
			this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colPlatform = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colConfig = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvConfigurations)).BeginInit();
			this.SuspendLayout();
			// 
			// chkRegenerateDebugInfo
			// 
			this.chkRegenerateDebugInfo.AutoSize = true;
			this.chkRegenerateDebugInfo.Location = new System.Drawing.Point(3, 3);
			this.chkRegenerateDebugInfo.Name = "chkRegenerateDebugInfo";
			this.chkRegenerateDebugInfo.Size = new System.Drawing.Size(138, 17);
			this.chkRegenerateDebugInfo.TabIndex = 0;
			this.chkRegenerateDebugInfo.Text = "Regenerate Debug Info";
			this.chkRegenerateDebugInfo.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.chkRegenerateDebugInfo, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.chkMarkedOnly, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.chkRenameProperties, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.chkRenameEvents, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.chkRenameFields, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.chkKeepPublicApi, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.chkHidePrivateApi, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.chkReuseNames, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.chkHideStrings, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.chkOptimizeMethods, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.chkSuppressIldasm, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.chkAnalyzeXaml, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.chkUseUnicodeNames, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.chkUseKoreanNames, 1, 6);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 164);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// chkMarkedOnly
			// 
			this.chkMarkedOnly.AutoSize = true;
			this.chkMarkedOnly.Location = new System.Drawing.Point(3, 26);
			this.chkMarkedOnly.Name = "chkMarkedOnly";
			this.chkMarkedOnly.Size = new System.Drawing.Size(86, 17);
			this.chkMarkedOnly.TabIndex = 0;
			this.chkMarkedOnly.Text = "Marked Only";
			this.chkMarkedOnly.UseVisualStyleBackColor = true;
			// 
			// chkRenameProperties
			// 
			this.chkRenameProperties.AutoSize = true;
			this.chkRenameProperties.Location = new System.Drawing.Point(3, 49);
			this.chkRenameProperties.Name = "chkRenameProperties";
			this.chkRenameProperties.Size = new System.Drawing.Size(116, 17);
			this.chkRenameProperties.TabIndex = 0;
			this.chkRenameProperties.Text = "Rename Properties";
			this.chkRenameProperties.UseVisualStyleBackColor = true;
			// 
			// chkRenameEvents
			// 
			this.chkRenameEvents.AutoSize = true;
			this.chkRenameEvents.Location = new System.Drawing.Point(3, 72);
			this.chkRenameEvents.Name = "chkRenameEvents";
			this.chkRenameEvents.Size = new System.Drawing.Size(102, 17);
			this.chkRenameEvents.TabIndex = 0;
			this.chkRenameEvents.Text = "Rename Events";
			this.chkRenameEvents.UseVisualStyleBackColor = true;
			// 
			// chkRenameFields
			// 
			this.chkRenameFields.AutoSize = true;
			this.chkRenameFields.Location = new System.Drawing.Point(3, 95);
			this.chkRenameFields.Name = "chkRenameFields";
			this.chkRenameFields.Size = new System.Drawing.Size(93, 17);
			this.chkRenameFields.TabIndex = 0;
			this.chkRenameFields.Text = "RenameFields";
			this.chkRenameFields.UseVisualStyleBackColor = true;
			// 
			// chkKeepPublicApi
			// 
			this.chkKeepPublicApi.AutoSize = true;
			this.chkKeepPublicApi.Location = new System.Drawing.Point(3, 118);
			this.chkKeepPublicApi.Name = "chkKeepPublicApi";
			this.chkKeepPublicApi.Size = new System.Drawing.Size(101, 17);
			this.chkKeepPublicApi.TabIndex = 0;
			this.chkKeepPublicApi.Text = "Keep Public Api";
			this.chkKeepPublicApi.UseVisualStyleBackColor = true;
			// 
			// chkHidePrivateApi
			// 
			this.chkHidePrivateApi.AutoSize = true;
			this.chkHidePrivateApi.Location = new System.Drawing.Point(3, 141);
			this.chkHidePrivateApi.Name = "chkHidePrivateApi";
			this.chkHidePrivateApi.Size = new System.Drawing.Size(102, 17);
			this.chkHidePrivateApi.TabIndex = 0;
			this.chkHidePrivateApi.Text = "Hide Private Api";
			this.chkHidePrivateApi.UseVisualStyleBackColor = true;
			// 
			// chkReuseNames
			// 
			this.chkReuseNames.AutoSize = true;
			this.chkReuseNames.Location = new System.Drawing.Point(195, 3);
			this.chkReuseNames.Name = "chkReuseNames";
			this.chkReuseNames.Size = new System.Drawing.Size(93, 17);
			this.chkReuseNames.TabIndex = 0;
			this.chkReuseNames.Text = "Reuse Names";
			this.chkReuseNames.UseVisualStyleBackColor = true;
			// 
			// chkHideStrings
			// 
			this.chkHideStrings.AutoSize = true;
			this.chkHideStrings.Location = new System.Drawing.Point(195, 26);
			this.chkHideStrings.Name = "chkHideStrings";
			this.chkHideStrings.Size = new System.Drawing.Size(83, 17);
			this.chkHideStrings.TabIndex = 0;
			this.chkHideStrings.Text = "Hide Strings";
			this.chkHideStrings.UseVisualStyleBackColor = true;
			// 
			// chkOptimizeMethods
			// 
			this.chkOptimizeMethods.AutoSize = true;
			this.chkOptimizeMethods.Location = new System.Drawing.Point(195, 49);
			this.chkOptimizeMethods.Name = "chkOptimizeMethods";
			this.chkOptimizeMethods.Size = new System.Drawing.Size(107, 17);
			this.chkOptimizeMethods.TabIndex = 0;
			this.chkOptimizeMethods.Text = "OptimizeMethods";
			this.chkOptimizeMethods.UseVisualStyleBackColor = true;
			// 
			// chkSuppressIldasm
			// 
			this.chkSuppressIldasm.AutoSize = true;
			this.chkSuppressIldasm.Location = new System.Drawing.Point(195, 72);
			this.chkSuppressIldasm.Name = "chkSuppressIldasm";
			this.chkSuppressIldasm.Size = new System.Drawing.Size(103, 17);
			this.chkSuppressIldasm.TabIndex = 0;
			this.chkSuppressIldasm.Text = "Suppress Ildasm";
			this.chkSuppressIldasm.UseVisualStyleBackColor = true;
			// 
			// chkAnalyzeXaml
			// 
			this.chkAnalyzeXaml.AutoSize = true;
			this.chkAnalyzeXaml.Location = new System.Drawing.Point(195, 95);
			this.chkAnalyzeXaml.Name = "chkAnalyzeXaml";
			this.chkAnalyzeXaml.Size = new System.Drawing.Size(89, 17);
			this.chkAnalyzeXaml.TabIndex = 0;
			this.chkAnalyzeXaml.Text = "Analyze Xaml";
			this.chkAnalyzeXaml.UseVisualStyleBackColor = true;
			// 
			// chkUseUnicodeNames
			// 
			this.chkUseUnicodeNames.AutoSize = true;
			this.chkUseUnicodeNames.Location = new System.Drawing.Point(195, 118);
			this.chkUseUnicodeNames.Name = "chkUseUnicodeNames";
			this.chkUseUnicodeNames.Size = new System.Drawing.Size(124, 17);
			this.chkUseUnicodeNames.TabIndex = 0;
			this.chkUseUnicodeNames.Text = "Use Unicode Names";
			this.chkUseUnicodeNames.UseVisualStyleBackColor = true;
			// 
			// chkUseKoreanNames
			// 
			this.chkUseKoreanNames.AutoSize = true;
			this.chkUseKoreanNames.Location = new System.Drawing.Point(195, 141);
			this.chkUseKoreanNames.Name = "chkUseKoreanNames";
			this.chkUseKoreanNames.Size = new System.Drawing.Size(118, 17);
			this.chkUseKoreanNames.TabIndex = 0;
			this.chkUseKoreanNames.Text = "Use Korean Names";
			this.chkUseKoreanNames.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(333, 604);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(252, 604);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblProjectName
			// 
			this.lblProjectName.AutoSize = true;
			this.lblProjectName.Location = new System.Drawing.Point(88, 9);
			this.lblProjectName.Name = "lblProjectName";
			this.lblProjectName.Size = new System.Drawing.Size(65, 13);
			this.lblProjectName.TabIndex = 1;
			this.lblProjectName.Text = "UNKNOWN";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Project:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtNamespaces);
			this.groupBox1.Location = new System.Drawing.Point(12, 392);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(395, 206);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Namespaces To Skip";
			// 
			// txtNamespaces
			// 
			this.txtNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNamespaces.Location = new System.Drawing.Point(6, 19);
			this.txtNamespaces.Multiline = true;
			this.txtNamespaces.Name = "txtNamespaces";
			this.txtNamespaces.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtNamespaces.Size = new System.Drawing.Size(383, 181);
			this.txtNamespaces.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.tableLayoutPanel1);
			this.groupBox2.Location = new System.Drawing.Point(11, 198);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(396, 188);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Obfuscar Settings";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.dgvConfigurations);
			this.groupBox3.Location = new System.Drawing.Point(11, 25);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(396, 167);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Configurations";
			// 
			// dgvConfigurations
			// 
			this.dgvConfigurations.AllowUserToAddRows = false;
			this.dgvConfigurations.AllowUserToDeleteRows = false;
			this.dgvConfigurations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvConfigurations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvConfigurations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colPlatform,
            this.colConfig});
			this.dgvConfigurations.Location = new System.Drawing.Point(6, 18);
			this.dgvConfigurations.Name = "dgvConfigurations";
			this.dgvConfigurations.RowHeadersVisible = false;
			this.dgvConfigurations.Size = new System.Drawing.Size(384, 143);
			this.dgvConfigurations.TabIndex = 0;
			// 
			// colName
			// 
			this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colName.HeaderText = "Name";
			this.colName.Name = "colName";
			this.colName.ReadOnly = true;
			// 
			// colPlatform
			// 
			this.colPlatform.HeaderText = "Platform";
			this.colPlatform.Name = "colPlatform";
			this.colPlatform.ReadOnly = true;
			// 
			// colConfig
			// 
			this.colConfig.HeaderText = "Enable";
			this.colConfig.Name = "colConfig";
			this.colConfig.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colConfig.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.colConfig.Width = 60;
			// 
			// FrmObfuscarSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(420, 632);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lblProjectName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmObfuscarSettings";
			this.Text = "Obfuscar Settings";
			this.Load += new System.EventHandler(this.FrmObfuscarSettings_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvConfigurations)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkRegenerateDebugInfo;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox chkMarkedOnly;
		private System.Windows.Forms.CheckBox chkRenameProperties;
		private System.Windows.Forms.CheckBox chkRenameEvents;
		private System.Windows.Forms.CheckBox chkRenameFields;
		private System.Windows.Forms.CheckBox chkKeepPublicApi;
		private System.Windows.Forms.CheckBox chkHidePrivateApi;
		private System.Windows.Forms.CheckBox chkReuseNames;
		private System.Windows.Forms.CheckBox chkHideStrings;
		private System.Windows.Forms.CheckBox chkOptimizeMethods;
		private System.Windows.Forms.CheckBox chkSuppressIldasm;
		private System.Windows.Forms.CheckBox chkAnalyzeXaml;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkUseUnicodeNames;
		private System.Windows.Forms.CheckBox chkUseKoreanNames;
		private System.Windows.Forms.Label lblProjectName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtNamespaces;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView dgvConfigurations;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPlatform;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colConfig;
	}
}