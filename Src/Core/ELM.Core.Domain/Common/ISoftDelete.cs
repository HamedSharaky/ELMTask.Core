namespace ELM.Core.Domain.Common
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }
    }
}
