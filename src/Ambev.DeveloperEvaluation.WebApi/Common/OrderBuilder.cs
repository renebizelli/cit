namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class OrderBuilder
{
    public static List<(string, bool)> Build(string? order, string defaultField)
    {
        if (string.IsNullOrEmpty(order)) return new List<(string, bool)> { (defaultField, true) };

        var orders = order.Trim().Split(',');

        var result = new List<(string, bool)>();

        foreach (var item in orders)
        {
            var orderDetail = item.Trim().Split(' ');

            var asc = orderDetail.Length == 1 || (orderDetail.Length == 2 && orderDetail[1].Trim().ToLower() == "asc");
            result.Add((orderDetail[0].Trim(), asc));
        }

        return result;
    }

}
