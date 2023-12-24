namespace Quesify.SharedKernel.Core.Specifications;

public abstract class CompositeSpecification<T> : Specification<T>
{
    public ISpecification<T> Left { get; }
    public ISpecification<T> Right { get; }

    public CompositeSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        Left = left;
        Right = right;
    }
}
