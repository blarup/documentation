using System;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace LoadTestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Meant for scaling test of microservices running on kubernetes");
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            for(int i=0; i<10; i++)
            {
                DownloadUrl("https://35.197.13.255/comments/v1/comments");
                Console.WriteLine(i);
                Thread.Sleep(500);
            }
        }

        public static string DownloadUrl(string url)
        {
            return new System.Net.WebClient().DownloadString(url);
        }


    }
}
