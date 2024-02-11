using System.Threading.Tasks;
using ELM.Core.Application.Common.Configuration.Commands;
using ELM.Core.Application.Common.Configuration.Queries;

namespace ELM.Core.Application.Common.Contracts;

public interface IELMMediator
{
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
    Task ExecuteCommandAsync(ICommand command);
    Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
}
