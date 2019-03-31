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
    public class JobServerProvider : IClientSend
    {
        public void SendMessage(string inputMessage)
        {
            Trace.WriteLine($"Cliend sent {inputMessage}", "Information");
            string internalEndpointName = "Internal";
            //NetTcpBinding binding = new NetTcpBinding();

            //svi endpointovi sem ovog prvog pozvanog koji je ujedno sluzio i za pozivanje ove fcije(znaci input)
            //umjesto ove kobaje u roles moze i ici jednostavno ime workerrole tj JobWorker1
            List<EndpointAddress> listOfInstancesEndpoints = RoleEnvironment.Roles[RoleEnvironment.CurrentRoleInstance.Role.Name].Instances.Where(
                x => x.Id != RoleEnvironment.CurrentRoleInstance.Id).Select(
                process => new EndpointAddress(String.Format("net.tcp://{0}/{1}", process.InstanceEndpoints[internalEndpointName].IPEndpoint.ToString(),
                internalEndpointName))).ToList();


            List<EndpointAddress> listOfInstancesEndpoints2 = RoleEnvironment.Roles["JobWorker1"].Instances.Where(
                x => x.Id != RoleEnvironment.CurrentRoleInstance.Id).Select(
                process => new EndpointAddress(String.Format("net.tcp://{0}/{1}", process.InstanceEndpoints[internalEndpointName].IPEndpoint.ToString(),
                internalEndpointName))).ToList();

            int nextID = int.Parse(RoleEnvironment.CurrentRoleInstance.Id.Split('_')[2]) + 1;
            if (nextID >= RoleEnvironment.Roles["JobWorker1"].Instances.Count)
                nextID = 0;

            //string adresa = string.Format("net.tcp://");
            var a =RoleEnvironment.Roles["JobWorker1"].Instances[nextID].InstanceEndpoints["Internal"].IPEndpoint;
            var b = RoleEnvironment.Roles["JobWorker1"].Instances[nextID].InstanceEndpoints["Internal"].IPEndpoint.ToString();

            string adresa = string.Format("net.tcp://{0}/Internal", a);
            string adresa2 = string.Format("net.tcp://{0}/Internal", b);

            IRecieveInternal proxyRecieveInternal = new ChannelFactory<IRecieveInternal>(new NetTcpBinding(), new EndpointAddress(adresa))
                .CreateChannel();
            proxyRecieveInternal.SendMessage(inputMessage, RoleEnvironment.CurrentRoleInstance.Id);

        }
    }
}
