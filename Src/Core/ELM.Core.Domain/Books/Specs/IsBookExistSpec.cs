using Ardalis.Specification;

namespace ELM.Core.Domain.Books.Specs
{
    public sealed class IsBookExistSpec : Specification<Book>
    {
        public IsBookExistSpec(string bookTitle, long bookId = 0)
        {
            if (bookId > 0)
            {
                Query.Where(p => p.Id != bookId);
            }

            Query.Where(p => p.BookInfo.BookTitle == bookTitle)
                .AsNoTracking();
        }
    }
}
