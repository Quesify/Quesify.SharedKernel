using MongoDB.Driver.Linq;
using Quesify.SharedKernel.Utilities.Guards;
using System.Linq.Dynamic.Core;

namespace MongoDB.Driver.Linq;

public static class IMongoQueryableExtensions
{
    public static IMongoQueryable<TSource> Order<TSource>(
        this IMongoQueryable<TSource> query,
        string ordering)
    {
        Guard.Against.Null(query, nameof(query));

        return string.IsNullOrWhiteSpace(ordering)
            ? query
            : (IMongoQueryable<TSource>)query.OrderBy(ordering);
    }

    public static IMongoQueryable<TSource> Page<TSource>(
        this IMongoQueryable<TSource> query,
        int page,
        int size)
    {
        Guard.Against.Null(query, nameof(query));
        return (IMongoQueryable<TSource>)query.Page(page: page, pageSize: size);
    }
}
