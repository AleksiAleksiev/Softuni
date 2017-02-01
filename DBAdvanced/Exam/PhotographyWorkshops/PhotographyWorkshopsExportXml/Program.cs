namespace PhotographyWorkshopsExportXml
{
    using System.Linq;
    using System.Xml.Linq;

    using PhotographyWorkshops.Data;
    using PhotographyWorkshops.Data.Interfaces;

    public class Program
    {
        public static void Main()
        {
            IUnitOfWork unit = new UnitOfWork(new PhotographyWorkshopsContext());
            ExportPhotographersWithSameCameraMake(unit);
            ExportWorkshopsByLocation(unit);
        }

        private static void ExportWorkshopsByLocation(IUnitOfWork unit)
        {
            var workshops =
                unit.Workshops.GetAll()
                    .Where(workshop => workshop.Participants.Count >= 5)
                    .GroupBy(workshop => workshop.Location)
                    .ToList();
            var xml = new XElement("locations");
            foreach (var location in workshops)
            {
                var locationXml = new XElement("location");
                locationXml.SetAttributeValue("name", location.Key);
                foreach (var workshop in location)
                {
                    var work = new XElement("workshop");
                    var totalProfit = 0.8M * (workshop.Participants.Count * workshop.PricePerParticipant);
                    work.SetAttributeValue("name", workshop.Name);
                    work.SetAttributeValue("total-profit", totalProfit);
                    var participants = new XElement("participants");
                    participants.SetAttributeValue("count", workshop.Participants.Count);
                    foreach (var workshopParticipant in workshop.Participants)
                    {
                        var part = new XElement("particpant");
                        part.Value = workshopParticipant.FirstName + " " + workshopParticipant.LastName;
                        participants.Add(part);
                    }

                    work.Add(participants);
                    locationXml.Add(work);
                }

                xml.Add(locationXml);
            }

            xml.Save("../../../exports/workshops-by-location.xml");
        }

        private static void ExportPhotographersWithSameCameraMake(IUnitOfWork unit)
        {
            var photographers =
                unit.Photographers.GetAll()
                    .Where(
                        photographer =>
                            photographer.PrimaryCamera.Make == photographer.SecondaryCamera.Make
                            && photographer.Lenses.Count > 0)
                    .Select(photographer => new
                    {
                        Name = photographer.FirstName + " " + photographer.LastName,
                        PrimaryCamera = photographer.PrimaryCamera.Make + " " + photographer.PrimaryCamera.Model,
                        Lenses = photographer.Lenses.Select(lens => lens.Make + " " + lens.FocalLength + "mm f" + lens.MaxAperture.Value)
                    })
                    .ToList();
            var xml = new XElement("photographers");
            foreach (var photographer in photographers)
            {
                var photograph = new XElement("photographer");
                photograph.SetAttributeValue("name", photographer.Name);
                photograph.SetAttributeValue("primary-camera", photographer.PrimaryCamera);
                var lenses = new XElement("lenses");
                foreach (var lense in photographer.Lenses)
                {
                    var lens = new XElement("lens");
                    lens.SetValue(lense);
                    lenses.Add(lens);
                }

                photograph.Add(lenses);
                xml.Add(photograph);
            }

            xml.Save("../../../exports/same-cameras-photographers.xml");
        }
    }
}
