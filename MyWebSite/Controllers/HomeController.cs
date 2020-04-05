using Contracts;
using MyWebSite.WCFServiceProxy;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MyWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine($"DefaultConnectionLimit = {System.Net.ServicePointManager.DefaultConnectionLimit}");
            System.Diagnostics.Debug.WriteLine($"Expect100Continue = {System.Net.ServicePointManager.Expect100Continue}");
            System.Diagnostics.Debug.WriteLine($"Expect100Continue = {System.Net.ServicePointManager.UseNagleAlgorithm}");
            System.Diagnostics.Debug.WriteLine($"HostingEnvironment.MaxConcurrentRequestsPerCPU = {HostingEnvironment.MaxConcurrentRequestsPerCPU}");
            System.Diagnostics.Debug.WriteLine($"HostingEnvironment.MaxConcurrentThreadsPerCPU = {HostingEnvironment.MaxConcurrentThreadsPerCPU}");
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