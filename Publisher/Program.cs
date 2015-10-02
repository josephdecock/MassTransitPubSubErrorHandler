namespace Publisher
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;
    using MassTransit;
    using Messages;

    class Program
    {
        static void Main()
        {
            IBusControl busControl = CreateBus();
            BusHandle busHandle = busControl.Start();

            try
            {
                for (;;)
                {
                    // this is run as a Task to avoid weird console application issues
                    Task.Run(async () =>
                    {
                        busControl.Publish<ISomethingHappened>(new SomethingHappened());
                        Console.WriteLine("Publishing a message");
                    }).Wait();

                    Console.Write("Quit exits, anything else sends a message: ");
                    string theHappening = Console.ReadLine();
                    if (theHappening == "quit")
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception!!! OMG!!! {0}", ex);
            }
            finally
            {
                busHandle.Stop(TimeSpan.FromSeconds(30));
            }
        }

        static IBusControl CreateBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(x => x.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), h => { }));
        }

        class SomethingHappened : ISomethingHappened
        {
        }
    }
}