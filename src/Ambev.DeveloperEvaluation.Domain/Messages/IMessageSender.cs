namespace Ambev.DeveloperEvaluation.Domain.Messages;

public interface IMessageSender
{
    Task SendAsync<T>(T message) where T : class;
}
