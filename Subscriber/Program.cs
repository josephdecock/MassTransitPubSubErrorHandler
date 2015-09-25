namespace Client
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

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();

            busHandle.Stop(TimeSpan.FromSeconds(30));
        }

        static IBusControl CreateBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(bus =>
            {
                var host = bus.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), h => { });

                bus.ReceiveEndpoint(host, "Subscriber", ep =>
                {
                    ep.Consumer<SomethingHappenedConsumer>();
                    ep.UseRetry(Retry.Interval(1, TimeSpan.FromSeconds(1)));
                });
            });
        }

        class SomethingHappenedConsumer : IConsumer<ISomethingHappened>
        {
            public Task Consume(ConsumeContext<ISomethingHappened> context)
            {
                throw new Exception("This is a contrived exception for example purposes");
            }
        }
    }
}