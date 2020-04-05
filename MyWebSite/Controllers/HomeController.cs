using Contracts;
using MyWebSite.WCFServiceProxy;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new LogInfo());
        }

        public ActionResult GetData()
        {
            LogInfo logInfo;
            Service2Client svcClient = null;
            try
            {
                using (svcClient = new Service2Client())
                {
                    logInfo = svcClient.GetData(new Contracts.LogInfo());
                }
            }
            finally
            {
                svcClient.SafeCloseExtension();
            }

            return View("Index", logInfo);
        }

        public async Task<ActionResult> AsyncGetData()
        {
            LogInfo logInfo;
            Service2Client svcClient = null;
            try
            {
                using (svcClient = new Service2Client())
                {
                    logInfo = svcClient.AsyncGetData(new Contracts.LogInfo());
                }
            }
            finally
            {
                svcClient.SafeCloseExtension();
            }

            return View("Index", logInfo);
        }


        public async Task<ActionResult> SuperAsyncGetData()
        {
            LogInfo logInfo;
            Service2Client svcClient = null;
            try
            {
                using (svcClient = new Service2Client())
                {
                    logInfo = await svcClient.SuperAsyncGetDataAsync(new Contracts.LogInfo());
                }
            }
            finally
            {
                svcClient.SafeCloseExtension();
            }

            return View("Index", logInfo);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}