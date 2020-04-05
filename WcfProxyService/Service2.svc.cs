using Contracts;
using System;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfProxyService.WCFPerfSample;
using IService1 = Contracts.IService1;

namespace WcfProxyService
{
    [ServiceContract(Namespace = "http://service")]
    public interface IService2
    {

        [OperationContract]
        LogInfo GetData(LogInfo value);

        [OperationContract]
        Task<LogInfo> AsyncGetData(LogInfo value);

        [OperationContract]
        Task<LogInfo> SuperAsyncGetData(LogInfo value);
    }

    public class Service2 : IService2
    {
        public LogInfo GetData(LogInfo value)
        {
            Service1Client svcClient = null;
            try
            {
                using (svcClient = new Service1Client())
                {
                    return addMe(svcClient.GetData(value));
                }
            }
            finally
            {
                svcClient.SafeCloseExtension();
            }

        }

        private LogInfo addMe(LogInfo logInfo)
        {
            logInfo.Publisher += $",{Environment.MachineName}";
            return logInfo;
        }

        public async Task<LogInfo> AsyncGetData(LogInfo value)
        {
            Service1Client svcClient = null;
            try
            {
                using (svcClient = new Service1Client())
                {
                    return addMe(svcClient.AsyncGetData(value));
                }
            }
            finally
            {
                svcClient.SafeCloseExtension();
            }
        }


        public async Task<LogInfo> SuperAsyncGetData(LogInfo value)
        {
            Service1Client svcClient = null;
            try
            {
                using (svcClient = new Service1Client())
                {
                    return addMe(await svcClient.SuperAsyncGetDataAsync(value));
                }
            }
            finally
            {
                svcClient.SafeCloseExtension();
            }
        }
    }
}
