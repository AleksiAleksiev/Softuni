namespace PhotographyWorkshops.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class IsoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int? iso = value as int?;
            return iso >= 100;
        }
    }
}
