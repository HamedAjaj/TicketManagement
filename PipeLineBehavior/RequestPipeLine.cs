using FluentValidation;
using MediatR;
using TicketManagement.PipeLineBehavior.Response;

namespace TicketManagement.PipeLineBehavior
{
    // Request PipeLine to check validation and goto next pipe to post or next based on validation
    public class RequestPipeLine<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public RequestPipeLine(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators.Select(validator => validator.Validate(context))
                .SelectMany(Omany => Omany.Errors)
                .Where(ObjCondition => ObjCondition != null)
                .ToList();


            if (failures.Any())
            {
                var errorResponse = new ResponseWrapper<object>
                {
                    Success = false,
                    Errors = failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}").ToList()
                };

                return Task.FromResult((TResponse)(object)errorResponse);
            }

            return next();
        }
    }
}
