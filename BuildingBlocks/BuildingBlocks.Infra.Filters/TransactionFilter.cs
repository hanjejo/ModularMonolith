using Microsoft.AspNetCore.Mvc.Filters;
using System.Transactions;

namespace BuildingBlocks.Infra.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TransactionFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var executedContext = await next();

                if (executedContext.Exception == null || executedContext.ExceptionHandled)
                {
                    transactionScope.Complete();
                }
            }
        }
    }
}
