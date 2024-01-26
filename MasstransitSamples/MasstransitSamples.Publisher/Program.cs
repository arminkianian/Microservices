using MassTransit;
using MasstransitSamples.Contracts;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
        {
            sbc.Host("rabbitmq://localhost");
        });

        await bus.StartAsync();

        do
        {
            Console.WriteLine("Write your message ");

            var message = new Message
            {
                Text = Console.ReadLine()
            };

            await bus.Publish(message);

            Console.WriteLine("Continue messaging? y/any thing else: ");

        } while (Console.ReadLine().ToLower() == "y");

        await bus.StopAsync();
    }
}