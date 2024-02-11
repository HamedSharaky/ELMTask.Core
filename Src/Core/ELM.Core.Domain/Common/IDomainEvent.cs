using MediatR;

namespace ELM.Core.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
