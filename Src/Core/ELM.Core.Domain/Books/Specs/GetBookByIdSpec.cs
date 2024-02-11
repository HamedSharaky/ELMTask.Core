using Ardalis.Specification;
using ELM.Core.Domain.Books;

namespace ELM.Core.Domain.Books.Specs
{
    public sealed class GetBookByIdSpec : Specification<Book>, ISingleResultSpecification<Book>
    {
        public GetBookByIdSpec(long bookId)
        {
            Query.Where(p => p.Id == bookId);
        }
    }
}
