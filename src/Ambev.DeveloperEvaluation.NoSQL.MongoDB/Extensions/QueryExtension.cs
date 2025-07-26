using Ambev.DeveloperEvaluation.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Extensions;

public static class QueryExtension
{
    public static IFindFluent<T, T> ApplyOrdering<T>(this IFindFluent<T, T> query, IOrderSettings orderSettings, Dictionary<string, Expression<Func<T, object>>> allowedOrderFields)
    {
        ArgumentNullException.ThrowIfNull(orderSettings.Criteria);

        if (!orderSettings.Criteria.Any()) return query;

        var builder = Builders<T>.Sort;

        var definitions = new List<SortDefinition<T>>();

        foreach (var (field, ascending) in orderSettings.Criteria)
        {
            if (!allowedOrderFields.TryGetValue(field, out var expression))
                continue;

            definitions.Add(ascending ? builder.Ascending(expression) : builder.Descending(expression));
        }

        query.Sort(builder.Combine((definitions.ToArray())));

        return query;
    }

    public static IFindFluent<T, T> ApplyPaging<T>(this IFindFluent<T, T> query, IPagingSettings pagingSettings)
    {
        return query.Skip((pagingSettings.Page - 1) * pagingSettings.Size).Limit(pagingSettings.Size);
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





