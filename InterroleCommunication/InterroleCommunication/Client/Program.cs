using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                string toRead = string.Empty;
                toRead = Console.ReadLine();

                IClientSend proxy = new ChannelFactory<IClientSend>(new NetTcpBinding(), new EndpointAddress
                    (string.Format("net.tcp://localhost:10100/Input"))).CreateChannel();

                proxy.SendMessage(toRead);
            }

            //string s = "skadkoasd.sadisadjasd.sdfk_IN_1";
            //string[] arrayOfS = s.Split('_');
            //Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            //Console.WriteLine(arrayOfS[2]);
            //if (s.Split('_')[2].Equals("1"))
            //    Console.WriteLine("Bravo majmune uspio si - jednako je ");

            ////logicno je da ne radi poredi strin i char idiote
            ////if (s.Split('_')[2].Equals('1'))
            ////    Console.WriteLine("I OVO RADI??");
            //foreach(var i in arrayOfS)
            //{
            //    Console.WriteLine(i);
            //}



        }
    }
}
