using Contracts;
using System;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WCFPerfSample
{
    public class Service1 : IService1
    {
        static Random random = new Random();

        public LogInfo GetData(LogInfo value)
        {
            using (wcftestEntities dbContent = new wcftestEntities())
            {
                var orders = dbContent.SalesOrderHeaders.Where(oh => oh.SalesOrderID > 70000)
                    .Include(oh => oh.SalesOrderDetails.Select(od => od.Product))
                    .Include(oh => oh.Address)
                    .Include(oh => oh.Customer)
                    .Include(oh => oh.SalesOrderDetails).ToList();

                value.FoundPerson = orders.First().Customer.LastName;
            }
            return value;
        }

        public async Task<LogInfo> AsyncGetData(LogInfo value)
        {
            using (wcftestEntities dbContent = new wcftestEntities())
            {
                var orders =  dbContent.SalesOrderHeaders.Where(oh => oh.SalesOrderID > 70000)
                    .Include(oh => oh.SalesOrderDetails.Select(od => od.Product))
                    .Include(oh => oh.Address)
                    .Include(oh => oh.Customer)
                    .Include(oh => oh.SalesOrderDetails).ToList();

                value.FoundPerson = orders.First().Customer.LastName;

            }

            return value;
        }


        public async Task<LogInfo> SuperAsyncGetData(LogInfo value)
        {
            using (wcftestEntities dbContent = new wcftestEntities())
            {
                var orders = await dbContent.SalesOrderHeaders.Where(oh => oh.SalesOrderID > 70000)
                    .Include(oh => oh.SalesOrderDetails.Select(od => od.Product))
                    .Include(oh => oh.Address)
                    .Include(oh => oh.Customer)
                    .Include(oh => oh.SalesOrderDetails).ToListAsync();

                value.FoundPerson = orders.First().Customer.LastName;

            }

            return value;
        }
    }
}
