using MediatR;

namespace FrightForce.Application.Base;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
