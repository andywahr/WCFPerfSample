using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace WCFPerfSample
{
    public class AIErrorHandler : IErrorHandler
    {
        private TelemetryClient telemetry = new TelemetryClient();

        public bool HandleError(Exception error)
        {
            telemetry.TrackException(error);
            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {

        }
    }

    public class AIErrorHandlerBehavior : BehaviorExtensionElement, IServiceBehavior 
    {
        public override Type BehaviorType => typeof(AIErrorHandlerBehavior);

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var errorHandler = new AIErrorHandler();
            foreach (ChannelDispatcher chanDisp in serviceHostBase.ChannelDispatchers)
            {
                chanDisp.ErrorHandlers.Add(errorHandler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        protected override object CreateBehavior()
        {
            return new AIErrorHandlerBehavior();
        }
    }
}