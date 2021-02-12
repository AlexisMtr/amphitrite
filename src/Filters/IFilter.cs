using System.Linq;

namespace Amphitrite.Filters
{
    public interface IFilter<T>
    {
        IQueryable<T> Filter(IQueryable<T> source);
    }

    public interface IFilter<TSource, TResult>
    {
        IQueryable<TResult> Filter(IQueryable<TSource> source);
    }
}
