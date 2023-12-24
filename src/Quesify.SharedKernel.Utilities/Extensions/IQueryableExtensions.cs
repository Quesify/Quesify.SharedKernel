using Quesify.SharedKernel.Utilities.Guards;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace System.Linq;

public static class IQueryableExtensions
{
    public static IQueryable<T> PageBy<T>(
        this IQueryable<T> query,
        int page,
        int size)
    {
        Guard.Against.Null(query, nameof(query));
        Guard.Against.NegativeOrZero(page, nameof(page));
        Guard.Against.NegativeOrZero(size, nameof(size));

        return query.Skip((page - 1) * size).Take(size);
    }

    public static IQueryable<T> PageByIf<T>(
        this IQueryable<T> query,
        int? page,
        int? size)
    {
        Guard.Against.Null(query, nameof(query));

        return (page ?? size) == null 
            ? query 
            : query.Skip((page!.Value - 1) * size!.Value).Take(size.Value);
    }

    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> predicate)
    {
        Guard.Against.Null(source, nameof(source));
        Guard.Against.Null(predicate, nameof(predicate));

        return condition
            ? source.Where(predicate) 
            : source;
    }

    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, bool>>? predicate)
    {
        Guard.Against.Null(source, nameof(source));

        return predicate == null
            ? source 
            : source.Where(predicate);
    }

    public static IQueryable<T> OrderByIf<T>(
        this IQueryable<T> query,
        string? sorting)
    {
        Guard.Against.Null(query, nameof(query));

        return sorting == null
            ? query 
            : query.OrderBy(sorting);
    }
}
