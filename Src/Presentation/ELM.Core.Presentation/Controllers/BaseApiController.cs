using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ELM.Core.Presentation.Controllers
{
    [ApiController]
    [Route("ELM/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();
    }
}