namespace ELM.Core.Application.Common.Exceptions
{
    public sealed class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(string validationMessage)
            : base(validationMessage)
        {
        }

        public BusinessRuleValidationException(IEnumerable<string> validationMessages)
            : base(string.Join(Environment.NewLine, validationMessages))
        {
        }
    }
}
