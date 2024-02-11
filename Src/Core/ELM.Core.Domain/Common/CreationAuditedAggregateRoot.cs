namespace ELM.Core.Domain.Common;

public abstract class CreationAuditedAggregateRoot<TId>
    : AggregateRoot<TId>, ICreationInfo where TId : IComparable<TId>
{
    public virtual int? CreatedBy { get; protected set; }
    public virtual DateTime? CreationDate { get; protected set; }

    protected CreationAuditedAggregateRoot()
    {
    }

    protected CreationAuditedAggregateRoot(TId id)
        : base(id)
    {
    }
}
