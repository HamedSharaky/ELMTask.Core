using CSharpFunctionalExtensions;

namespace ELM.Core.Domain.Common;

public abstract class CreationAuditedEntity<TId>
    : Entity<TId>, ICreationInfo where TId : IComparable<TId>
{
    public virtual int? CreatedBy { get; protected set; }
    public virtual DateTime? CreationDate { get; protected set; }

    protected CreationAuditedEntity()
    {
    }

    protected CreationAuditedEntity(TId id)
        : base(id)
    {
    }
}
