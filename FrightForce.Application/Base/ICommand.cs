using MediatR;

namespace FrightForce.Application.Base;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommand
{
}