using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
namespace Topic.Send
{
    public class Invoice
    {
        public string Number { get; set; }
        public string Price { get; set; }

        public string Type { get; set; }
    }
    
    public class  Purchase
    {
        public string Number { get; set; }
        public string Price { get; set; }
        public string Type { get; set; }
    }

  public static class Program
    {

        const string ServiceBusConnectionString = "Endpoint=sb://financeservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DsuN0sYgt5zf2xGldsh4l8NLGXO1+tM5F32SBJRXHQQ=";
        const string TopicName = "quicklane";
        static ITopicClient topicClient;

        public static async Task Main(string[] args)
        {
            const int numberOfMessages = 10;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            // Send messages.
            await SendMessagesAsync(numberOfMessages);

            Console.ReadKey();

            await topicClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the topic.

                    Invoice inv = new Invoice();
                    inv.Number = "123"+i.ToString();
                    inv.Price = "100";
                    inv.Type = "Invoice";

                    Purchase pur = new Purchase();
                    inv.Number = "123" + i.ToString();
                    inv.Price = "100";
                    inv.Type = "Invoice";


                    string messageBody = JsonConvert.SerializeObject(inv);
                  //  Movie m = JsonConvert.DeserializeObject<Movie>(json);
                    //  string messageBody = $"Message {i}";


                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    if ((i + 1) % 2 == 0)
                    {
                        message.UserProperties.Add("Type", "QuickBook");
                     //   message.UserProperties.Add("Type", "Zero");
                    }
                    else
                    {
                      //  message.UserProperties.Add("Type", "QuickBook");
                        message.UserProperties.Add("Type", "Zero");
                    }

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the topic.
                    await topicClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

    }
}
