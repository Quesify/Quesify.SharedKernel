using System.Linq.Expressions;

namespace Quesify.SharedKernel.Core.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();

    bool IsSatisfiedBy(T obj);
}
