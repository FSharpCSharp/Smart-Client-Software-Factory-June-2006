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

//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

namespace GlobalBank.BranchSystems.Module.Views
{
	partial class FindCustomerResultsView
	{
		/// <summary>
		/// The presenter used by this view.
		/// </summary>
		private GlobalBank.BranchSystems.Module.Views.FindCustomerResultsViewPresenter _presenter = null;

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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindCustomerResultsView));
			this._customerDataGridView = new System.Windows.Forms.DataGridView();
			this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.middleInitialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.motherMaidenNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.customerLevelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._customerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label12 = new System.Windows.Forms.Label();
			this._ssnTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this._zipTextBox = new System.Windows.Forms.TextBox();
			this._addressBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label11 = new System.Windows.Forms.Label();
			this._emailTextBox = new System.Windows.Forms.TextBox();
			this._emailAddressBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label10 = new System.Windows.Forms.Label();
			this._cellNumberTextBox = new System.Windows.Forms.TextBox();
			this._mobilePhoneNumberBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label9 = new System.Windows.Forms.Label();
			this._workNumberTextBox = new System.Windows.Forms.TextBox();
			this._workPhoneNumberBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label8 = new System.Windows.Forms.Label();
			this._homeNumberTextBox = new System.Windows.Forms.TextBox();
			this._homePhoneNumberBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label7 = new System.Windows.Forms.Label();
			this._stateTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this._cityTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this._streetTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._middleInitialTextBox = new System.Windows.Forms.TextBox();
			this._lastNameTextBox = new System.Windows.Forms.TextBox();
			this._firstNameTextBox = new System.Windows.Forms.TextBox();
			this._cancelButton = new System.Windows.Forms.Button();
			this._queueForServiceButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._customerDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._customerBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._addressBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._emailAddressBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._mobilePhoneNumberBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._workPhoneNumberBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._homePhoneNumberBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// _customerDataGridView
			// 
			this._customerDataGridView.AllowUserToAddRows = false;
			this._customerDataGridView.AllowUserToDeleteRows = false;
			this._customerDataGridView.AutoGenerateColumns = false;
			this._customerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._customerDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lastNameDataGridViewTextBoxColumn,
            this.middleInitialDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.motherMaidenNameDataGridViewTextBoxColumn,
            this.customerLevelDataGridViewTextBoxColumn});
			this._customerDataGridView.DataSource = this._customerBindingSource;
			resources.ApplyResources(this._customerDataGridView, "_customerDataGridView");
			this._customerDataGridView.Name = "_customerDataGridView";
			this._customerDataGridView.ReadOnly = true;
			// 
			// lastNameDataGridViewTextBoxColumn
			// 
			this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
			resources.ApplyResources(this.lastNameDataGridViewTextBoxColumn, "lastNameDataGridViewTextBoxColumn");
			this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
			this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// middleInitialDataGridViewTextBoxColumn
			// 
			this.middleInitialDataGridViewTextBoxColumn.DataPropertyName = "MiddleInitial";
			resources.ApplyResources(this.middleInitialDataGridViewTextBoxColumn, "middleInitialDataGridViewTextBoxColumn");
			this.middleInitialDataGridViewTextBoxColumn.Name = "middleInitialDataGridViewTextBoxColumn";
			this.middleInitialDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// firstNameDataGridViewTextBoxColumn
			// 
			this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
			resources.ApplyResources(this.firstNameDataGridViewTextBoxColumn, "firstNameDataGridViewTextBoxColumn");
			this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
			this.firstNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// motherMaidenNameDataGridViewTextBoxColumn
			// 
			this.motherMaidenNameDataGridViewTextBoxColumn.DataPropertyName = "MotherMaidenName";
			resources.ApplyResources(this.motherMaidenNameDataGridViewTextBoxColumn, "motherMaidenNameDataGridViewTextBoxColumn");
			this.motherMaidenNameDataGridViewTextBoxColumn.Name = "motherMaidenNameDataGridViewTextBoxColumn";
			this.motherMaidenNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// customerLevelDataGridViewTextBoxColumn
			// 
			this.customerLevelDataGridViewTextBoxColumn.DataPropertyName = "CustomerLevel";
			resources.ApplyResources(this.customerLevelDataGridViewTextBoxColumn, "customerLevelDataGridViewTextBoxColumn");
			this.customerLevelDataGridViewTextBoxColumn.Name = "customerLevelDataGridViewTextBoxColumn";
			this.customerLevelDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// _customerBindingSource
			// 
			this._customerBindingSource.DataSource = typeof(GlobalBank.Infrastructure.Interface.BusinessEntities.Customer);
			this._customerBindingSource.CurrentChanged += new System.EventHandler(this._customerBindingSource_CurrentChanged);
			// 
			// label12
			// 
			resources.ApplyResources(this.label12, "label12");
			this.label12.Name = "label12";
			// 
			// _ssnTextBox
			// 
			this._ssnTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._ssnTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._customerBindingSource, "SocialSecurityNumber", true));
			resources.ApplyResources(this._ssnTextBox, "_ssnTextBox");
			this._ssnTextBox.Name = "_ssnTextBox";
			this._ssnTextBox.ReadOnly = true;
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// _zipTextBox
			// 
			this._zipTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._zipTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._addressBindingSource, "PostalZipCode", true));
			resources.ApplyResources(this._zipTextBox, "_zipTextBox");
			this._zipTextBox.Name = "_zipTextBox";
			this._zipTextBox.ReadOnly = true;
			// 
			// _addressBindingSource
			// 
			this._addressBindingSource.DataSource = typeof(GlobalBank.Infrastructure.Interface.BusinessEntities.Address);
			// 
			// label11
			// 
			resources.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			// 
			// _emailTextBox
			// 
			this._emailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._emailTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._emailAddressBindingSource, "Address", true));
			resources.ApplyResources(this._emailTextBox, "_emailTextBox");
			this._emailTextBox.Name = "_emailTextBox";
			this._emailTextBox.ReadOnly = true;
			// 
			// _emailAddressBindingSource
			// 
			this._emailAddressBindingSource.DataSource = typeof(GlobalBank.Infrastructure.Interface.BusinessEntities.EmailAddress);
			// 
			// label10
			// 
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			// 
			// _cellNumberTextBox
			// 
			this._cellNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._cellNumberTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._mobilePhoneNumberBindingSource, "Number", true));
			resources.ApplyResources(this._cellNumberTextBox, "_cellNumberTextBox");
			this._cellNumberTextBox.Name = "_cellNumberTextBox";
			this._cellNumberTextBox.ReadOnly = true;
			// 
			// _mobilePhoneNumberBindingSource
			// 
			this._mobilePhoneNumberBindingSource.DataSource = typeof(GlobalBank.Infrastructure.Interface.BusinessEntities.PhoneNumber);
			// 
			// label9
			// 
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			// 
			// _workNumberTextBox
			// 
			this._workNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._workNumberTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._workPhoneNumberBindingSource, "Number", true));
			resources.ApplyResources(this._workNumberTextBox, "_workNumberTextBox");
			this._workNumberTextBox.Name = "_workNumberTextBox";
			this._workNumberTextBox.ReadOnly = true;
			// 
			// _workPhoneNumberBindingSource
			// 
			this._workPhoneNumberBindingSource.DataSource = typeof(GlobalBank.Infrastructure.Interface.BusinessEntities.PhoneNumber);
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// _homeNumberTextBox
			// 
			this._homeNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._homeNumberTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._homePhoneNumberBindingSource, "Number", true));
			resources.ApplyResources(this._homeNumberTextBox, "_homeNumberTextBox");
			this._homeNumberTextBox.Name = "_homeNumberTextBox";
			this._homeNumberTextBox.ReadOnly = true;
			// 
			// _homePhoneNumberBindingSource
			// 
			this._homePhoneNumberBindingSource.DataSource = typeof(GlobalBank.Infrastructure.Interface.BusinessEntities.PhoneNumber);
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// _stateTextBox
			// 
			this._stateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._stateTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._addressBindingSource, "StateProvince", true));
			resources.ApplyResources(this._stateTextBox, "_stateTextBox");
			this._stateTextBox.Name = "_stateTextBox";
			this._stateTextBox.ReadOnly = true;
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// _cityTextBox
			// 
			this._cityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._cityTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._addressBindingSource, "City", true));
			resources.ApplyResources(this._cityTextBox, "_cityTextBox");
			this._cityTextBox.Name = "_cityTextBox";
			this._cityTextBox.ReadOnly = true;
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// _streetTextBox
			// 
			this._streetTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._streetTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._addressBindingSource, "Address1", true));
			resources.ApplyResources(this._streetTextBox, "_streetTextBox");
			this._streetTextBox.Name = "_streetTextBox";
			this._streetTextBox.ReadOnly = true;
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _middleInitialTextBox
			// 
			this._middleInitialTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._middleInitialTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._customerBindingSource, "MiddleInitial", true));
			resources.ApplyResources(this._middleInitialTextBox, "_middleInitialTextBox");
			this._middleInitialTextBox.Name = "_middleInitialTextBox";
			this._middleInitialTextBox.ReadOnly = true;
			// 
			// _lastNameTextBox
			// 
			this._lastNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._lastNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._customerBindingSource, "LastName", true));
			resources.ApplyResources(this._lastNameTextBox, "_lastNameTextBox");
			this._lastNameTextBox.Name = "_lastNameTextBox";
			this._lastNameTextBox.ReadOnly = true;
			// 
			// _firstNameTextBox
			// 
			this._firstNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._firstNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._customerBindingSource, "FirstName", true));
			resources.ApplyResources(this._firstNameTextBox, "_firstNameTextBox");
			this._firstNameTextBox.Name = "_firstNameTextBox";
			this._firstNameTextBox.ReadOnly = true;
			// 
			// _cancelButton
			// 
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
			// 
			// _queueForServiceButton
			// 
			resources.ApplyResources(this._queueForServiceButton, "_queueForServiceButton");
			this._queueForServiceButton.Name = "_queueForServiceButton";
			this._queueForServiceButton.UseVisualStyleBackColor = true;
			this._queueForServiceButton.Click += new System.EventHandler(this._queueForServiceButton_Click);
			// 
			// FindCustomerResultsView
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._queueForServiceButton);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this.label12);
			this.Controls.Add(this._ssnTextBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this._zipTextBox);
			this.Controls.Add(this.label11);
			this.Controls.Add(this._emailTextBox);
			this.Controls.Add(this.label10);
			this.Controls.Add(this._cellNumberTextBox);
			this.Controls.Add(this.label9);
			this.Controls.Add(this._workNumberTextBox);
			this.Controls.Add(this.label8);
			this.Controls.Add(this._homeNumberTextBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this._stateTextBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this._cityTextBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this._streetTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._middleInitialTextBox);
			this.Controls.Add(this._lastNameTextBox);
			this.Controls.Add(this._firstNameTextBox);
			this.Controls.Add(this._customerDataGridView);
			this.Name = "FindCustomerResultsView";
			((System.ComponentModel.ISupportInitialize)(this._customerDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._customerBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._addressBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._emailAddressBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._mobilePhoneNumberBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._workPhoneNumberBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._homePhoneNumberBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView _customerDataGridView;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox _ssnTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox _zipTextBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox _emailTextBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox _cellNumberTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox _workNumberTextBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox _homeNumberTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox _stateTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox _cityTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox _streetTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _middleInitialTextBox;
		private System.Windows.Forms.TextBox _lastNameTextBox;
		private System.Windows.Forms.TextBox _firstNameTextBox;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.BindingSource _addressBindingSource;
		private System.Windows.Forms.BindingSource _homePhoneNumberBindingSource;
		private System.Windows.Forms.BindingSource _mobilePhoneNumberBindingSource;
		private System.Windows.Forms.BindingSource _workPhoneNumberBindingSource;
		private System.Windows.Forms.BindingSource _emailAddressBindingSource;
		private System.Windows.Forms.Button _queueForServiceButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn middleInitialDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn motherMaidenNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn customerLevelDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource _customerBindingSource;
	}
}

