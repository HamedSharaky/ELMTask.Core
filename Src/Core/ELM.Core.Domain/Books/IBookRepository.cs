using ELM.Core.Domain.Books;
using ELM.Core.Domain.Books.Outputs;
using ELM.Core.Domain.Common;
using ELM.Core.Domain.Common.Dtos.Cache;

namespace ELM.Core.Domain.Books
{
    public interface IBookRepository : IRepository<Book, long>
    {
        Task<BookSearchCacheDto> SearchBookAsync(string searchTerm); 
        Task<IEnumerable<BookOutput>> GetBooksByIdzAsync(IEnumerable<long> bookIdz); 
    }
}
