// See https://aka.ms/new-console-template for more information
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TransactionalEvent.Dal;

Console.WriteLine("Hello, World!");

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("rabbitmq://localhost");
    cfg.ReceiveEndpoint("person_queue", ep =>
    {
        ep.Durable = true;
    });
});


var dbContextOptionsBuilder = new DbContextOptionsBuilder<PersonDbContext>();

dbContextOptionsBuilder.UseSqlServer("Server=.,1433;Database=PersonDb;User Id=sa;Password=Abcd_1234;MultipleActiveResultSets=true;TrustServerCertificate=True");

var context = new PersonDbContext(dbContextOptionsBuilder.Options);

await bus.StartAsync();

do
{
    var events = context.OutBoxEvents.ToList();

    foreach (var outboxEvent in events)
    {
        await bus.Publish(outboxEvent);
        context.Remove(outboxEvent);
        context.SaveChanges();
    }

    System.Threading.Thread.Sleep(2000);

} while (true);


await bus.StopAsync();
