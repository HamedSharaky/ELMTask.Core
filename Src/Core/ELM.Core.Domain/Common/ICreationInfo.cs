namespace ELM.Core.Domain.Common;

public interface ICreationInfo
{
    int? CreatedBy { get; }
    DateTime? CreationDate { get; }
}
