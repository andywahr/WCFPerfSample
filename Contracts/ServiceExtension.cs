using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public static class ServiceExtension
    {
        public static void SafeCloseExtension<T>(this ClientBase<T> proxy) where T : class
        {
            bool success = false;
            try
            {
                if (proxy?.State != CommunicationState.Faulted)
                {
                    proxy?.Close();
                    success = true;
                }
            }
            finally
            {
                if (!success)
                    proxy?.Abort();
            }
        }

    }
}
