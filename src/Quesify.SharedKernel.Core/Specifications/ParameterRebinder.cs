using System.Linq.Expressions;

namespace Quesify.SharedKernel.Core.Specifications;

internal class ParameterRebinder : ExpressionVisitor
{
    private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

    internal ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
    {
        _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
    }

    internal static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression expression)
    {
        return new ParameterRebinder(map).Visit(expression);
    }

    protected override Expression VisitParameter(ParameterExpression parameterExpression)
    {
        if (_map.TryGetValue(parameterExpression, out var replacement))
        {
            parameterExpression = replacement;
        }

        return base.VisitParameter(parameterExpression);
    }
}
