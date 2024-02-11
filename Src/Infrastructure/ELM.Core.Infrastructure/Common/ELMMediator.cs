using ELM.Core.Application.Common.Configuration.Commands;
using ELM.Core.Application.Common.Configuration.Queries;
using ELM.Core.Application.Common.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ELM.Core.Infrastructure.Common
{
    public sealed class ELMMediator : IELMMediator
    {
        private readonly IMediator _mediator;

        public ELMMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await _mediator.Send(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            return await _mediator.Send(query);
        }
    }
}
