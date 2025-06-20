using System.ComponentModel.DataAnnotations;

namespace FirstMVCApp.CustomAttributes.ValidationAttributes
{
    public class JourDeSemaineAttribute : ValidationAttribute
    {
        public JourDeSemaineAttribute(bool samediAuthorise,
                string errorSamediAutorise = "{0} tous les jours sauf le dimanche",
                 string errorSamediPasAutorise = "{0} tous les jours sauf le dimanche et le samedi"
                )
        {
            SamediAuthorise = samediAuthorise;
            ErrorSamediAutorise = errorSamediAutorise;
            ErrorSamediPasAutorise = errorSamediPasAutorise;
        }

        public bool SamediAuthorise { get; }
        public string ErrorSamediAutorise { get; }
        public string ErrorSamediPasAutorise { get; }

        public override bool IsValid(object? value)
        {
            if(value is null)
            {
                return true;
            }
            if(value is DateTime d)
            {
                if (d.DayOfWeek == DayOfWeek.Sunday) return false;
                if (d.DayOfWeek == DayOfWeek.Saturday && !this.SamediAuthorise) return false;
                return true;

            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            if (SamediAuthorise)
            {
                return string.Format(ErrorSamediAutorise,name);
            }
            else
            {
                return string.Format(ErrorSamediPasAutorise, name);
            }
        }
    }
}
