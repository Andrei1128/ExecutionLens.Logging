using Castle.DynamicProxy;
using ExecutionLens.Logging.APPLICATION.Contracts;
using ExecutionLens.Logging.APPLICATION.Utilities;
using ExecutionLens.Logging.DOMAIN.Factories;

namespace ExecutionLens.Logging.APPLICATION.Implementations;

internal class InterceptorService(ILogService _logService, LogManager _logManager) : IInterceptorService
{
    public void Intercept(IInvocation invocation)
    {
        if (!_logManager.IsLogging)
        {
            invocation.Proceed();
        }
        else
            try
            {
                _logService.AddLogEntry(MethodEntryFactory.Create(invocation));

                invocation.Proceed();

                _logService.AddLogExit(MethodExitFactory.Create(invocation));

            }
            catch (Exception ex)
            {
                _logService.AddLogExit(MethodExitFactory.Create(ex));

                throw;
            }
    }
}
