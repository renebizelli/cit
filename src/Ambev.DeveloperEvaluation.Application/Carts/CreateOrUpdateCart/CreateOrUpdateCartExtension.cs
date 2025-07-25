namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public static class CreateOrUpdateCartExtension
{
    public static void NormalizeItems(this CreateOrUpdateCartCommand command)
    {
        command.Items = command.Items.GroupBy(p => p.ProductId)
                       .Select(g => new CreateOrUpdateCartCommand.CartItem
                       {
                           ProductId = g.Key,
                           Quantity = g.Sum(p => p.Quantity)
                       })
                       .ToList();
    }
}
