﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IdentityService.Application.Resources {
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
    internal class ErrorMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IdentityService.Application.Resources.ErrorMessage", typeof(ErrorMessage).Assembly);
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
        ///   Looks up a localized string similar to Такой Email уже существует.
        /// </summary>
        internal static string EmailAlreadyExist {
            get {
                return ResourceManager.GetString("EmailAlreadyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Такого Email нет.
        /// </summary>
        internal static string EmailNotExist {
            get {
                return ResourceManager.GetString("EmailNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Неверный пароль.
        /// </summary>
        internal static string InvalidPassword {
            get {
                return ResourceManager.GetString("InvalidPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Недопустимый токен обновления.
        /// </summary>
        internal static string InvalidRefreshToken {
            get {
                return ResourceManager.GetString("InvalidRefreshToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Такого user нет.
        /// </summary>
        internal static string UserNotExist {
            get {
                return ResourceManager.GetString("UserNotExist", resourceCulture);
            }
        }
    }
}
