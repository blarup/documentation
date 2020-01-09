using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LoadTestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> requests = new List<int>();

            for (int i = 0; i < 500; i++)
                requests.Add(i);

            Console.WriteLine("Meant for scaling test of microservices running on kubernetes");
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Test(requests, 10, "https://35.197.13.255/ratings/v1/ratings");
            Console.WriteLine("Finished");
        }

        public static string DownloadUrl(string url)
        {
            return new System.Net.WebClient().DownloadString(url);
        }

        private static void Test(List<int> requests, int threads, string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Parallel.ForEach(requests, new ParallelOptions
            {
                MaxDegreeOfParallelism = threads
            }, (item) =>
                    {                     
                        try
                        {
                            DownloadUrl(url);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    });
        }

    }
}
