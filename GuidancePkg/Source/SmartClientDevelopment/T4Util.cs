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
using System.Text;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TextTemplating;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient
{
	public static class T4Util
	{
		private enum Location
		{
			Server,
			Client
		};

		public static void WriteDelegateDecl(TextTransformation template,CodeFunction codeFunction)
		{
			template.WriteLine("		public delegate void {0}(bool success{1});", codeFunction.Name, GetReturnTypeAsParam(codeFunction,Location.Client));
		}

		private static string GetReturnTypeAsParam(CodeFunction codeFunction,Location location)
		{
			string extraParameter = string.Empty;
			if (codeFunction.Type.TypeKind != vsCMTypeRef.vsCMTypeRefVoid)
			{
				extraParameter = "," + GetTypeName(codeFunction.Type,location) + " returnValue";
			}
			return extraParameter;
		}

		private static string GetReturnType(CodeFunction codeFunction)
		{
			string extraParameter = string.Empty;
			if (codeFunction.Type.TypeKind != vsCMTypeRef.vsCMTypeRefVoid)
			{
				extraParameter = ",FromServerToClient(returnValue)";
			}
			return extraParameter;
		}

		private static string GetParams(CodeFunction codeFunction)
		{
			string parameters = string.Empty;
			if (codeFunction.Parameters.Count > 0)
			{
				foreach (CodeParameter param in codeFunction.Parameters)
				{
					parameters += ",FromClientToServer(" + param.Name + ")";
				}
			}
			return parameters;
		}

		private static string GetTypeName(CodeTypeRef codeType,Location location)
		{
			if (location == Location.Client)
			{
				int iDot = codeType.AsString.LastIndexOf('.');
				if (iDot != -1)
				{
					return codeType.AsString.Substring(iDot+1);
				}
			}
			return codeType.AsString;
		}

		private static string GetParamsDecl(CodeFunction codeFunction)
		{
			string parameters = string.Empty;
			if (codeFunction.Parameters.Count>0)
			{
				foreach (CodeParameter param in codeFunction.Parameters)
				{
					parameters += GetTypeName(param.Type,Location.Client) + " " + param.Name + ",";
				}
			}
			return parameters;
		}

		public static void WriteInterfaceDecl(TextTransformation template,CodeFunction codeFunction,string prefix)
		{
			template.WriteLine("	  void {0}({1}{2}Callbacks.{0} callback);", codeFunction.Name, GetParamsDecl(codeFunction), prefix);
		}

		public static void WriteInterfaceImpl(TextTransformation template, CodeFunction codeFunction,string prefix)
		{
			template.WriteLine("		public void {0}({1}{2}Callbacks.{0} callback)", codeFunction.Name, GetParamsDecl(codeFunction), prefix);
			template.WriteLine("		{");
			if (codeFunction.Type.TypeKind == vsCMTypeRef.vsCMTypeRefVoid )
			{
				template.WriteLine("			_serviceProxy.{0}(TIMEOUT{1});", codeFunction.Name, GetParams(codeFunction));
			}
			else
			{
				template.WriteLine("			_serviceProxy.{0}(TIMEOUT{2},delegate(bool success{1})", codeFunction.Name, GetReturnTypeAsParam(codeFunction,Location.Server),GetParams(codeFunction));
				template.WriteLine("			{");
				template.WriteLine("				callback(success{0});", GetReturnType(codeFunction));
				template.WriteLine("			});");
			}
			template.WriteLine("		}");
			template.WriteLine(string.Empty);
		}
	}

}
