namespace PhotographyWorkshops.Models.Dtos
{
    using System.Collections.Generic;

    public class WorkshopDto
    {
        public string Name { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Location { get; set; }

        public string Price { get; set; }

        public string TrainerFirstName { get; set; }

        public string TrainerLastName { get; set; }

        public List<List<string>> Participants { get; set; } = new List<List<string>>();
    }
}
