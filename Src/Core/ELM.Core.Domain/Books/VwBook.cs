using Ardalis.GuardClauses;
using ELM.Core.Domain.Books.Inputs;
using ELM.Core.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace ELM.Core.Domain.Books
{
    public sealed class VwBook : ReadOnlyEntity<long>
    {
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastModified { get; private set; }

        private VwBook()
        {
        }
    }
}
