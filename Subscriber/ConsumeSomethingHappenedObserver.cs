
namespace Subscriber
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using MassTransit.Pipeline;
    using Messages;

    public class ConsumeSomethingHappenedObserver : IConsumeMessageObserver<ISomethingHappened>
    {
        public async Task ConsumeFault(ConsumeContext<ISomethingHappened> context, Exception exception)
        {
            await Console.Out.WriteLineAsync("ConsumeSomethingHappenedObserver.ConsumeFault");
        }

        public async Task PostConsume(ConsumeContext<ISomethingHappened> context)
        {
            await Console.Out.WriteLineAsync("ConsumeSomethingHappenedObserver.PostConsume");
        }

        public async Task PreConsume(ConsumeContext<ISomethingHappened> context)
        {
            await Console.Out.WriteLineAsync("ConsumeSomethingHappenedObserver.PreConsume");
        }
    }
}
