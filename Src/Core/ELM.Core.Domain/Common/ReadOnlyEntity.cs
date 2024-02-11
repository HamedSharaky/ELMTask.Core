namespace ELM.Core.Domain.Common;

public abstract class ReadOnlyEntity<TId>
{
    public virtual TId Id { get; init; }

    protected ReadOnlyEntity()
    {
    }

    protected ReadOnlyEntity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object obj)
    {
        ReadOnlyEntity<TId> entity = obj as ReadOnlyEntity<TId>;

        if ((object)entity == null)
        {
            return false;
        }

        if ((object)this == entity)
        {
            return true;
        }

        if (GetUnproxiedType(this) != GetUnproxiedType(entity))
        {
            return false;
        }

        if (IsTransient() || entity.IsTransient())
        {
            return false;
        }

        return Id.Equals(entity.Id);
    }

    private bool IsTransient()
    {
        if (Id != null)
        {
            return Id.Equals(default(TId));
        }
        return true;
    }

    public static bool operator ==(ReadOnlyEntity<TId> a, ReadOnlyEntity<TId> b)
    {
        if ((object)a == null && (object)b == null)
        {
            return true;
        }

        if ((object)a == null || (object)b == null)
        {
            return false;
        }
        return a.Equals(b);
    }

    public static bool operator !=(ReadOnlyEntity<TId> a, ReadOnlyEntity<TId> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetUnproxiedType(this).ToString() + Id).GetHashCode();
    }

    private static Type GetUnproxiedType(object obj)
    {
        Type type = obj.GetType();
        string text = type.ToString();

        if (text.Contains("Castle.Proxies.") || text.EndsWith("Proxy"))
        {
            return type.BaseType;
        }

        return type;
    }
}
