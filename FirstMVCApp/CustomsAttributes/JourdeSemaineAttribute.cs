using System.ComponentModel.DataAnnotations;

namespace FirstMVCApp.CustomsAttributes
{
    internal class JourdeSemaineAttribute : ValidationAttribute
    {
        public bool WithSamedi { get; }

        public JourdeSemaineAttribute(bool withSamedi, string errorMessage) : base(errorMessage)
        {
            WithSamedi = withSamedi;
            ErrorMessage = errorMessage;
        }

        public JourdeSemaineAttribute(bool withSamedi) : base("{0} doit être un jour ouvré samedi {1}")
        {
            WithSamedi = withSamedi;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                if (value is DateTime mydate)
                {
                    if (WithSamedi)
                    {
                        return mydate.DayOfWeek != DayOfWeek.Sunday ;
                    }
                    else
                    {
                        return mydate.DayOfWeek != DayOfWeek.Sunday && mydate.DayOfWeek != DayOfWeek.Saturday;
                    }
                }
            }
            return false;
        }

        // Formatage du message d'erreur => name est le displayName
        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, name, WithSamedi ? "inclus" : "exclus");
        }
    }
}