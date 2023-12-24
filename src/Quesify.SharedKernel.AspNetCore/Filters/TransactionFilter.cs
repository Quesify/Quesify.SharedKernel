using Microsoft.AspNetCore.Mvc.Filters;
using System.Transactions;

namespace Quesify.SharedKernel.AspNetCore.Filters;

public class TransactionAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await next();
        transactionScope.Complete();
    }
}
