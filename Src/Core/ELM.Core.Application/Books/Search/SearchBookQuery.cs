using ELM.Core.Application.Common.Configuration.Queries;
using ELM.Core.Common.Dtos.Pagination;

namespace ELM.Core.Application.Books.Search
{
    public sealed record SearchBookQuery : BasePaginationRequestDto, IQuery<SearchBookQueryOutput>
    {
        public string SearchTerm { get; init; }
    }
}
