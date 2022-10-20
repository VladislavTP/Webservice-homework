using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Reflection;

namespace WCFService
{
   
    internal class Server
    {
        
        static void Main(string[] args)
        {
            Uri[] uris = new Uri[1];
            string adress = "http://localhost:8080/Service1";
            uris[0] = new Uri(adress);
            IService1 service = new Service1();
            ServiceHost host = new ServiceHost(new Service1(), uris);
            BasicHttpBinding binding = new BasicHttpBinding("MyBind");
            host.AddServiceEndpoint(typeof(IService1), binding, "");
            host.Opened += HostOpened;
            host.Open();
            Console.ReadLine();
        }

        private static void HostOpened(object sender, EventArgs evArgs)
        {
            Console.WriteLine("Dictionary creator service started");
        }


    }
}
