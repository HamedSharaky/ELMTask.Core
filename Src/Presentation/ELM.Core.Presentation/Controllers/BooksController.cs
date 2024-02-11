using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ELM.Core.Application.Common.Contracts;
using ELM.Core.Application.Books.Search;

namespace ELM.Core.Presentation.Controllers
{
    [ApiController]
    public sealed class BooksController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(SearchBookQueryOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] SearchBookQuery query)
        {
            var output = await Mediator.Send(query);

            return Ok(output);
        }

    }
}
