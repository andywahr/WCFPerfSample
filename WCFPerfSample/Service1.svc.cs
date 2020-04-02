using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WCFPerfSample
{
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            throw new NullReferenceException("bob");
            using (wcftestEntities dbContent = new wcftestEntities())
            {
                var orders = dbContent.SalesOrderHeaders.Where(oh => oh.SalesOrderID > 70000)
                    .Include(oh => oh.SalesOrderDetails.Select(od => od.Product))
                    .Include(oh => oh.Address)
                    .Include(oh => oh.Customer)
                    .Include(oh => oh.SalesOrderDetails).ToList();

                return orders.First().Customer.LastName;
            }
        }

        public async Task<string> AsyncGetData(int value)
        {
            using (wcftestEntities dbContent = new wcftestEntities())
            {
                var orders = await dbContent.SalesOrderHeaders.Where(oh => oh.SalesOrderID > 70000)
                    .Include(oh => oh.SalesOrderDetails.Select(od => od.Product))
                    .Include(oh => oh.Address)
                    .Include(oh => oh.Customer)
                    .Include(oh => oh.SalesOrderDetails).ToListAsync();

                return orders.First().Customer.LastName;
            }
        }
    }
}
