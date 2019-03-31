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
    public class InternalJobServerProvider : IRecieveInternal
    {
        public void SendMessage(string inputMessage, string instanceId)
        {
            Trace.WriteLine($"String {inputMessage} recieved from brother instance at {instanceId}","Information");


            string adresa = String.Format("net.tcp://{0}/Internal2",RoleEnvironment.Roles["JobWorker2"].Instances[0].InstanceEndpoints["Internal2"].IPEndpoint.ToString());
            IForwardToSecondRole proxyForward = new ChannelFactory<IForwardToSecondRole>(new NetTcpBinding(), new EndpointAddress(adresa))
                .CreateChannel();
            proxyForward.SendMessage(inputMessage, instanceId, RoleEnvironment.CurrentRoleInstance.Id);
        }
    }
}
