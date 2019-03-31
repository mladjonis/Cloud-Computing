using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker2
{
    public class InternalJobServer2
    {
        private string internalEndpointName = "Internal2";
        private ServiceHost serviceHost;

        public InternalJobServer2()
        {
            RoleInstanceEndpoint internalEndpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[internalEndpointName];

            NetTcpBinding binding = new NetTcpBinding();
            string endpoint = string.Format("net.tcp://{0}/{1}", internalEndpoint.IPEndpoint, internalEndpointName);
            serviceHost = new ServiceHost(typeof(InternalJobServerProvider2));

            serviceHost.AddServiceEndpoint(typeof(IForwardToSecondRole), binding, endpoint);
        }


        public void Start()
        {
            try
            {
                serviceHost.Open();
                Trace.WriteLine($"Host openet on {internalEndpointName}","Information");
            }catch(Exception e)
            {
                Trace.WriteLine($"Exception on opening host at {internalEndpointName} with message {e.Message}", "Information");
            }
        }

        public void Stop()
        {
            try
            {
                serviceHost.Close();
                Trace.WriteLine($"Host closed on {internalEndpointName}", "Information");
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Exception on closing host at {internalEndpointName} with message {e.Message}", "Information");
            }
        }
    }
}
