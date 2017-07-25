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

namespace GlobalBank.AppraiserWorkbench.AppraiserWorkbenchModule.Views.AvailableAppraisals
{
	partial class AvailableAppraisalsView
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Label label9;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvailableAppraisalsView));
			System.Windows.Forms.Label label7;
			System.Windows.Forms.Label label6;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label8;
			this._propertyAddressTextBox = new System.Windows.Forms.TextBox();
			this._bindingSource = new System.Windows.Forms.BindingSource(this.components);
			this._notesTextBox = new System.Windows.Forms.TextBox();
			this._descriptionTextBox = new System.Windows.Forms.TextBox();
			this._propertyTypeLabel = new System.Windows.Forms.Label();
			this._dateToCompleteLabel = new System.Windows.Forms.Label();
			this._idLabel = new System.Windows.Forms.Label();
			this._listView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._coverPanel = new System.Windows.Forms.Panel();
			this._progressBar = new System.Windows.Forms.ProgressBar();
			this._coverLabel = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this._bindingSource)).BeginInit();
			this._coverPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label9
			// 
			resources.ApplyResources(label9, "label9");
			label9.Name = "label9";
			// 
			// label7
			// 
			resources.ApplyResources(label7, "label7");
			label7.Name = "label7";
			// 
			// label6
			// 
			resources.ApplyResources(label6, "label6");
			label6.Name = "label6";
			// 
			// label4
			// 
			resources.ApplyResources(label4, "label4");
			label4.Name = "label4";
			// 
			// label3
			// 
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			// 
			// label2
			// 
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			// 
			// label1
			// 
			label1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			resources.ApplyResources(label1, "label1");
			label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			label1.Name = "label1";
			// 
			// label8
			// 
			label8.BackColor = System.Drawing.SystemColors.InactiveCaption;
			resources.ApplyResources(label8, "label8");
			label8.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			label8.Name = "label8";
			// 
			// _propertyAddressTextBox
			// 
			resources.ApplyResources(this._propertyAddressTextBox, "_propertyAddressTextBox");
			this._propertyAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._propertyAddressTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._bindingSource, "PropertyAddress", true));
			this._propertyAddressTextBox.Name = "_propertyAddressTextBox";
			this._propertyAddressTextBox.ReadOnly = true;
			// 
			// _bindingSource
			// 
			this._bindingSource.DataSource = typeof(GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.BusinessEntities.Appraisal);
			// 
			// _notesTextBox
			// 
			resources.ApplyResources(this._notesTextBox, "_notesTextBox");
			this._notesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._notesTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._bindingSource, "Notes", true));
			this._notesTextBox.Name = "_notesTextBox";
			this._notesTextBox.ReadOnly = true;
			// 
			// _descriptionTextBox
			// 
			resources.ApplyResources(this._descriptionTextBox, "_descriptionTextBox");
			this._descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._descriptionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._bindingSource, "Description", true));
			this._descriptionTextBox.Name = "_descriptionTextBox";
			this._descriptionTextBox.ReadOnly = true;
			// 
			// _propertyTypeLabel
			// 
			resources.ApplyResources(this._propertyTypeLabel, "_propertyTypeLabel");
			this._propertyTypeLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._bindingSource, "PropertyType", true));
			this._propertyTypeLabel.Name = "_propertyTypeLabel";
			// 
			// _dateToCompleteLabel
			// 
			resources.ApplyResources(this._dateToCompleteLabel, "_dateToCompleteLabel");
			this._dateToCompleteLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._bindingSource, "DateToComplete", true));
			this._dateToCompleteLabel.Name = "_dateToCompleteLabel";
			// 
			// _idLabel
			// 
			resources.ApplyResources(this._idLabel, "_idLabel");
			this._idLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._bindingSource, "Id", true));
			this._idLabel.Name = "_idLabel";
			// 
			// _listView
			// 
			resources.ApplyResources(this._listView, "_listView");
			this._listView.CheckBoxes = true;
			this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this._listView.FullRowSelect = true;
			this._listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this._listView.HideSelection = false;
			this._listView.Name = "_listView";
			this._listView.UseCompatibleStateImageBehavior = false;
			this._listView.View = System.Windows.Forms.View.Details;
			this._listView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewItemChecked);
			this._listView.SelectedIndexChanged += new System.EventHandler(this.SelectedListViewItemChanged);
			// 
			// _okButton
			// 
			resources.ApplyResources(this._okButton, "_okButton");
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this.OkButtonClicked);
			// 
			// _cancelButton
			// 
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			// 
			// _coverPanel
			// 
			resources.ApplyResources(this._coverPanel, "_coverPanel");
			this._coverPanel.Controls.Add(this._progressBar);
			this._coverPanel.Controls.Add(this._coverLabel);
			this._coverPanel.Name = "_coverPanel";
			// 
			// _progressBar
			// 
			resources.ApplyResources(this._progressBar, "_progressBar");
			this._progressBar.Name = "_progressBar";
			// 
			// _coverLabel
			// 
			resources.ApplyResources(this._coverLabel, "_coverLabel");
			this._coverLabel.Name = "_coverLabel";
			// 
			// AvailableAppraisalsView
			// 
			this.AcceptButton = this._okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancelButton;
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._listView);
			this.Controls.Add(label8);
			this.Controls.Add(label9);
			this.Controls.Add(this._propertyAddressTextBox);
			this.Controls.Add(this._notesTextBox);
			this.Controls.Add(label7);
			this.Controls.Add(this._descriptionTextBox);
			this.Controls.Add(label6);
			this.Controls.Add(this._propertyTypeLabel);
			this.Controls.Add(label4);
			this.Controls.Add(this._dateToCompleteLabel);
			this.Controls.Add(label3);
			this.Controls.Add(label2);
			this.Controls.Add(this._idLabel);
			this.Controls.Add(label1);
			this.Controls.Add(this._coverPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AvailableAppraisalsView";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			((System.ComponentModel.ISupportInitialize)(this._bindingSource)).EndInit();
			this._coverPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _propertyAddressTextBox;
		private System.Windows.Forms.TextBox _notesTextBox;
		private System.Windows.Forms.TextBox _descriptionTextBox;
		private System.Windows.Forms.Label _propertyTypeLabel;
		private System.Windows.Forms.Label _dateToCompleteLabel;
		private System.Windows.Forms.Label _idLabel;
		private System.Windows.Forms.ListView _listView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.BindingSource _bindingSource;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Panel _coverPanel;
		private System.Windows.Forms.Label _coverLabel;
		private System.Windows.Forms.ProgressBar _progressBar;
	}
}
