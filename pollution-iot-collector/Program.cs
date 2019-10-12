using System;
using System.Text;
using System.Threading.Tasks;
using Evangeloper.PollutionDataCollector.Business;
using Evangeloper.PollutionDataCollector.Models;
using Microsoft.Azure.EventHubs;

namespace Evangeloper.PollutionDataCollector
{
    class Program
    {
        private static EventHubClient eventHubClient;
        private const string eventHubConnectionString = "PROVIDE_CONNECTION_STRING";
        private const string eventHubName = "PROVIDE_HUB_NAME";

        static void Main(string[] args)
        {
            var collector = new PollutionCollector();
            Console.WriteLine("Ready to start collecting");
            MainAsync(args, collector).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args, IPollutionCollector collector)
        {
           try
           {
                var connectionStringBuilder = new EventHubsConnectionStringBuilder(eventHubConnectionString)
                {
                    EntityPath = eventHubName
                };
                
                eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

                for(int i = 0; i <= 100000; i++)
                {
                    var dataSample = collector.Collect();
                    Console.WriteLine($"Sample {i} - ID: {dataSample.ReadingId} - Date: {dataSample.ReadingDateTime} - Location ID: {dataSample.LocationId} - Pollution Level: {dataSample.PollutionLevel}");
                    await SendMessagesToEventHub(dataSample);
                }

                await eventHubClient.CloseAsync();
                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
           }  
           catch (Exception ex)
           {
                Console.WriteLine($"{DateTime.Now} > Exception: {ex.Message}");
           }
        }

        private static async Task SendMessagesToEventHub(PollutionData data)
        {
            try
            {
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(data.ToJSON())));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} > Exception: {ex.Message}");
            }
        }

    }
}
