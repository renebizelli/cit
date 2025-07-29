using Ambev.DeveloperEvaluation.Domain.Messages;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Messaging.Rebus;

public class SaleCancelledHandler : IHandleMessages<SaleCancelled> 
{
    public Task Handle(SaleCancelled message)
    {
        Console.WriteLine( "****************************************************");
        Console.WriteLine("SALE DELETED");
        Console.WriteLine("****************************************************");
        Console.WriteLine( message.SaleId.ToString());
        Console.WriteLine("****************************************************");

        return Task.CompletedTask;
    }
 
}
