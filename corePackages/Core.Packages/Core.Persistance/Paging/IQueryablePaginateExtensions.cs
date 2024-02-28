using Microsoft.EntityFrameworkCore;

namespace Core.Persistance.Paging;

public static class IQueryablePaginateExtensions
{
    public static async Task<Paginate<T>> ToPaginateAsync<T>(
        this IQueryable<T> source,
        int index,
        int size, 
        CancellationToken cancellationToken = default
    )
    {
        //if (from > index)
        //    throw new ArgumentException($"From: {from.ToString()} > Index: {index.ToString()}, must from <= Index");

        int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

        List<T> items = await source.Skip(size * index)
            .Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

        Paginate<T> list =
            new()
            {
                Index = index,
                Count = count,
                Items = items,
                Size = size,
                Pages = (int)Math.Ceiling(count / (double)size)
            };
        return list;
    }

    public static Paginate<T> ToPaginate<T>
        (this IQueryable<T> source, int index, int size, int from = 0)
    {
        if (from > index)
            throw new ArgumentException($"From: {from.ToString()} > Index: {index.ToString()}, must from <= Index");

        int count = source.Count();
        var items = source.Skip(size * index).Take(size).ToList();

        Paginate<T> list =
            new()
            {
                Index = index,
                Size = size,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };
        return list;
    }
}
