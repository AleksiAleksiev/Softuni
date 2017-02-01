namespace PhotographyWorkshops.ImportXml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using PhotographyWorkshops.Data;
    using PhotographyWorkshops.Data.Interfaces;
    using PhotographyWorkshops.Models;
    using PhotographyWorkshops.Models.Dtos;

    public class Program
    {
        private const string AccessoriesPath = "../../../import/accessories.xml";

        private const string WorkshopsPath = "../../../import/workshops.xml";

        private const string ErrorMessage = "Error. Invalid data provided";

        private static readonly IWriter Writer = new ConsoleWriter();

        private static Random rng = new Random();


        public static void Main()
        {
            IUnitOfWork unit = new UnitOfWork(new PhotographyWorkshopsContext());
            ImportAccessories(unit);
            //ImportWorkshops(unit);
        }

        private static void ImportWorkshops(IUnitOfWork unit) // Would refactor but out of time :(
        {
            XDocument xml = XDocument.Load(WorkshopsPath);
            List<WorkshopDto> dtos = new List<WorkshopDto>();
            foreach (var workshop in xml.Descendants("workshop"))
            {
                if (workshop.Attribute("name") == null
                    || workshop.Attribute("price") == null
                    || workshop.Attribute("location") == null
                    || !workshop.Descendants("trainer").Any())
                {
                    Writer.WriteLine(ErrorMessage);
                    continue;
                }
                var trainerNames = workshop.Descendants("trainer").First().Value.Split(' ');
                var dto = new WorkshopDto
                {
                    Name = workshop.Attribute("name").Value,
                    Price = workshop.Attribute("price").Value,
                    Location = workshop.Attribute("location").Value,
                    TrainerFirstName = trainerNames[0],
                    TrainerLastName = trainerNames[1],
                    StartDate = workshop.Attribute("start-date")?.Value,
                    EndDate = workshop.Attribute("end-date")?.Value
                };

                if (workshop.Descendants("participant").Any())
                {
                    foreach (var participant in workshop.Descendants("participant"))
                    {
                        dto.Participants.Add(new List<string>
                        {
                            participant.Attribute("first-name").Value,
                            participant.Attribute("last-name").Value
                        });
                    }
                }

                dtos.Add(dto);
            }

            foreach (var dto in dtos)
            {
                var workshopEntity = new Workshop
                {
                    Name = dto.Name,
                    PricePerParticipant = decimal.Parse(dto.Price),
                    Location = dto.Location,
                    Trainer = unit.Photographers
                                             .FirstOrDefault(photographer => photographer.FirstName == dto.TrainerFirstName && photographer.LastName == dto.TrainerLastName)
                };
                workshopEntity.StartDate = (dto.StartDate != null) ? DateTime.Parse(dto.StartDate) : (DateTime?)null;
                workshopEntity.EndDate = (dto.EndDate != null) ? DateTime.Parse(dto.EndDate) : (DateTime?)null;
                foreach (var dtoParticipant in dto.Participants)
                {
                    var firstName = dtoParticipant[0];
                    var lastName = dtoParticipant[1];
                    var participant =
                        unit.Photographers.FirstOrDefault(
                            photographer =>
                                photographer.FirstName == firstName
                                && photographer.LastName == lastName);
                    workshopEntity.Participants.Add(participant);
                }

                unit.Workshops.Add(workshopEntity);
                Writer.WriteLine($"Successfully imported {workshopEntity.Name}");
            }

            unit.Commit();

        }

        private static void ImportAccessories(IUnitOfWork unit)
        {
            XDocument xml = XDocument.Load(AccessoriesPath);
            var photographers = unit.Photographers.GetAll().ToList();
            foreach (var accessory in xml.Descendants("accessory"))
            {
                var accessoryEntity = new Accessory() { Name = accessory.Attribute("name")?.Value };
                unit.Accessories.Add(accessoryEntity);
                Writer.WriteLine($"Successfully imported {accessoryEntity.Name}");
                var owner = photographers[rng.Next(0, photographers.Count)];
                accessoryEntity.Owner = owner;
            }

            unit.Commit();
        }
    }
}
