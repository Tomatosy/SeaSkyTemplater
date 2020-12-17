using Tomato.StandardLib.MyModel;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
	public interface IDALPageBaseNew<T, OutputT, ViewT> where T : BasePageModel, new()where OutputT : class, T, new()where ViewT : class, T, new()
	{
		PageModel<OutputT> ListPage(T model);

		PageModel<ViewT> ListViewPage(ViewT model);

		PageModel<OutputT> ListPage(string sql, int pageNO = 1, int pageSize = 10, Collection<IDataParameter> parms = null, string orderBy = "", string preSql = "");
	}
}
