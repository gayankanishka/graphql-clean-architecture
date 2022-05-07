using MediatR;
using Microsoft.Extensions.Logging;

namespace ConferencePlanner.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "ConferencePlanner GraphQL Request: Unhandled Exception for Request {Name} {@Request}",
                requestName, request);

            throw;
        }
    }
}