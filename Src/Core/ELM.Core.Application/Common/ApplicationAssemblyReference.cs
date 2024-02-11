using System.Reflection;

namespace ELM.Core.Application.Common
{
    public static class ApplicationAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
        public static readonly string NameSpace = Assembly.GetName().Name;
    }
}
