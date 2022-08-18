using FluentValidation;
using MediatR;

namespace Kanakku.UI.Impl.Event
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);
            var validationFailures = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();
            if (validationFailures.Any())
            {
                throw new ValidationException(validationFailures);
            }

            return await next();
        }
    }
}
