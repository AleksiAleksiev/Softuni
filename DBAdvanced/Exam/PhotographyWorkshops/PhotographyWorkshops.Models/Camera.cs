namespace PhotographyWorkshops.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PhotographyWorkshops.Models.Attributes;

    public abstract class Camera
    {
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public bool? IsFullFrame { get; set; }

        [Iso]
        public int MinIso { get; set; }

        public int? MaxIso { get; set; }

        [InverseProperty("PrimaryCamera")]
        public virtual ICollection<Photographer> PhotographersPrimaryCamera { get; set; } = new HashSet<Photographer>();

        [InverseProperty("SecondaryCamera")]
        public virtual ICollection<Photographer> PhotographersSecondaryCamera { get; set; } = new HashSet<Photographer>();
    }
}
