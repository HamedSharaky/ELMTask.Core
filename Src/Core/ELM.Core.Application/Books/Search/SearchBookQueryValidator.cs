using FluentValidation;

namespace ELM.Core.Application.Books.Search
{
    public sealed class SearchBookQueryValidator : AbstractValidator<SearchBookQuery>
    {
        public SearchBookQueryValidator()
        {
            RuleFor(r => r.SearchTerm).NotEmpty();
            RuleFor(e => e.PageSize).GreaterThan(0);
            RuleFor(e => e.Page).GreaterThan(0);
        }
    }
}
