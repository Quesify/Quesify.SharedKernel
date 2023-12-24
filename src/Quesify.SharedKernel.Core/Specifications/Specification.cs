using System.Linq.Expressions;

namespace Quesify.SharedKernel.Core.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public virtual bool IsSatisfiedBy(T obj)
    {
        return ToExpression().Compile()(obj);
    }
}
