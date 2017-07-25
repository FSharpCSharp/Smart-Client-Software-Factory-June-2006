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

using System.Collections.Generic;

namespace Microsoft.Practices.GuidanceAutomation.SmartClient.Library
{
	/// <summary>
	/// An ComObject that is capable if dynamic invocation using the IDispatch interface
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DispatchObject<T>
	{
		#region Private Fields and Constructor

		private T _realObject;
		private Dictionary<string, int> _dispIDCache;

		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="realObject"></param>
		public DispatchObject(T realObject)
		{
			this._realObject = realObject;
			this._dispIDCache = new Dictionary<string, int>();
		}

		#endregion

		#region Private Implementation

		private int GetDISPID(string memberName)
		{
			if (!_dispIDCache.ContainsKey(memberName))
			{
				int dispID = -1;
				NativeMethods.HasDISPID(_realObject, memberName, out dispID);
				_dispIDCache.Add(memberName, dispID);
			}
			return _dispIDCache[memberName];
		}

		private object GetProperty(string property)
		{
			int dispID = GetDISPID(property);
			if (dispID != -1)
			{
				return NativeMethods.InvokeGetProperty(_realObject, dispID, new object[] { });
			}
			return null;
		}

		private void SetProperty(string property, object value)
		{
			NativeMethods.InvokeSetProperty(_realObject, GetDISPID(property), new object[] { value });
		}

		#endregion

		/// <summary>
		/// The real COM object 
		/// </summary>
		public T RealObject
		{
			get
			{
				return this._realObject;
			}
		}

		/// <summary>
		/// Public property accesor
		/// </summary>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public object this[string propertyName]
		{
			get
			{
				return GetProperty(propertyName);
			}
			set
			{
				SetProperty(propertyName, value);
			}
		}

		/// <summary>
		/// Invokes the function <paramref name="funcName"/> using IDispatch.Invoke 
		/// </summary>
		/// <param name="funcName"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public object Invoke(string funcName, object[] parameters)
		{
			return NativeMethods.InvokeFunc(this._realObject, GetDISPID(funcName), parameters);
		}

		/// <summary>
		/// <see cref="Invoke(string,object[])"/>
		/// </summary>
		/// <param name="funcName"></param>
		/// <returns></returns>
		public object Invoke(string funcName)
		{
			return Invoke(funcName, new object[] { });
		}

		/// <summary>
		/// <see cref="Invoke(string,object[])"/>
		/// </summary>
		/// <param name="funcName"></param>
		/// <param name="param1"></param>
		/// <returns></returns>
		public object Invoke(string funcName, object param1)
		{
			return Invoke(funcName, new object[] { param1 });
		}

		/// <summary>
		/// <see cref="Invoke(string,object[])"/>
		/// </summary>
		/// <param name="funcName"></param>
		/// <param name="param1"></param>
		/// <param name="param2"></param>
		/// <returns></returns>
		public object Invoke(string funcName, object param1, object param2)
		{
			return Invoke(funcName, new object[] { param1, param2 });
		}

		/// <summary>
		/// <see cref="Invoke(string,object[])"/>
		/// </summary>
		/// <param name="funcName"></param>
		/// <param name="param1"></param>
		/// <param name="param2"></param>
		/// <param name="param3"></param>
		/// <returns></returns>
		public object Invoke(string funcName, object param1, object param2, object param3)
		{
			return Invoke(funcName, new object[] { param1, param2, param3 });
		}

		/// <summary>
		/// Implicit cast operator
		/// </summary>
		/// <param name="realObject"></param>
		/// <returns></returns>
		public static implicit operator DispatchObject<T>(T realObject)
		{
			return new DispatchObject<T>(realObject);
		}

		/// <summary>
		/// Implicit cast operator
		/// </summary>
		/// <param name="dispatchObject"></param>
		/// <returns></returns>
		public static implicit operator T(DispatchObject<T> dispatchObject)
		{
			return dispatchObject._realObject;
		}
	}
}
