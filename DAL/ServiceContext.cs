using System;
using System.Configuration;

namespace Tomato.NewTempProject.DAL
{
	public class ServiceContext : IDisposable
	{
        public string UserName
        {
            get
            {
                //return HttpContext.Current.Session["UserName"].ToString();
                return "TestUser";//TO DO
            }
        }

		private ServiceContext()
		{
        }

		private static readonly object _syncRoot = new object();
		private static object SyncRoot
		{
			get { return _syncRoot; }
		}

		private static readonly string domainPropertyName = typeof(ServiceContext).AssemblyQualifiedName;

		public static ServiceContext Current
		{
			get
			{
				var current = (ServiceContext)AppDomain.CurrentDomain.GetData(domainPropertyName);
				if(current == null)
					lock(ServiceContext.SyncRoot)
					{
						current = (ServiceContext)AppDomain.CurrentDomain.GetData(domainPropertyName);
						if(current == null)
							AppDomain.CurrentDomain.SetData(domainPropertyName, current = new ServiceContext());
					}

				return current;
			}
		}

		#region IDisposable Members
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
		#endregion
	}
}
