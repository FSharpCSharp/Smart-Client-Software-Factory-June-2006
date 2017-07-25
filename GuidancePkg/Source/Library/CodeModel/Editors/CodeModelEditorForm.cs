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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.Practices.WizardFramework;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library.CodeModel.Editors
{
	/// <summary>
	/// Allows browsing for a class name.
	/// </summary>
	internal class CodeModelEditorForm : Form
	{
		ITypeDescriptorContext _context;
		CodeModelEditor.BrowseRoot _root;
		CodeModelEditor.BrowseKind _kind;
		ICodeModelEditorFilter _filter;
		CodeElement _customRoot;
		Hashtable _filterState;
		bool _onlyUserCode;

		#region Designer stuff
		private ImageList _imgIcons;
		private IContainer _components;
		private Panel _pnlButtons;
		private Button _btnOK;
		private Button _btnCancel;
		private OpenFileDialog _dlgOpenAssembly;
		private ToolTip _tpTooltip;
		private CheckBox _flattenNameSpaces;
		private TreeView _tvBrowser;


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_components != null)
				{
					_components.Dispose();
				}
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
			this._components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(CodeModelEditorForm));
			TreeNode treeNode1 = new TreeNode("Base Class", 2, 2);
			TreeNode treeNode2 = new TreeNode("Interface", 3, 3);
			TreeNode treeNode3 = new TreeNode("Base Types", 5, 5, new TreeNode[] {
            treeNode1,
            treeNode2});
			TreeNode treeNode4 = new TreeNode("Event");
			TreeNode treeNode5 = new TreeNode("member");
			TreeNode treeNode6 = new TreeNode("param");
			TreeNode treeNode7 = new TreeNode("Method", new TreeNode[] {
            treeNode6});
			TreeNode treeNode8 = new TreeNode("Property");
			TreeNode treeNode9 = new TreeNode("Class", 2, 2, new TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode7,
            treeNode8});
			TreeNode treeNode10 = new TreeNode("Delegate");
			TreeNode treeNode11 = new TreeNode("Enum", 4, 4);
			TreeNode treeNode12 = new TreeNode("Interface", 3, 3);
			TreeNode treeNode13 = new TreeNode("Namespace", 1, 1, new TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
			TreeNode treeNode14 = new TreeNode("Assembly", 0, 0, new TreeNode[] {
            treeNode13});
			this._imgIcons = new ImageList(this._components);
			this._tvBrowser = new TreeView();
			this._pnlButtons = new Panel();
			this._flattenNameSpaces = new CheckBox();
			this._btnCancel = new Button();
			this._btnOK = new Button();
			this._dlgOpenAssembly = new OpenFileDialog();
			this._tpTooltip = new ToolTip(this._components);
			this._pnlButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// imgIcons
			// 
			this._imgIcons.ImageStream = ((ImageListStreamer)(resources.GetObject("imgIcons.ImageStream")));
			this._imgIcons.Images.SetKeyName(0, "Module");
			this._imgIcons.Images.SetKeyName(1, "Namespace");
			this._imgIcons.Images.SetKeyName(2, "Class");
			this._imgIcons.Images.SetKeyName(3, "Interface");
			this._imgIcons.Images.SetKeyName(4, "Enum");
			this._imgIcons.Images.SetKeyName(5, "BaseTypes");
			this._imgIcons.Images.SetKeyName(6, "Parameter");
			this._imgIcons.Images.SetKeyName(7, "Member");
			this._imgIcons.Images.SetKeyName(8, "EnumMember");
			this._imgIcons.Images.SetKeyName(9, "Method");
			this._imgIcons.Images.SetKeyName(10, "Delegate");
			this._imgIcons.Images.SetKeyName(11, "Event");
			this._imgIcons.Images.SetKeyName(12, "Property");
			// 
			// tvBrowser
			// 
			this._tvBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tvBrowser.ImageIndex = 0;
			this._tvBrowser.ImageList = this._imgIcons;
			this._tvBrowser.Location = new Point(0, 0);
			this._tvBrowser.Name = "tvBrowser";
			treeNode1.ImageIndex = 2;
			treeNode1.Name = "Interface";
			treeNode1.SelectedImageIndex = 2;
			treeNode1.Text = "Base Class";
			treeNode2.ImageIndex = 3;
			treeNode2.Name = "Node7";
			treeNode2.SelectedImageIndex = 3;
			treeNode2.Text = "Interface";
			treeNode3.ImageIndex = 5;
			treeNode3.Name = "Base Types";
			treeNode3.SelectedImageIndex = 5;
			treeNode3.Text = "Base Types";
			treeNode4.ImageKey = "Event";
			treeNode4.Name = "EVent";
			treeNode4.SelectedImageKey = "Event";
			treeNode4.Text = "Event";
			treeNode5.ImageKey = "Member";
			treeNode5.Name = "Node1";
			treeNode5.SelectedImageKey = "Member";
			treeNode5.Text = "member";
			treeNode6.ImageKey = "Parameter";
			treeNode6.Name = "Node0";
			treeNode6.SelectedImageKey = "Parameter";
			treeNode6.Text = "param";
			treeNode7.ImageKey = "Method";
			treeNode7.Name = "Node0";
			treeNode7.SelectedImageKey = "Method";
			treeNode7.Text = "Method";
			treeNode8.ImageKey = "Property";
			treeNode8.Name = "Node2";
			treeNode8.SelectedImageKey = "Property";
			treeNode8.Text = "Property";
			treeNode9.ImageIndex = 2;
			treeNode9.Name = "";
			treeNode9.SelectedImageIndex = 2;
			treeNode9.Text = "Class";
			treeNode10.ImageKey = "Delegate";
			treeNode10.Name = "Delegate";
			treeNode10.SelectedImageKey = "Delegate";
			treeNode10.Text = "Delegate";
			treeNode11.ImageIndex = 4;
			treeNode11.Name = "Enum";
			treeNode11.SelectedImageIndex = 4;
			treeNode11.Text = "Enum";
			treeNode12.ImageIndex = 3;
			treeNode12.Name = "Node6";
			treeNode12.SelectedImageIndex = 3;
			treeNode12.Text = "Interface";
			treeNode13.ImageIndex = 1;
			treeNode13.Name = "";
			treeNode13.SelectedImageIndex = 1;
			treeNode13.Text = "Namespace";
			treeNode14.ImageIndex = 0;
			treeNode14.Name = "";
			treeNode14.SelectedImageIndex = 0;
			treeNode14.Text = "Assembly";
			this._tvBrowser.Nodes.AddRange(new TreeNode[] {
            treeNode14});
			this._tvBrowser.SelectedImageIndex = 0;
			this._tvBrowser.Size = new Size(440, 462);
			this._tvBrowser.Sorted = true;
			this._tvBrowser.TabIndex = 0;
			this._tvBrowser.BeforeExpand += new TreeViewCancelEventHandler(this.OnBeforeExpand);
			this._tvBrowser.DoubleClick += new EventHandler(this.tvBrowser_DoubleClick);
			this._tvBrowser.AfterSelect += new TreeViewEventHandler(this.OnAfterSelect);
			// 
			// pnlButtons
			// 
			this._pnlButtons.Controls.Add(this._flattenNameSpaces);
			this._pnlButtons.Controls.Add(this._btnCancel);
			this._pnlButtons.Controls.Add(this._btnOK);
			this._pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._pnlButtons.Location = new Point(0, 462);
			this._pnlButtons.Name = "pnlButtons";
			this._pnlButtons.Size = new Size(440, 34);
			this._pnlButtons.TabIndex = 1;
			// 
			// flattenNameSpaces
			// 
			this._flattenNameSpaces.AutoSize = true;
			this._flattenNameSpaces.Location = new Point(12, 6);
			this._flattenNameSpaces.Name = "flattenNameSpaces";
			this._flattenNameSpaces.Size = new Size(119, 17);
			this._flattenNameSpaces.TabIndex = 2;
			this._flattenNameSpaces.Text = "Flatten Namespaces";
			this._flattenNameSpaces.CheckedChanged += new EventHandler(this.flattenNameSpaces_CheckedChanged);
			// 
			// btnCancel
			// 
			this._btnCancel.Anchor = ((AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnCancel.Location = new Point(362, 6);
			this._btnCancel.Name = "btnCancel";
			this._btnCancel.Size = new Size(75, 23);
			this._btnCancel.TabIndex = 1;
			this._btnCancel.Text = "&Cancel";
			// 
			// btnOK
			// 
			this._btnOK.Anchor = ((AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._btnOK.Enabled = false;
			this._btnOK.Location = new Point(282, 6);
			this._btnOK.Name = "btnOK";
			this._btnOK.Size = new Size(75, 23);
			this._btnOK.TabIndex = 0;
			this._btnOK.Text = "&OK";
			// 
			// dlgOpenAssembly
			// 
			this._dlgOpenAssembly.DefaultExt = "dll";
			this._dlgOpenAssembly.Filter = "Assemblies|*.dll;*.exe";
			this._dlgOpenAssembly.Title = "Open assembly";
			// 
			// CodeModelEditorForm
			// 
			this.AcceptButton = this._btnOK;
			this.CancelButton = this._btnCancel;
			this.ClientSize = new Size(440, 496);
			this.Controls.Add(this._tvBrowser);
			this.Controls.Add(this._pnlButtons);
			this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CodeModelEditorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Code Model Editor";
			this._pnlButtons.ResumeLayout(false);
			this._pnlButtons.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region Ctor

		/// <summary>
		/// Provides a UI for the <see cref="CodeModelEditor"/>
		/// </summary>
		public CodeModelEditorForm(
				ITypeDescriptorContext context,
				CodeModelEditor.BrowseRoot root,
				CodeModelEditor.BrowseKind kind,
				ICodeModelEditorFilter filter,
				CodeElement customRoot,
				bool onlyUserCode)
			: this()
		{
			this._context = context;
			this._customRoot = customRoot;
			this._root = root;
			this._kind = kind;
			this._filter = filter;
			this._filterState = new Hashtable();
			this._onlyUserCode = onlyUserCode;
		}

		/// <summary>
		/// Provides a UI for the <see cref="CodeModelEditor"/>
		/// </summary>
		public CodeModelEditorForm()
		{
			InitializeComponent();
		}

		#endregion Ctor

		#region Protected methods

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			LoadRootNodes();
		}

		#endregion Protected methods

		#region Private Implementation

		private CodeElements GetChilds(CodeElement codeElement)
		{
			if ((GetBrowseKind(codeElement) & this._kind) == this._kind)
			{
				return null;
			}
			return new CodeElementEx(codeElement).Members;
		}

		private bool Filter(CodeElement codeElement)
		{
			if (!CanBrowse(GetBrowseKind(codeElement)))
			{
				return true;
			}
			CodeElements children = GetChilds(codeElement);
			if (children != null)
			{
				bool hasValidChildren = false;
				if (children != null)
				{
					foreach (CodeElement child in children)
					{
						if (!Filter(child))
						{
							hasValidChildren = true;
							break;
						}
					}
				}
				if (!hasValidChildren)
				{
					return true;
				}
			}
			if (!_filterState.ContainsKey(codeElement))
			{
				bool filterResult = !CanBrowse(GetBrowseKind(codeElement));
				if (!filterResult && _filter != null)
				{
					try
					{
						filterResult = _filter.Filter(codeElement);
					}
					catch
					{
						filterResult = false;
					}
				}
				_filterState.Add(codeElement, filterResult);
			}
			return (bool)_filterState[codeElement];
		}

		private void LoadProject(Project prj)
		{
			if (prj.Object is SolutionFolder)
			{
				foreach (ProjectItem prjItem in prj.ProjectItems)
				{
					if (prjItem.Object is Project)
					{
						LoadProject(((Project)prjItem.Object));
					}
				}
				// Ignore solution folders
				return;
			}
			if (prj.CodeModel == null)
			{
				// Ignore project that do not support the code model
				return;
			}
			AddNodeWithChilds(this._tvBrowser.Nodes, prj);
		}

		private void AddNodeWithChilds(TreeNodeCollection parent, object obj)
		{
			TreeNode node = null;
			if (obj is Project)
			{
				node = new ProjectNode((Project)obj);
			}
			else if (obj is CodeNamespace)
			{
				node = new NamespaceNode(((CodeNamespace)obj).FullName, this);
			}
			else if (obj is CodeClass)
			{
				node = new ClassNode((CodeClass)obj, this);
			}
			else
			{
				throw new InvalidOperationException("Invalid root type");
			}
			AddNodeWithChilds(parent, node);
		}

		private void AddNodeWithChilds(TreeNodeCollection parent, TreeNode node)
		{
			// So that the + sign appears.
			TreeNode mockNode = new TreeNode("mock");
			mockNode.Tag = MockMarker;
			node.Nodes.Add(mockNode);
			parent.Add(node);
		}

		private void LoadRootNodes()
		{
			_tvBrowser.Nodes.Clear();
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				DTE vs = (DTE)GetService(typeof(DTE));
				if (_root == CodeModelEditor.BrowseRoot.Solution)
				{
					Projects projects = (Projects)vs.Solution.Projects;
					foreach (Project prj in projects)
					{
						LoadProject(prj);
					}
				}
				else if (_root == CodeModelEditor.BrowseRoot.SelectedProjects)
				{
					foreach (SelectedItem selection in vs.SelectedItems)
					{
						if (selection.Project != null)
						{
							LoadProject(selection.Project);
						}
						else if (selection.ProjectItem != null)
						{
							LoadProject(selection.ProjectItem.ContainingProject);
						}
					}
				}
				else if (_root == CodeModelEditor.BrowseRoot.CustomRoot)
				{
					LoadCustomRoot();
				}
			}
			catch (Exception e)
			{
				_tvBrowser.Nodes.Clear();
				throw e;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void LoadCustomRoot()
		{
			if (_customRoot == null)
			{
				throw new InvalidOperationException("Custom root not specified or not found.");
			}
			AddNodeWithChilds(_tvBrowser.Nodes, _customRoot);
		}

		private object MockMarker = new object();

		private bool HasToCreateChilds(TreeNode node)
		{
			return (node.Nodes.Count == 1 && node.Nodes[0].Tag == MockMarker);
		}

		#endregion

		#region Event handlers

		private bool NameSpaceIsEmpty(CodeNamespace cmNamespace)
		{
			foreach (CodeElement codeElement in cmNamespace.Members)
			{
				if (!(codeElement is CodeNamespace))
				{
					return false;
				}
				return NameSpaceIsEmpty((CodeNamespace)codeElement);
			}
			return true;
		}

		private void AddNamespace(TreeNode parentNode, CodeNamespace cmNamespace)
		{
			if (Filter((CodeElement)cmNamespace))
			{
				return;
			}
			TreeNode namespaceNode = null;
			string name = cmNamespace.Name;
			if (_flattenNameSpaces.Checked)
			{
				name = cmNamespace.FullName;
			}
			TreeNode[] namespaceNodes = parentNode.Nodes.Find(name, false);
			if (namespaceNodes.Length == 1)
			{
				namespaceNode = namespaceNodes[0];
			}
			else
			{
				namespaceNode = new NamespaceNode(name, this);
				AddNodeWithChilds(parentNode.Nodes, namespaceNode);
			}
			((List<CodeNamespace>)namespaceNode.Tag).Add(cmNamespace);
		}

		private void BeforeExpand(TreeNode parentNode, CodeNamespace cmNamespace)
		{
			if (Filter((CodeElement)cmNamespace))
			{
				return;
			}
			if (NameSpaceIsEmpty(cmNamespace))
			{
				return;
			}
			if (_flattenNameSpaces.Checked)
			{
				if (parentNode is NamespaceNode)
				{
					// If we have flatten namespaces then don't expand child namespaces
					return;
				}
				List<CodeNamespace> nameSpaces = new List<CodeNamespace>();
				GetAllNamespaces(cmNamespace, ref nameSpaces);
				foreach (CodeNamespace nameSpace in nameSpaces)
				{
					AddNamespace(parentNode, nameSpace);
				}
			}
			else
			{
				AddNamespace(parentNode, cmNamespace);
			}
		}

		private void GetAllNamespaces(CodeNamespace cmNamespace, ref List<CodeNamespace> nameSpaces)
		{
			bool insert = false;
			foreach (CodeElement codeElement in cmNamespace.Members)
			{
				if (codeElement is CodeNamespace)
				{
					CodeNamespace subSpace = (CodeNamespace)codeElement;
					GetAllNamespaces(subSpace, ref nameSpaces);
				}
				else
				{
					// If the namespace constains other things other than just namespaces then inserted
					insert = true;
				}
			}
			if (insert)
			{
				nameSpaces.Add(cmNamespace);
			}
		}

		private const string WebProjectKind = "{E24C65DC-7377-472B-9ABA-BC803B73C61A}";

		private void Expand(TreeNode parentNode, ProjectItem prItem)
		{
			ProjectItems childItems = prItem.ProjectItems;
			if (childItems != null)
			{
				foreach (ProjectItem subItem in childItems)
				{
					Expand(parentNode, subItem);
				}
			}
			if (prItem.FileCodeModel != null)
			{
				foreach (CodeElement codeElement in prItem.FileCodeModel.CodeElements)
				{
					/*if (codeElement.InfoLocation != vsCMInfoLocation.vsCMInfoLocationProject)
					{
							continue;
					}*/
					BeforeExpand(parentNode, codeElement);
				}
			}
		}

		private void Expand(TreeNode parentNode, Project prj)
		{
			if (_onlyUserCode)
			{
				foreach (ProjectItem prItem in prj.ProjectItems)
				{
					Expand(parentNode, prItem);
				}
			}
			else
			{
				foreach (CodeElement codeElement in prj.CodeModel.CodeElements)
				{
					BeforeExpand(parentNode, codeElement);
				}
			}
		}

		private void Expand(TreeNode parentNode, List<CodeNamespace> namepaces)
		{
			foreach (CodeNamespace codeNamepace in namepaces)
			{
				foreach (CodeElement codeElement in codeNamepace.Members)
				{
					BeforeExpand(parentNode, codeElement);
				}
			}
		}

		private void Expand(TreeNode parentNode, CodeFunction codeFunction)
		{
			foreach (CodeElement codeElement in codeFunction.Parameters)
			{
				BeforeExpand(parentNode, codeElement);
			}
		}

		private void Expand(TreeNode parentNode, CodeClass codeClass)
		{
			foreach (CodeElement codeElement in codeClass.Members)
			{
				BeforeExpand(parentNode, codeElement);
			}
		}

		private void BeforeExpand(TreeNode parentNode, CodeVariable codeVariable)
		{
			MemberNode cv = new MemberNode(codeVariable, this);
			parentNode.Nodes.Add(cv);
		}

		private void BeforeExpand(TreeNode parentNode, CodeProperty codeProperty)
		{
			PropertyNode cp = new PropertyNode(codeProperty, this);
			parentNode.Nodes.Add(cp);
		}

		private void BeforeExpand(TreeNode parentNode, CodeEvent codeEvent)
		{
			EventNode ce = new EventNode(codeEvent, this);
			parentNode.Nodes.Add(ce);
		}

		private void BeforeExpand(TreeNode parentNode, CodeFunction codeFunction)
		{
			MethodNode cn = new MethodNode(codeFunction, this);
			parentNode.Nodes.Add(cn);
		}

		private void BeforeExpand(TreeNode parentNode, CodeClass codeClass)
		{
			ClassNode cn = new ClassNode(codeClass, this);
			CodeElements children = GetChilds((CodeElement)codeClass);
			if (children != null && children.Count > 0)
			{
				AddNodeWithChilds(parentNode.Nodes, cn);
			}
			else
			{
				parentNode.Nodes.Add(cn);
			}
		}

		private void BeforeExpand(TreeNode parentNode, CodeInterface codeInterface)
		{
			InterfaceNode cn = new InterfaceNode(codeInterface, this);
			if (codeInterface.Members.Count > 0)
			{
				AddNodeWithChilds(parentNode.Nodes, cn);
			}
			else
			{
				parentNode.Nodes.Add(cn);
			}
		}

		private void BeforeExpand(TreeNode parentNode, CodeEnum codeEnum)
		{
			EnumNode cn = new EnumNode(codeEnum, this);
			if (codeEnum.Members.Count > 0)
			{
				AddNodeWithChilds(parentNode.Nodes, cn);
			}
			else
			{
				parentNode.Nodes.Add(cn);
			}
		}

		private void BeforeExpand(TreeNode parentNode, CodeDelegate codeDelegate)
		{
			DelegateNode dn = new DelegateNode(codeDelegate, this);
			parentNode.Nodes.Add(dn);
		}

		bool CanBrowse(CodeModelEditor.BrowseKind kind)
		{
			if (kind == CodeModelEditor.BrowseKind.Namespace)
			{
				return true;
			}
			return ((this._kind & kind) != 0);
		}

		private static CodeModelEditor.BrowseKind GetBrowseKind(CodeElement codeElement)
		{
			if (codeElement is CodeClass)
			{
				return CodeModelEditor.BrowseKind.Class;
			}
			else if (codeElement is CodeInterface)
			{
				return CodeModelEditor.BrowseKind.Interface;
			}
			else if (codeElement is CodeEnum)
			{
				return CodeModelEditor.BrowseKind.Enum;
			}
			else if (codeElement is CodeDelegate)
			{
				return CodeModelEditor.BrowseKind.Delegate;
			}
			else if (codeElement is CodeFunction)
			{
				return CodeModelEditor.BrowseKind.Function;
			}
			else if (codeElement is CodeVariable)
			{
				return CodeModelEditor.BrowseKind.Variable;
			}
			else if (codeElement is CodeEvent)
			{
				return CodeModelEditor.BrowseKind.Event;
			}
			else if (codeElement is CodeProperty)
			{
				return CodeModelEditor.BrowseKind.Prop;
			}
			else if (codeElement is CodeNamespace)
			{
				return CodeModelEditor.BrowseKind.Namespace;
			}
			return CodeModelEditor.BrowseKind.None;
		}

		private void BeforeExpand(TreeNode parentNode, CodeElement codeElement)
		{
			if (Filter(codeElement))
			{
				return;
			}
			if (codeElement is CodeClass)
			{
				BeforeExpand(parentNode, (CodeClass)codeElement);
			}
			else if (codeElement is CodeInterface)
			{
				BeforeExpand(parentNode, (CodeInterface)codeElement);
			}
			else if (codeElement is CodeEnum)
			{
				BeforeExpand(parentNode, (CodeEnum)codeElement);
			}
			else if (codeElement is CodeDelegate)
			{
				BeforeExpand(parentNode, (CodeDelegate)codeElement);
			}
			else if (codeElement is CodeFunction)
			{
				BeforeExpand(parentNode, (CodeFunction)codeElement);
			}
			else if (codeElement is CodeVariable)
			{
				BeforeExpand(parentNode, (CodeVariable)codeElement);
			}
			else if (codeElement is CodeEvent)
			{
				BeforeExpand(parentNode, (CodeEvent)codeElement);
			}
			else if (codeElement is CodeProperty)
			{
				BeforeExpand(parentNode, (CodeProperty)codeElement);
			}
			else if (codeElement is CodeNamespace)
			{
				BeforeExpand(parentNode, (CodeNamespace)codeElement);
			}
		}

		private void OnBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (!HasToCreateChilds(e.Node))
			{
				return;
			}
			_tvBrowser.SuspendLayout();
			try
			{
				this.Cursor = Cursors.WaitCursor;
				// Clear the previous "mock" node
				TreeNode parentNode = e.Node;
				parentNode.Nodes.Clear();
				object tag = e.Node.Tag;
				if (tag == null)
				{
					return;
				}
				else if (tag is Project)
				{
					Expand(parentNode, (Project)tag);
				}
				else if (tag is List<CodeNamespace>)
				{
					Expand(parentNode, (List<CodeNamespace>)tag);
				}
				else if (tag is CodeClass)
				{
					Expand(parentNode, (CodeClass)tag);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
				_tvBrowser.ResumeLayout();
			}
		}

		private void tvBrowser_DoubleClick(object sender, EventArgs e)
		{
			this._btnOK.PerformClick();
		}

		private void flattenNameSpaces_CheckedChanged(object sender, EventArgs e)
		{
			LoadRootNodes();
		}

		private void OnAfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node is CodeModelNode && ((CodeModelNode)e.Node).Kind == _kind)
			{
				this._btnOK.Enabled = true;
			}
			else
			{
				this._btnOK.Enabled = false;
			}
		}

		#endregion Event handlers

		#region Properties

		/// <summary>
		/// Exposes the selected class.
		/// </summary>
		internal object SelectedObject
		{
			get
			{
				if (_tvBrowser.SelectedNode == null ||
						 _tvBrowser.SelectedNode.Tag == null)
				{
					return null;
				}
				TreeNode selectedNode = _tvBrowser.SelectedNode;
				if (selectedNode is NamespaceNode)
				{
					List<CodeNamespace> codeNamespaces = (List<CodeNamespace>)selectedNode.Tag;
					return codeNamespaces[0];
				}
				else
				{
					return selectedNode.Tag;
				}
			}
		}

		#endregion Properties

		#region Derived nodes

		class ProjectNode : TreeNode
		{
			public ProjectNode(Project project)
			{
				try
				{
					base.Text = project.Properties.Item("AssemblyName").Value.ToString();
				}
				catch
				{
					base.Text = project.Name;
				}
				base.Tag = project;
				base.ImageIndex = 0;
				base.SelectedImageIndex = 0;
			}
		}

		class CodeModelNode : TreeNode
		{
			CodeModelEditorForm parentForm = null;

			public CodeModelNode(CodeModelEditor.BrowseKind kind, CodeModelEditorForm parentForm)
			{
				this.kind = kind;
				this.parentForm = parentForm;
			}

			public CodeModelEditor.BrowseKind Kind
			{
				get { return kind; }
			} CodeModelEditor.BrowseKind kind;

			protected void SetText(string text)
			{
				string newName = null;
				if (this.Kind != CodeModelEditor.BrowseKind.Namespace)
				{
					try
					{
						TypeConverter typeConverter = null;
						if (parentForm._context.Instance is ValueEditor)
						{
							typeConverter = ((ValueEditor)parentForm._context.Instance).ConverterInstance;
						}
						if (typeConverter != null)
						{
							newName = (string)typeConverter.ConvertTo(
									parentForm._context,
									CultureInfo.CurrentCulture,
									this.Tag,
									typeof(string));
						}
					}
					catch
					{
						newName = text;
					}
				}
				if (string.IsNullOrEmpty(newName))
				{
					newName = text;
				}
				if (!string.IsNullOrEmpty(newName))
				{
					this.Text = newName;
				}
			}
		}

		class NamespaceNode : CodeModelNode
		{
			public NamespaceNode(string Name, CodeModelEditorForm context)
				: base(CodeModelEditor.BrowseKind.Namespace, context)
			{
				base.Name = Name;
				SetText(Name);
				base.Tag = new List<CodeNamespace>();
				base.ImageKey = "Namespace";
				base.SelectedImageKey = "Namespace";
			}
		}

		class ClassNode : CodeModelNode
		{
			public ClassNode(CodeClass cc, CodeModelEditorForm context)
				: base(CodeModelEditor.BrowseKind.Class, context)
			{
				base.Tag = cc;
				base.ImageKey = "Class";
				base.SelectedImageKey = "Class";
				SetText(cc.Name);
			}
		}

		class FunctionNode : CodeModelNode
		{
			public FunctionNode(CodeFunction cf, CodeModelEditor.BrowseKind kind, CodeModelEditorForm context)
				: base(kind | CodeModelEditor.BrowseKind.Function, context)
			{
				StringBuilder sb = new StringBuilder();
				if (cf.FunctionKind == vsCMFunction.vsCMFunctionConstructor)
				{
					sb.Append(".ctor");
				}
				else if (cf.FunctionKind == vsCMFunction.vsCMFunctionDestructor)
				{
					sb.Append(".dtor");
				}
				else
				{
					sb.Append(cf.Name);
				}
				sb.Append("(");
				int iParam = 0;
				foreach (CodeParameter codeParameter in cf.Parameters)
				{
					iParam++;
					sb.Append(codeParameter.Type.AsString);
					if (iParam != cf.Parameters.Count)
					{
						sb.Append(",");
					}
				}
				sb.Append(")");
				base.Text = sb.ToString();
				base.Tag = cf;
				base.ImageKey = "Method";
				base.SelectedImageKey = "Method";
			}
		}

		class MethodNode : FunctionNode
		{
			public MethodNode(CodeFunction cf, CodeModelEditorForm context)
				: base(cf, CodeModelEditor.BrowseKind.Class, context)
			{
			}
		}

		class InterfaceNode : CodeModelNode
		{
			public InterfaceNode(CodeInterface ci, CodeModelEditorForm context)
				: base(CodeModelEditor.BrowseKind.Interface, context)
			{
				base.Tag = ci;
				base.ImageKey = "Interface";
				base.SelectedImageKey = "Interface";
				SetText(ci.Name);
			}
		}

		class EnumNode : CodeModelNode
		{
			public EnumNode(CodeEnum ce, CodeModelEditorForm context)
				: base(CodeModelEditor.BrowseKind.Enum, context)
			{
				base.Tag = ce;
				base.ImageKey = "Enum";
				base.SelectedImageKey = "Enum";
				SetText(ce.Name);
			}
		}

		class DelegateNode : CodeModelNode
		{
			public DelegateNode(CodeDelegate cd, CodeModelEditorForm context)
				: base(CodeModelEditor.BrowseKind.Delegate, context)
			{
				base.Tag = cd;
				base.ImageKey = "Delegate";
				base.SelectedImageKey = "Delegate";
				SetText(cd.Name);
			}
		}

		class MemberNode : CodeModelNode
		{
			public MemberNode(CodeVariable cv, CodeModelEditorForm context)
				: base(CodeModelEditor.BrowseKind.ClassMember, context)
			{
				base.Tag = cv;
				base.ImageKey = "Member";
				base.SelectedImageKey = "Member";
				SetText(cv.Name);
			}
		}

		class EventNode : CodeModelNode
		{
			public EventNode(CodeEvent ce, CodeModelEditorForm context)
				: base(CodeModelEditor.BrowseKind.ClassEvent, context)
			{
				base.Tag = ce;
				base.ImageKey = "Event";
				base.SelectedImageKey = "Event";
				SetText(ce.Name);
			}
		}

		class PropertyNode : CodeModelNode
		{
			public PropertyNode(CodeProperty cp, CodeModelEditorForm context)
				: base(CodeModelEditor.BrowseKind.ClassProperty, context)
			{
				base.Tag = cp;
				base.ImageKey = "Property";
				base.SelectedImageKey = "Property";
				SetText(cp.Name);
			}
		}

		#endregion Derived nodes

	}
}
