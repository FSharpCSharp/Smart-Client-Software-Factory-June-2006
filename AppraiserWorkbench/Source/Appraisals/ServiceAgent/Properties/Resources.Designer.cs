﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GlobalBank.AppraiserWorkbench.AppraisalServiceAgent.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot load the appraisal {0}. The file does not exists..
        /// </summary>
        internal static string AppraisalDoesNotExist {
            get {
                return ResourceManager.GetString("AppraisalDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Appraisal &quot;{0}&quot; has been assigned to you..
        /// </summary>
        internal static string AuditAssigmentGranted {
            get {
                return ResourceManager.GetString("AuditAssigmentGranted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service denied assigned the Appraisal &quot;{0}&quot; to you..
        /// </summary>
        internal static string AuditAssignmentRejected {
            get {
                return ResourceManager.GetString("AuditAssignmentRejected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You request the Appraisal &quot;{0}&quot; to be assigned to you..
        /// </summary>
        internal static string AuditAssignmentRequest {
            get {
                return ResourceManager.GetString("AuditAssignmentRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Appraisal &quot;{0}&quot; has been released..
        /// </summary>
        internal static string AuditReleased {
            get {
                return ResourceManager.GetString("AuditReleased", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You submitted the Appraisal &quot;{0}&quot;..
        /// </summary>
        internal static string AuditSubmitted {
            get {
                return ResourceManager.GetString("AuditSubmitted", resourceCulture);
            }
        }
    }
}
