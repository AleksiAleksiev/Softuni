namespace PhotographyWorkshops.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class PhotographerPhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string number = value as string;
            return Regex.IsMatch(number, "^\\+\\d{1,3}\\/\\d{8,10}$");
        }
    }
}
