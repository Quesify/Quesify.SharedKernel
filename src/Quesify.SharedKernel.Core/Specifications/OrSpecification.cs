using System.Linq.Expressions;

namespace Quesify.SharedKernel.Core.Specifications;

public class OrSpecification<T> : CompositeSpecification<T>
{
    public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        : base(left, right)
    {
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        return Left.ToExpression().Or(Right.ToExpression());
    }
}
