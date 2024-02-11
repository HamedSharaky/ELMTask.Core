namespace ELM.Core.Domain.Common;

public abstract class AuditedEntity<TId>
    : CreationAuditedEntity<TId>, IModificationInfo where TId : IComparable<TId>
{
    public virtual int? LastModifiedBy { get; protected set; }
    public virtual DateTime? LastModificationDate { get; protected set; }

    protected AuditedEntity()
    {
    }

    protected AuditedEntity(TId id)
        : base(id)
    {
    }
}
