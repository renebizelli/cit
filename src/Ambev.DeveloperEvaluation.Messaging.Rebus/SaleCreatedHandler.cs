using Ambev.DeveloperEvaluation.Domain.Messages;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Messaging.Rebus;

public class SaleCreatedHandler : IHandleMessages<SaleCreated> 
{
    public Task Handle(SaleCreated message)
    {
        Console.WriteLine("****************************************************");
        Console.WriteLine("SALE CREATED");
        Console.WriteLine("****************************************************");
        Console.WriteLine(message.Sale.Id);
        Console.WriteLine("****************************************************");

        return Task.CompletedTask;
    }
 
}
