using MediatR;

namespace ELM.Core.Application.Common.Configuration.Commands
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
