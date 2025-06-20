using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace FirstMVCApp.CustomAttributes.ValidationAttributes
{
    public class EstDivisibleParAttribute : ValidationAttribute
    {
        public EstDivisibleParAttribute(int diviseur, string errorMessage) : base(errorMessage)
        {
            Diviseur = diviseur;
            ErrorMessage = errorMessage;
          
        }
        public EstDivisibleParAttribute(int diviseur) : base("{0} doit être divisible par {1}")
        {
            Diviseur = diviseur;

        }


        public int Diviseur { get; }

        public override bool IsValid(object? value)
        {
           if(value == null)
            {
                // Valide si null car attribut Required spécialisé pour ce cas
                return true;
            }
           else
            {
                if(value is int)
                {
                    return ((int)value) % Diviseur == 0;
                }
            }
            return false;
        }



        // Formatage du message d'erreur => name est le displayName
        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, name, Diviseur);
        }
    }
}
