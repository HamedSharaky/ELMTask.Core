using Dapper;
using ELM.Core.Application.Common.Interfaces;
using ELM.Core.Domain.Books;
using ELM.Core.Domain.Books.Outputs;
using ELM.Core.Domain.Common.Dtos.Cache;
using ELM.Core.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ELM.Core.Persistence.Domain.Books
{
    internal sealed class BookRepository : Repository<Book, long>, IBookRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public BookRepository(ELMDbContext context, ISqlConnectionFactory sqlConnectionFactory) : base(context)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<BookSearchCacheDto> SearchBookAsync(string searchTerm)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var bookIdz = await connection.QueryAsync<long>(
                $@"
                    SELECT [Id] AS BookId
                    FROM [dbo].[vwBook] WITH (NOEXPAND)
                    WHERE BookTitle LIKE @SearchTerm
	                    OR BookDescription LIKE @SearchTerm
	                    OR Author LIKE @SearchTerm
	                    OR PublishDate LIKE @SearchTerm
                    ORDER BY [LastModified] DESC
                ",
                new
                {
                    SearchTerm = $"%{searchTerm}%"
                });

            return new BookSearchCacheDto
            {
                BookIdz = bookIdz
            };
        }

        public async Task<IEnumerable<BookOutput>> GetBooksByIdzAsync(IEnumerable<long> bookIdz)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var books = await connection.QueryAsync<BookQueryOutput>(
                $@"
                    SELECT [BookId] AS [{nameof(BookQueryOutput.BookId)}], 
                           [LastModified] AS [{nameof(BookQueryOutput.LastModified)}],
                           [BookInfo] AS [{nameof(BookQueryOutput.BookInfo)}]
                    FROM [dbo].[Book]
                    WHERE [BookId] IN @Idz
                ",
                new
                {
                    Idz = bookIdz
                });

            return books.Select(GetBookOutput).ToList();
        }

        private static BookOutput GetBookOutput(BookQueryOutput book)
        {
            var bookInfo = JsonConvert.DeserializeObject<BookInfo>(book.BookInfo);

            return new BookOutput
            {
                BookId = book.BookId,
                LastModified = book.LastModified,
                BookTitle = bookInfo.BookTitle,
                BookDescription = bookInfo.BookDescription,
                Author = bookInfo.Author,
                PublishDate = bookInfo.PublishDate,
                CoverBase64 = bookInfo.CoverBase64
            };
        }

        private sealed record BookQueryOutput
        {
            public long BookId { get; init; }
            public DateTime LastModified { get; init; }
            public string BookInfo { get; init; }
        }
    }
}
