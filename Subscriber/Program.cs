namespace Subscriber
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
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), h => { });

                cfg.ReceiveEndpoint(host, "Subscriber", e =>
                {
                    e.Consumer<SomethingHappenedConsumer>();
                    e.UseRetry(Retry.Interval(1, TimeSpan.FromSeconds(1)));
                });
            });

            //The callbacks in this observer get called...
            bus.ConnectReceiveObserver(new ReceiveObserver());

            //...but not in these two observers
            bus.ConnectConsumeObserver(new ConsumeObserver());
            bus.ConnectConsumeMessageObserver(new ConsumeSomethingHappenedObserver());

            return bus;
        }

        class SomethingHappenedConsumer : IConsumer<ISomethingHappened>
        {
            //static Random rand = new Random();

            public async Task Consume(ConsumeContext<ISomethingHappened> context)
            {
                //if (rand.Next(2) > 0)
                //{
                //    throw new Exception("This is a contrived exception for example purposes");
                //}
                await Console.Out.WriteLineAsync("Successfully consumed a message");
            }
        }
    }
}