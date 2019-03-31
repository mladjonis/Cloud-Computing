using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker2
{
    public class InternalJobServerProvider2 : IForwardToSecondRole
    {
        List<Entity> entities = new List<Entity>();

        public void SendMessage(string inputMessage, string selfId, string recievedId)
        {
            Trace.TraceInformation(String.Format("String {0} received from JobWorker1", inputMessage));

            entities.Add(new Entity(inputMessage, selfId, recievedId));
        }
    }
}
