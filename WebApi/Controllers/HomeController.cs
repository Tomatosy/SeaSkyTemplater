using Tomato.NewTempProject.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Transactions;
using Tomato.NewTempProject.Model;
using Tomato.StandardLib.MyModel;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string url = Request.Url.ToString();
            return Redirect(url + "/swagger");
        }
    }
}
