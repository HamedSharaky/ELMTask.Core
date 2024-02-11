using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace ELM.Core.Common.Configurations;

public static class ConfigurationsExtensions
{
    public static TValue GetConfiguration<TKey, TValue>(
        this IConfiguration configuration,
        Expression<Func<TKey, string>> keyName)
    {
        var keyNameString = GetKeyFullName(keyName);

        var section = configuration.GetSection(keyNameString);

        if (section is null)
        {
            return default;
        }

        return (TValue)Convert.ChangeType(section.Value, typeof(TValue));
    }

    private static string GetKeyFullName<T>(Expression<Func<T, string>> keyName)
    {
        var fullName = keyName.Parameters[0].Type.FullName;

        fullName = fullName.Substring(fullName.LastIndexOf(".") + 1).Replace("+", ".") + "." +
               keyName.Body.ToString().Substring(keyName.Body.ToString().IndexOf(".") + 1);

        return fullName;
    }
}
