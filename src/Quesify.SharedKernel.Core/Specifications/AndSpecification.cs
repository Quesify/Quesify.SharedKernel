using System.Linq.Expressions;

namespace Quesify.SharedKernel.Core.Specifications;

public class AndSpecification<T> : CompositeSpecification<T>
{
    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        : base(left, right)
    {
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        return Left.ToExpression().And(Right.ToExpression());
    }
}
