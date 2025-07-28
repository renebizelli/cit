using Ambev.DeveloperEvaluation.Domain.Messages;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Messaging.Rebus;

public class SaleItemCancelledHandler : IHandleMessages<SaleItemCancelled> 
{
    public Task Handle(SaleItemCancelled message)
    {
        Console.WriteLine( "****************************************************");
        Console.WriteLine("SALE ITEM DELETED");
        Console.WriteLine("****************************************************");
        Console.WriteLine( message.SaleId.ToString());
        Console.WriteLine( message.SaleItemId.ToString());
        Console.WriteLine("****************************************************");

        return Task.CompletedTask;
    }
 
}
