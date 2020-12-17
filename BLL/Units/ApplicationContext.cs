using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Tomato.NewTempProject.BLL
{
    public class ApplicationContext : IDisposable
    {
        public UnityContainer UnityContainer { get; private set; }

        public string UserName
        {
            get
            {
                //return HttpContext.Current.Session["UserName"].ToString();
                return "testUser";
            }
        }

        public Guid UserId
        {
            get
            {
                return new Guid(HttpContext.Current.Session["UserId"].ToString());
            }
        }

        public List<string> ApiList
        {
            get
            {
                return HttpContext.Current.Session["apiList"] as List<string>;
            }
        }

        private ApplicationContext()
        {
            this.UnityContainer = new UnityContainer();
            var ucs = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            if (ucs != null)
            {
                var defaultContainer = ucs.Containers["defaultContainer"];
                if (defaultContainer != null)
                {
                    ucs.Configure(this.UnityContainer, "defaultContainer");
                }
            }
        }

        private static readonly object _syncRoot = new object();
        private static object SyncRoot
        {
            get { return _syncRoot; }
        }

        private static readonly string domainPropertyName = typeof(ApplicationContext).AssemblyQualifiedName;

        public static ApplicationContext Current
        {
            get
            {
                var current = (ApplicationContext)AppDomain.CurrentDomain.GetData(domainPropertyName);
                if (current == null)
                    lock (ApplicationContext.SyncRoot)
                    {
                        current = (ApplicationContext)AppDomain.CurrentDomain.GetData(domainPropertyName);
                        if (current == null)
                            AppDomain.CurrentDomain.SetData(domainPropertyName, current = new ApplicationContext());
                    }

                return current;
            }
        }

        #region IDisposable Members

        private bool Disposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    this.UnityContainer.Dispose();
                }

                // Release unmanaged resources here.
            }
        }

        ~ApplicationContext()
        {
            Dispose(false);
        }

        #endregion
    }
}
