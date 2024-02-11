using AutoMapper;
using ELM.Core.Application.Common.Configuration.Queries;
using ELM.Core.Application.Common.Exceptions;
using ELM.Core.Application.Common.Interfaces;
using ELM.Core.Common.Configurations;
using ELM.Core.Domain.Books;
using ELM.Core.Domain.Common.Dtos.Cache;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ELM.Core.Application.Books.Search
{
    internal sealed class SearchBookQueryHandler : IQueryHandler<SearchBookQuery, SearchBookQueryOutput>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICacheService _cacheService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SearchBookQueryHandler(IBookRepository bookRepository,
            ICacheService cacheService,
            IConfiguration configuration,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _cacheService = cacheService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<SearchBookQueryOutput> Handle(SearchBookQuery request, CancellationToken cancellationToken)
        {
            var searchToken = GetBookSearchCacheKey(request.SearchTerm);
            var searchCacheData = new BookSearchCacheDto();

            if (_cacheService.IsCacheEntryExist(searchToken))
            {
                searchCacheData = _cacheService.GetCacheEntry<BookSearchCacheDto>(searchToken);
            }
            else
            {
                searchCacheData = await _bookRepository.SearchBookAsync(request.SearchTerm);
                
                var cacheDuration = _configuration.GetConfiguration<CommonKeys, int>(k => k.BookSearchCacheDurationInMinutes);

                _= _cacheService.CreateCacheEntryAsync(searchToken, searchCacheData, DateTime.Now.AddMinutes(cacheDuration));
            }

            if (searchCacheData.BookIdz?.Any() != true)
            {
                return new SearchBookQueryOutput();
            }

            var totalItems = searchCacheData.BookIdz.Count();

            var currentPageBookIdz = searchCacheData.BookIdz
                .Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            var books = await _bookRepository.GetBooksByIdzAsync(currentPageBookIdz);

            return new SearchBookQueryOutput
            {
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize),
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                Books = _mapper.Map<IList<BookInListDto>>(books)
            };
        }

        private static string GetBookSearchCacheKey(string searchTerm)
        {
            return CacheKeysNames.BookSearch + searchTerm.Trim();
        }
    }
}
