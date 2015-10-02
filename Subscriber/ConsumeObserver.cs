namespace Subscriber
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using MassTransit.Pipeline;

    public class ConsumeObserver : IConsumeObserver
    {
        public async Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
        {
            await Console.Out.WriteLineAsync("ConsumeObserver.ConsumeFault");
        }

        public async Task PostConsume<T>(ConsumeContext<T> context) where T : class
        {
            await Console.Out.WriteLineAsync("ConsumeObserver.PostConsume");
        }

        public async Task PreConsume<T>(ConsumeContext<T> context) where T : class
        {
            await Console.Out.WriteLineAsync("ConsumeObserver.PreConsume");
        }
    }
}
