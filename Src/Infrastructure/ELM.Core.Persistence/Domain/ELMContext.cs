namespace ELM.Core.Persistence.Domain;

internal static class ELMContext
{
    public static readonly string DEFAULT_SCHEMA = "dbo";

    public static class Views
    {
        public static readonly string Book = "vwBook";
    }

    public static class Functions
    {
    }
}
