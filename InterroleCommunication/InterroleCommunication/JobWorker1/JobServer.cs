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
    public class JobServer
    {
        private string externalEndpointName = "Input";
        private ServiceHost serviceHost;

        public JobServer()
        {
            RoleInstanceEndpoint inputEndpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[externalEndpointName];
            string endpoint = string.Format("net.tcp://{0}/{1}", inputEndpoint.IPEndpoint, externalEndpointName);
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost = new ServiceHost(typeof(JobServerProvider));

            serviceHost.AddServiceEndpoint(typeof(IClientSend), binding, endpoint);
        }

        public void Start()
        {
            try
            {
                serviceHost.Open();
                Trace.WriteLine($"Host on {externalEndpointName} opened ", "Information");
            }catch(Exception e)
            {
                Trace.WriteLine($"Host on {externalEndpointName} encoured error while opening", "Information");
            }
        }

        public void Stop()
        {
            try
            {
                serviceHost.Close();
                Trace.WriteLine($"Host on {externalEndpointName} closed ", "Information");
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Host on {externalEndpointName} encoured error while closing", "Information");
            }
        }
    }
}
