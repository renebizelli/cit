using Ambev.DeveloperEvaluation.Domain.Messages;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Messaging.Rebus;

public class MessageSender : IMessageSender
{
    private readonly IBus _bus; 
    public MessageSender(IBus bus)
    {
        _bus = bus;
    }

    public async Task SendAsync<T>(T message) where T : class
    => await _bus.Send(message);
}
