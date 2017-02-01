namespace PhotographyWorkshops.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PhotographyWorkshops.Models.Attributes;

    public class Photographer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        [PhotographerPhone]
        public string Phone { get; set; }

        [Required]
        public virtual Camera PrimaryCamera { get; set; }

        [Required]
        public virtual Camera SecondaryCamera { get; set; }

        public virtual ICollection<Lens> Lenses { get; set; } = new HashSet<Lens>();

        public virtual ICollection<Accessory> Accessories { get; set; } = new HashSet<Accessory>();

        [InverseProperty("Trainer")]
        public virtual ICollection<Workshop> WorkshopsTrainer { get; set; } = new HashSet<Workshop>();

        [InverseProperty("Participants")]
        public virtual ICollection<Workshop> WorkshopsParticipant { get; set; } = new HashSet<Workshop>();


    }
}