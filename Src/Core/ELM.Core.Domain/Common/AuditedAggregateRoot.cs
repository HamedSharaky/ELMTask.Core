namespace ELM.Core.Domain.Common;

public abstract class AuditedAggregateRoot<TId>
    : CreationAuditedAggregateRoot<TId>, IModificationInfo where TId : IComparable<TId>
{
    public virtual int? LastModifiedBy { get; protected set; }
    public virtual DateTime? LastModificationDate { get; protected set; }

    protected AuditedAggregateRoot()
    {
    }

    protected AuditedAggregateRoot(TId id)
        : base(id)
    {
    }
}
