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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Services.Description;
using EnvDTE;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.GuidanceAutomation.SmartClient.Library;
using Microsoft.Practices.RecipeFramework.Services;
using VSLangProj80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Actions
{
	public class AddAsyncWebServiceProxyAction : ConfigurableAction
	{
		private string _webServiceUrl;
		private string _serviceName;

		public override void Execute()
		{
			IVsAddWebReferenceDlg2 dlg = GetService<IVsAddWebReferenceDlg>() as IVsAddWebReferenceDlg2;
			System.Diagnostics.Trace.Assert(dlg != null);

			IDiscoveryResult discoveryResult = null;
			int fCanceled = 0;
			int hResult = dlg.AddWebReferenceDlg(null, out _webServiceUrl, out _serviceName, out discoveryResult, out fCanceled);

			if (!ErrorHandler.Succeeded(hResult) || fCanceled != 0)
				return;

			Project project = GetSelectedProject();
			ProjectItem webReferenceItem = CreateWebReference(project);
			ProjectItem wsdlItem = GetWsdlItem(webReferenceItem);

			Debug.Assert(wsdlItem != null);

			ServiceDescription wsdl = ReadWsdlFile(wsdlItem);
			List<string> operationNames = GetWsdlOperations(wsdl);

			string rootNamespace = GetGlobalValue("RootNamespace");
			string projectNamespace = project.Properties.Item("DefaultNamespace").Value.ToString();
			string nameSpace = String.Format("{0}.{1}", projectNamespace, _serviceName);

			ProjectItem mainFolder = project.ProjectItems.AddFolder(_serviceName, EnvDTE.Constants.vsProjectItemKindPhysicalFolder);
			mainFolder.ProjectItems.AddFolder("Commands", EnvDTE.Constants.vsProjectItemKindPhysicalFolder);

			foreach (Service service in wsdl.Services)
			{
				string simpleClassName = service.Name;
				CodeType generatedProxyCodeType = project.CodeModel.CodeTypeFromFullName(String.Format("{0}.{1}", nameSpace, simpleClassName));

				List<CodeFunction> serviceMethods = GetServiceMethods(operationNames, generatedProxyCodeType);

				if (serviceMethods.Count > 0)
				{
					Dictionary<string, object> args = new Dictionary<string, object>();
					T4TemplateProcessor processor = new T4TemplateProcessor(GetService<DTE>(), GetService<IConfigurationService>());

					args.Clear();
					args["ClassName"] = String.Format("I{0}Proxy", simpleClassName);
					args["Namespace"] = nameSpace;
					args["Methods"] = GetMethodsArgument(serviceMethods);
					processor.ProcessTemplateToFile(@"Templates\Text\WebServiceProxy\IServiceProxy.t4", GenerateFilename(args), args);

					args.Clear();
					args["ClassName"] = String.Format("I{0}", simpleClassName);
					args["Namespace"] = nameSpace;
					args["Methods"] = GetMethodsArgument(serviceMethods);
					args["RootNamespace"] = rootNamespace;
					processor.ProcessTemplateToFile(@"Templates\Text\WebServiceProxy\IServiceWrapper.t4", GenerateFilename(args), args);

					args.Clear();
					args["ClassName"] = String.Format("{0}Proxy", simpleClassName);
					args["ClassInterface"] = String.Format("I{0}Proxy", simpleClassName);
					args["WsdlGeneratedService"] = String.Format("{0}", simpleClassName);
					args["WsdlGeneratedServiceInterface"] = String.Format("I{0}", simpleClassName);
					args["Namespace"] = nameSpace;
					args["ProjectNamespace"] = projectNamespace;
					args["Methods"] = GetMethodsArgument(serviceMethods);
					args["RootNamespace"] = rootNamespace;
					processor.ProcessTemplateToFile(@"Templates\Text\WebServiceProxy\ServiceProxy.t4", GenerateFilename(args), args);

					args.Clear();
					args["ClassName"] = String.Format("{0}", simpleClassName);
					args["ClassInterface"] = String.Format("I{0}", simpleClassName);
					args["Namespace"] = nameSpace;
					args["RootNamespace"] = rootNamespace;
					processor.ProcessTemplateToFile(@"Templates\Text\WebServiceProxy\ServiceWrapper.t4", GenerateFilename(args), args);

					foreach (CodeFunction function in serviceMethods)
					{
						args.Clear();
						args["ClassName"] = String.Format("{0}Command", function.Name);
						args["Namespace"] = String.Format("{0}.Commands", nameSpace);
						args["ServiceMethod"] = CodeFunctionToDictionary(function);
						args["FullyQualifiedServiceInterface"] = String.Format("{0}.I{1}", nameSpace, simpleClassName);
						args["RootNamespace"] = rootNamespace;

						if (function.Type.AsString == "void")
							processor.ProcessTemplateToFile(@"Templates\Text\WebServiceProxy\Command.t4", GenerateCommandFilename(args), args);
						else
							processor.ProcessTemplateToFile(@"Templates\Text\WebServiceProxy\CommandWithCallback.t4", GenerateCommandFilename(args), args);
					}
				}
			}
		}

		private string GetGlobalValue(string valueName)
		{
			DTE dte = GetService<DTE>();
			if (dte.Solution.Globals.get_VariableExists(valueName))
			{
				return (string)dte.Solution.Globals[valueName];
			}
			throw new InvalidOperationException("Global value not found");
		}

		private IDictionary<string, object> CodeFunctionToDictionary(CodeFunction function)
		{
			Dictionary<string, object> result = new Dictionary<string, object>();
			IList<KeyValuePair<string, string>> parms = new List<KeyValuePair<string, string>>();

			result["ReturnType"] = function.Type.AsString;
			result["Name"] = function.Name;
			result["Parameters"] = parms;

			foreach (CodeParameter param in function.Parameters)
				parms.Add(new KeyValuePair<string, string>(param.Type.AsString, param.Name));

			return result;
		}

		private string GenerateFilename(IDictionary<string, object> args)
		{
			string className = args["ClassName"].ToString();
			return Path.Combine(Path.Combine(Path.GetDirectoryName(GetSelectedProject().FileName), _serviceName), className + ".cs");
		}

		private string GenerateCommandFilename(IDictionary<string, object> args)
		{
			string className = args["ClassName"].ToString();
			return Path.Combine(Path.Combine(Path.GetDirectoryName(GetSelectedProject().FileName), _serviceName + @"\Commands"), className + ".cs");
		}

		private static List<CodeFunction> GetServiceMethods(List<string> operationNames, CodeType generatedProxyCodeType)
		{
			List<CodeFunction> serviceMethods = new List<CodeFunction>();

			foreach (CodeElement memberElement in generatedProxyCodeType.Members)
				if (memberElement.Kind == vsCMElement.vsCMElementFunction && operationNames.Contains(memberElement.Name))
					serviceMethods.Add((CodeFunction)memberElement);

			return serviceMethods;
		}

		private static ServiceDescription ReadWsdlFile(ProjectItem wsdlItem)
		{
			return ServiceDescription.Read(wsdlItem.get_FileNames(0));
		}

		private static List<string> GetWsdlOperations(ServiceDescription wsdl)
		{
			List<string> operationNames = new List<string>();

			foreach (PortType type in wsdl.PortTypes)
				foreach (Operation oper in type.Operations)
					operationNames.Add(oper.Name);

			return operationNames;
		}

		private static ProjectItem GetWsdlItem(ProjectItem webReferenceItem)
		{
			foreach (ProjectItem item in webReferenceItem.ProjectItems)
				if (item.Name.ToLowerInvariant().EndsWith(".wsdl"))
					return item;

			return null;
		}

		private ProjectItem CreateWebReference(Project project)
		{
			ProjectItem webReferenceItem = ((VSProject2)project.Object).AddWebReference(_webServiceUrl);
			webReferenceItem.Name = _serviceName;
			return webReferenceItem;
		}

		private Project GetSelectedProject()
		{
			return DteHelper.GetSelectedProject(GetService<DTE>());
		}

		private IList<IDictionary<string, object>> GetMethodsArgument(IList<CodeFunction> serviceMethods)
		{
			List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();

			foreach (CodeFunction func in serviceMethods)
				result.Add(CodeFunctionToDictionary(func));

			return result;
		}

		private List<CodeClass> FindAllClasses(CodeElements codeElements, string desiredBaseFullName)
		{
			List<CodeClass> results = new List<CodeClass>();

			foreach (CodeElement element in codeElements)
			{
				if (element.Kind == vsCMElement.vsCMElementClass)
				{
					CodeClass klass = (CodeClass)element;

					foreach (CodeElement baseClass in klass.Bases)
						if (baseClass.FullName == desiredBaseFullName)
							results.Add(klass);
				}

				results.AddRange(FindAllClasses(element.Children, desiredBaseFullName));
			}

			return results;
		}

		public override void Undo()
		{
		}
	}
}
