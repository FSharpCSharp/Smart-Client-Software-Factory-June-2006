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

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AppraisalDetail
{
	partial class AppraisalDetailView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppraisalDetailView));
			this.label1 = new System.Windows.Forms.Label();
			this._idLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this._dateToCompleteLabel = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this._propertyTypeLabel = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this._descriptionTextBox = new System.Windows.Forms.TextBox();
			this._notesTextBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this._propertyAddressTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this._attachmentsListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this._coverPanel = new System.Windows.Forms.Panel();
			this._coverLabel = new System.Windows.Forms.LinkLabel();
			this._submitButton = new System.Windows.Forms.Button();
			this._coverPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			resources.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.label1.Name = "label1";
			// 
			// _idLabel
			// 
			resources.ApplyResources(this._idLabel, "_idLabel");
			this._idLabel.Name = "_idLabel";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// _dateToCompleteLabel
			// 
			resources.ApplyResources(this._dateToCompleteLabel, "_dateToCompleteLabel");
			this._dateToCompleteLabel.Name = "_dateToCompleteLabel";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// _propertyTypeLabel
			// 
			resources.ApplyResources(this._propertyTypeLabel, "_propertyTypeLabel");
			this._propertyTypeLabel.Name = "_propertyTypeLabel";
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// _descriptionTextBox
			// 
			resources.ApplyResources(this._descriptionTextBox, "_descriptionTextBox");
			this._descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._descriptionTextBox.Name = "_descriptionTextBox";
			this._descriptionTextBox.ReadOnly = true;
			// 
			// _notesTextBox
			// 
			resources.ApplyResources(this._notesTextBox, "_notesTextBox");
			this._notesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._notesTextBox.Name = "_notesTextBox";
			this._notesTextBox.ReadOnly = true;
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.label8.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.label8.Name = "label8";
			// 
			// _propertyAddressTextBox
			// 
			resources.ApplyResources(this._propertyAddressTextBox, "_propertyAddressTextBox");
			this._propertyAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._propertyAddressTextBox.Name = "_propertyAddressTextBox";
			this._propertyAddressTextBox.ReadOnly = true;
			// 
			// label9
			// 
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			// 
			// _attachmentsListView
			// 
			resources.ApplyResources(this._attachmentsListView, "_attachmentsListView");
			this._attachmentsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this._attachmentsListView.FullRowSelect = true;
			this._attachmentsListView.Name = "_attachmentsListView";
			this._attachmentsListView.UseCompatibleStateImageBehavior = false;
			this._attachmentsListView.View = System.Windows.Forms.View.Details;
			this._attachmentsListView.DoubleClick += new System.EventHandler(this._attachmentsListView_DoubleClick);
			// 
			// columnHeader1
			// 
			resources.ApplyResources(this.columnHeader1, "columnHeader1");
			// 
			// columnHeader2
			// 
			resources.ApplyResources(this.columnHeader2, "columnHeader2");
			// 
			// _coverPanel
			// 
			this._coverPanel.BackColor = System.Drawing.SystemColors.Control;
			this._coverPanel.Controls.Add(this._coverLabel);
			resources.ApplyResources(this._coverPanel, "_coverPanel");
			this._coverPanel.Name = "_coverPanel";
			// 
			// _coverLabel
			// 
			resources.ApplyResources(this._coverLabel, "_coverLabel");
			this._coverLabel.Name = "_coverLabel";
			this._coverLabel.TabStop = true;
			this._coverLabel.UseCompatibleTextRendering = true;
			this._coverLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._coverLabel_LinkClicked);
			// 
			// _submitButton
			// 
			resources.ApplyResources(this._submitButton, "_submitButton");
			this._submitButton.Name = "_submitButton";
			this._submitButton.UseVisualStyleBackColor = true;
			this._submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// AppraisalDetailView
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._attachmentsListView);
			this.Controls.Add(this.label9);
			this.Controls.Add(this._propertyAddressTextBox);
			this.Controls.Add(this.label8);
			this.Controls.Add(this._notesTextBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this._descriptionTextBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this._propertyTypeLabel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this._dateToCompleteLabel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._idLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._coverPanel);
			this.Controls.Add(this._submitButton);
			this.Name = "AppraisalDetailView";
			this._coverPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label _idLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label _dateToCompleteLabel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label _propertyTypeLabel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox _descriptionTextBox;
		private System.Windows.Forms.TextBox _notesTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox _propertyAddressTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListView _attachmentsListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel _coverPanel;
		private System.Windows.Forms.LinkLabel _coverLabel;
		private System.Windows.Forms.Button _submitButton;
	}
}
