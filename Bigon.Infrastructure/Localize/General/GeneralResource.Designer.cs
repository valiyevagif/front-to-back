﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bigon.Infrastructure.Localize.General {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class GeneralResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GeneralResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Bigon.Infrastructure.Localize.General.GeneralResource", typeof(GeneralResource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Qeyd olunan məlumatlar standartlara cavab vermir.
        /// </summary>
        public static string BAD_DATA {
            get {
                return ResourceManager.GetString("BAD_DATA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Müddəti keçmiş token.
        /// </summary>
        public static string EXPIRED_TOKEN {
            get {
                return ResourceManager.GetString("EXPIRED_TOKEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Səlahiyyət çatışmazlığı.
        /// </summary>
        public static string INSUFFICIENT_PERMISSION {
            get {
                return ResourceManager.GetString("INSUFFICIENT_PERMISSION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Qeyd mövcud deyil.
        /// </summary>
        public static string RECORD_NOT_FOUND {
            get {
                return ResourceManager.GetString("RECORD_NOT_FOUND", resourceCulture);
            }
        }
    }
}