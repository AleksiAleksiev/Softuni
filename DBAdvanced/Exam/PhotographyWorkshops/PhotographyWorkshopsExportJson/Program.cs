namespace PhotographyWorkshopsExportJson
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using PhotographyWorkshops.Data;
    using PhotographyWorkshops.Data.Interfaces;
    using PhotographyWorkshops.Models;

    public class Program
    {
        private const string ExportPath = "../../../exports/";

        public static void Main()
        {
            IUnitOfWork unit = new UnitOfWork(new PhotographyWorkshopsContext());
            ExportOrderedPhotographers(unit);
            ExportLandscapePhotographers(unit);
        }

        private static void ExportLandscapePhotographers(IUnitOfWork unit)
        {
            var photographers =
                unit.Photographers.GetAll()
                    .Where(
                        photographer =>
                            photographer.PrimaryCamera is DslrCamera
                            && photographer.Lenses.Count > 0
                            && photographer.Lenses.All(lens => lens.FocalLength.Value <= 30))
                    .OrderBy(photographer => photographer.FirstName)
                    .Select(
                        photographer =>
                            new
                            {
                                photographer.FirstName,
                                photographer.LastName,
                                CameraMake = photographer.PrimaryCamera.Make,
                                LensesCount = photographer.Lenses.Count
                            })
                                .ToList();
            ExportJson(photographers, "landscape-photographers.json");
        }

        private static void ExportOrderedPhotographers(IUnitOfWork unit)
        {
            var photographers =
                unit.Photographers.GetAll()
                    .OrderBy(photographer => photographer.FirstName)
                    .ThenByDescending(photographer => photographer.LastName)
                    .Select(photographer => new { photographer.FirstName, photographer.LastName, photographer.Phone })
                    .ToList();
            ExportJson(photographers, "photographers-ordered.json");
        }

        private static void ExportJson<T>(IEnumerable<T> photographers, string filename)
        {
            var json = JsonConvert.SerializeObject(photographers, Formatting.Indented);
            File.WriteAllText(ExportPath + filename, json);
        }
    }
}
