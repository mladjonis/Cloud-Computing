using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker1
{
    public class InternalJobServer
    {
        private string internalEndpointName = "Internal";
        private ServiceHost serviceHost;

        public InternalJobServer()
        {
            RoleInstanceEndpoint internalEndpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[internalEndpointName];
            NetTcpBinding binding = new NetTcpBinding();
            string endpoint = string.Format("net.tcp://{0}/{1}", internalEndpoint.IPEndpoint, internalEndpointName);
            serviceHost = new ServiceHost(typeof(InternalJobServerProvider));

            serviceHost.AddServiceEndpoint(typeof(IRecieveInternal), binding, endpoint);
        }

        public void Start()
        {
            try
            {
                serviceHost.Open();
                Trace.WriteLine($"Host on {internalEndpointName} opened", "Information");
            }catch(Exception e)
            {
                Trace.WriteLine($"Host on {internalEndpointName} encoured error while opening", "Information");
            }
        }

        public void Stop()
        {
            try
            {
                serviceHost.Close();
                Trace.WriteLine($"Host on {internalEndpointName} closed", "Information");
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Host on {internalEndpointName} encoured error while closing", "Information");
            }
        }
    }
}
