using AutoMapper;
using CollaboratorsManagement.Common.Dtos.Pagination;
using ELM.Core.Application.Common.Mappings;
using ELM.Core.Domain.Books;
using ELM.Core.Domain.Books.Outputs;
using System.Collections.Generic;

namespace ELM.Core.Application.Books.Search
{
    public sealed record SearchBookQueryOutput: BasePaginationResponseDto
    {
        public IList<BookInListDto> Books { get; init; }
    }

    public sealed record BookInListDto: IMapFrom<BookOutput>
    {
        public long BookId { get; init; }
        public string BookTitle { get; init; }
        public string BookDescription { get; init; }
        public string Author { get; init; }
        public DateTime PublishDate { get; init; }
        public string CoverBase64 { get; init; }
        public DateTime LastModified { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookInListDto, BookOutput>(MemberList.None).ReverseMap();
        }
    }
}
