//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

namespace GlobalBank.Infrastructure.Shell
{
	partial class ShellForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellForm));
			this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._mainToolStrip = new System.Windows.Forms.ToolStrip();
			this._mainStatusStrip = new System.Windows.Forms.StatusStrip();
			this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._leftWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
			this._rightWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
			this._mainMenuStrip.SuspendLayout();
			this._mainStatusStrip.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _mainMenuStrip
			// 
			this._mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			resources.ApplyResources(this._mainMenuStrip, "_mainMenuStrip");
			this._mainMenuStrip.Name = "_mainMenuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnFileExit);
			// 
			// _mainToolStrip
			// 
			resources.ApplyResources(this._mainToolStrip, "_mainToolStrip");
			this._mainToolStrip.Name = "_mainToolStrip";
			// 
			// _mainStatusStrip
			// 
			this._mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel});
			resources.ApplyResources(this._mainStatusStrip, "_mainStatusStrip");
			this._mainStatusStrip.Name = "_mainStatusStrip";
			// 
			// _statusLabel
			// 
			this._statusLabel.Name = "_statusLabel";
			resources.ApplyResources(this._statusLabel, "_statusLabel");
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._leftWorkspace);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._rightWorkspace);
			// 
			// _leftWorkspace
			// 
			resources.ApplyResources(this._leftWorkspace, "_leftWorkspace");
			this._leftWorkspace.Name = "_leftWorkspace";
			// 
			// _rightWorkspace
			// 
			resources.ApplyResources(this._rightWorkspace, "_rightWorkspace");
			this._rightWorkspace.Name = "_rightWorkspace";
			// 
			// ShellForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this._mainStatusStrip);
			this.Controls.Add(this._mainToolStrip);
			this.Controls.Add(this._mainMenuStrip);
			this.MainMenuStrip = this._mainMenuStrip;
			this.Name = "ShellForm";
			this._mainMenuStrip.ResumeLayout(false);
			this._mainMenuStrip.PerformLayout();
			this._mainStatusStrip.ResumeLayout(false);
			this._mainStatusStrip.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip _mainMenuStrip;
		private System.Windows.Forms.ToolStrip _mainToolStrip;
		private System.Windows.Forms.StatusStrip _mainStatusStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace _rightWorkspace;
		private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace _leftWorkspace;
		private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
	}
}
