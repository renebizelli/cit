using Ambev.DeveloperEvaluation.Domain.Messages;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;

namespace Ambev.DeveloperEvaluation.Messaging.Rebus;
public static class RebusConfiguration
{
    public static void AddMessageHandlers(this IServiceCollection services)
    {
        services.AddRebus(configure =>
        {
            configure
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "sales"))
                .Routing(r => r.TypeBased()
                .Map<SaleCreated>("sales")
                .Map<SaleCancelled>("sales")
                .Map<SaleItemCancelled>("sales"));

            return configure;
        });

        services.AddScoped<IMessageSender, MessageSender>();

        services.AutoRegisterHandlersFromAssemblyOf<SaleCreatedHandler>();
        services.AutoRegisterHandlersFromAssemblyOf<SaleCancelledHandler>();
        services.AutoRegisterHandlersFromAssemblyOf<SaleItemCancelledHandler>();
    }
}
