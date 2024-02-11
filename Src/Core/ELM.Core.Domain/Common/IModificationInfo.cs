namespace ELM.Core.Domain.Common;

public interface IModificationInfo
{
    int? LastModifiedBy { get; }
    DateTime? LastModificationDate { get; }
}
