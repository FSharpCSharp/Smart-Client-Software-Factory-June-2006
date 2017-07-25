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

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.MyAppraisals
{
	partial class MyAppraisalsView
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
			if (disposing)
			{
				if (_presenter != null)
					_presenter.Dispose();

				if (components != null)
					components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyAppraisalsView));
			this.listView1 = new System.Windows.Forms.ListView();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			resources.ApplyResources(this.listView1, "listView1");
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.List;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			resources.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.label1.Name = "label1";
			// 
			// MyAppraisalsView
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.label1);
			this.Name = "MyAppraisalsView";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label label1;
	}
}
