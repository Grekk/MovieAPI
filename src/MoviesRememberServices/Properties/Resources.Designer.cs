﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MoviesRememberServices.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MoviesRememberServices.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to http://api.allocine.fr/rest/v3/.
        /// </summary>
        internal static string API_URL {
            get {
                return ResourceManager.GetString("API_URL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.allocine.fr/rest/v3/movie?partner=YW5kcm9pZC12M3M&amp;profile=large&amp;format=json.
        /// </summary>
        internal static string DISPLAY_MOVIE_URL {
            get {
                return ResourceManager.GetString("DISPLAY_MOVIE_URL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.allocine.fr/rest/v3/movielist?partner=YW5kcm9pZC12M3M&amp;count=24&amp;filter=comingsoon&amp;order=dateasc&amp;format=json&amp;page=.
        /// </summary>
        internal static string ORDER_BY_DATE_MOVIES_COMING_SOON {
            get {
                return ResourceManager.GetString("ORDER_BY_DATE_MOVIES_COMING_SOON", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.allocine.fr/rest/v3/movielist?partner=YW5kcm9pZC12M3M&amp;count=24&amp;filter=nowshowing&amp;&amp;order=datedesc&amp;format=json&amp;page=.
        /// </summary>
        internal static string ORDER_BY_DATE_MOVIES_NOW_SHOWING {
            get {
                return ResourceManager.GetString("ORDER_BY_DATE_MOVIES_NOW_SHOWING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.allocine.fr/rest/v3/search?partner=YW5kcm9pZC12M3M&amp;filter=movie,theater,person,news,tvseries&amp;count=5&amp;page=1&amp;format=json&amp;q=.
        /// </summary>
        internal static string SEARCH_MOVIE_URL {
            get {
                return ResourceManager.GetString("SEARCH_MOVIE_URL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.allocine.fr/rest/v3/movielist?partner=YW5kcm9pZC12M3M&amp;count=24&amp;filter=comingsoon&amp;order=toprank&amp;format=json&amp;page=.
        /// </summary>
        internal static string TOP_RANKED_MOVIES_COMING_SOON {
            get {
                return ResourceManager.GetString("TOP_RANKED_MOVIES_COMING_SOON", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.allocine.fr/rest/v3/movielist?partner=YW5kcm9pZC12M3M&amp;count=24&amp;filter=nowshowing&amp;order=toprank&amp;format=json&amp;page=.
        /// </summary>
        internal static string TOP_RANKED_MOVIES_NOW_SHOWING {
            get {
                return ResourceManager.GetString("TOP_RANKED_MOVIES_NOW_SHOWING", resourceCulture);
            }
        }
    }
}