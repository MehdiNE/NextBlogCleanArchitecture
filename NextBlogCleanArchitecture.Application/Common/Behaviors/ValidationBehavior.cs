using FluentResults;
using FluentValidation;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    // MediatR's DI will inject all IValidator<TRequest> implementations
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // If there are no validators for this request, just continue
        if (!_validators.Any())
        {
            return await next(cancellationToken);
        }

        // Create a validation context
        var context = new ValidationContext<TRequest>(request);

        // Run all validators and collect the results
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        // Get all the validation failures
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        // If there are any validation failures...
        if (failures.Any())
        {
            // ...create a new response of type TResponse (which must be a Result)
            var result = new TResponse();

            // Convert FluentValidation failures to FluentResults errors
            var validationErrors = failures
                .Select(f => new Error(f.ErrorMessage)
                .WithMetadata("PropertyName", f.PropertyName));

            // Add the errors to the result
            result.Reasons.AddRange(validationErrors);

            // Return the failed result, short-circuiting the request pipeline
            return result;
        }

        // If validation succeeds, continue to the actual request handler
        return await next(cancellationToken);
    }
}


//using ErrorOr;
//using FluentValidation;
//using MediatR;

//namespace NextBlogCleanArchitecture.Application.Common.Behaviors;

//public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
//    : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : IRequest<TResponse>
//        where TResponse : IErrorOr
//{
//    private readonly IValidator<TRequest>? _validator = validator;

//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next,
//        CancellationToken cancellationToken)
//    {
//        if (_validator is null)
//        {
//            return await next(cancellationToken);
//        }

//        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

//        if (validationResult.IsValid)
//        {
//            return await next();
//        }

//        var errors = validationResult.Errors
//            .ConvertAll(error => Error.Validation(
//                code: error.PropertyName,
//                description: error.ErrorMessage));

//        return (dynamic)errors;
//    }
//}
