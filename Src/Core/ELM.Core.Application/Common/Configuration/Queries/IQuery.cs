using MediatR;

namespace ELM.Core.Application.Common.Configuration.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
