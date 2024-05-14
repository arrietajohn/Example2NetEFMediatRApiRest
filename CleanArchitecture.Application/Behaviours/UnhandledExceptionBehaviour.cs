using MediatR;
using Microsoft.Extensions.Logging;

namespace MyProject.Application.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex) { 
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "Application Request: Sucedio una excepcion para el request {Name} {@Request}", requestName, request);
            throw;
        }
    }
}
