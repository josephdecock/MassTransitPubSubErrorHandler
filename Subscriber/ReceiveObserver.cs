namespace Subscriber
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;

    public class ReceiveObserver : IReceiveObserver
    {
        public async Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
        {
            await Console.Out.WriteLineAsync("ReceiveObserver.ConsumeFault");
        }

        public async Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            await Console.Out.WriteLineAsync("ReceiveObserver.PostConsume");
        }

        public async Task PostReceive(ReceiveContext context)
        {
            await Console.Out.WriteLineAsync("ReceiveObserver.PostReceive");
        }

        public async Task PreReceive(ReceiveContext context)
        {
            await Console.Out.WriteLineAsync("ReceiveObserver.PreReceive");
        }

        public async Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            await Console.Out.WriteLineAsync("ReceiveObserver.ReceiveFault");
        }
    }
}
