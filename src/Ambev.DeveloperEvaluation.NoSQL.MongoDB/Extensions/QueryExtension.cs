using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Extensions;

public static class QueryExtension
{
    public static IFindFluent<T, T> ApplyOrdering<T>(this IFindFluent<T, T> query, List<(string, bool)> sortCriteria, Dictionary<string, Expression<Func<T, object>>> allowedOrderFields)
    {
        ArgumentNullException.ThrowIfNull(sortCriteria);

        if (!sortCriteria.Any()) return query;

        foreach (var (field, ascending) in sortCriteria)
        {
            if (!allowedOrderFields.TryGetValue(field, out var expression))
                continue;

            query = ascending ? query.SortBy(expression) : query.SortByDescending(expression);
        }

        return query;
    }

    public static IFindFluent<T, T> ApplyPaging<T>(this IFindFluent<T, T> query, int page, int pageSize)
    {
        return query.Skip((page - 1) * pageSize).Limit(pageSize);
    }

    public static FilterDefinition<T> ApplyWhereLike<T>(this FilterDefinition<T> query, string? criteria, Expression<Func<T, object>> selector)
    {
        if (string.IsNullOrWhiteSpace(criteria))
            return query;

        string trimmed = criteria.Trim('*');
        var param = selector.Parameters[0];
        var member = selector.Body;

        var filter = criteria switch
        {
            var s when s.StartsWith("*") && s.EndsWith("*") =>
                Builders<T>.Filter.Regex(selector, new BsonRegularExpression($".*{trimmed}.*", "i")),

            var s when s.StartsWith("*") =>
                Builders<T>.Filter.Regex(selector, new BsonRegularExpression($"{trimmed}^", "i")),

            var s when s.EndsWith("*") =>
                Builders<T>.Filter.Regex(selector, new BsonRegularExpression($"^{trimmed}", "i")),

            _ => Builders<T>.Filter.Regex(selector, new BsonRegularExpression($".*{trimmed}.*", "i"))
        }; 

        return query &= filter;
    }

    public static FilterDefinition<T> ApplyWhereRange<T>(this FilterDefinition<T> query, decimal minValue, decimal maxValue, Expression<Func<T, decimal>> selector)
    {
        if (minValue > 0)
        {
            query &= Builders<T>.Filter.Gte(selector, minValue);
        }

        if (maxValue > 0)
        {
            query &= Builders<T>.Filter.Lte(selector, maxValue);
        }

        return query;
    }

}





