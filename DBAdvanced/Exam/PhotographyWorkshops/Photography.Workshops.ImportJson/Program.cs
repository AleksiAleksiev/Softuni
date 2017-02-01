namespace Photography.Workshops.ImportJson
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;

    using AutoMapper;

    using Newtonsoft.Json;

    using PhotographyWorkshops.Data;
    using PhotographyWorkshops.Data.Interfaces;
    using PhotographyWorkshops.Models;
    using PhotographyWorkshops.Models.Dtos;

    public class Program
    {
        private const string LensPath = "../../../import/lenses.json";

        private const string CameraPath = "../../../import/cameras.json";

        private const string PhotographerPath = "../../../import/photographers.json";

        private const string ErrorMessage = "Error. Invalid data provided";

        private static readonly IWriter Writer = new ConsoleWriter();

        private static Random rng = new Random();


        public static void Main()
        {
            IUnitOfWork unit = new UnitOfWork(new PhotographyWorkshopsContext());
            ConfigureMapper();
            ImportLenses(unit);
            //ImportCameras(unit);
            //ImportPhotographers(unit);
        }

        private static void ConfigureMapper()
        {
            Mapper.Initialize(
                expression =>
                    {
                        expression.CreateMap<CameraDto, DslrCamera>();
                        expression.CreateMap<CameraDto, MirrorlessCamera>();
                        expression.CreateMap<PhotographerDto, Photographer>()
                            .ForMember(photographer => photographer.Lenses, opt => opt.Ignore());
                    });
        }

        private static void ImportPhotographers(IUnitOfWork unit)
        {
            var json = File.ReadAllText(PhotographerPath);
            var photographerDtos = JsonConvert.DeserializeObject<IEnumerable<PhotographerDto>>(json);
            var cameras = unit.Cameras.GetAll().ToList();
            foreach (var photographerDto in photographerDtos)
            {
                if (photographerDto.FirstName == null || photographerDto.LastName == null)
                {
                    Writer.WriteLine(ErrorMessage);
                    continue;
                }

                var photographerEntity = Mapper.Map<Photographer>(photographerDto);
                photographerEntity.PrimaryCamera = cameras[rng.Next(0, cameras.Count)];
                photographerEntity.SecondaryCamera = cameras[rng.Next(0, cameras.Count)];
                foreach (var index in photographerDto.Lenses)
                {
                    var lens = unit.Lenses.Find(index);
                    if (lens == null 
                        || (lens.CompatibleWith != photographerEntity.PrimaryCamera.Make && lens.CompatibleWith != photographerEntity.SecondaryCamera.Make))
                    {
                        continue;
                    }

                    photographerEntity.Lenses.Add(lens);
                }

                try
                {
                    unit.Photographers.Add(photographerEntity);
                    unit.Commit();
                    Writer.WriteLine($"Successfully imported {photographerEntity.FirstName} {photographerEntity.LastName} | Lenses: {photographerEntity.Lenses.Count}");
                }
                catch (DbEntityValidationException ex)
                {
                    unit.Photographers.Remove(photographerEntity);
                    Writer.WriteLine(ErrorMessage);
                }
            }
        }

        private static void ImportCameras(IUnitOfWork unit)
        {
            var json = File.ReadAllText(CameraPath);
            var cameraDtos = JsonConvert.DeserializeObject<IEnumerable<CameraDto>>(json);
            foreach (var cameraDto in cameraDtos)
            {
                if (cameraDto.Type == "DSLR")
                {
                    var dslrEntity = Mapper.Map<DslrCamera>(cameraDto);
                    try
                    {
                        unit.Cameras.Add(dslrEntity);
                        unit.Commit();
                        Writer.WriteLine($"Successfully imported DSLR {dslrEntity.Make} {dslrEntity.Model}");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        unit.Cameras.Remove(dslrEntity);
                        Writer.WriteLine(ErrorMessage);
                    }
                }
                else if (cameraDto.Type == "Mirrorless")
                {
                    var mirrorlessEntity = Mapper.Map<MirrorlessCamera>(cameraDto);
                    try
                    {
                        unit.Cameras.Add(mirrorlessEntity);
                        unit.Commit();
                        Writer.WriteLine($"Successfully imported Mirrorless {mirrorlessEntity.Make} {mirrorlessEntity.Model}");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        unit.Cameras.Remove(mirrorlessEntity);
                        Writer.WriteLine(ErrorMessage);
                    }
                }
                else
                {
                    Writer.WriteLine(ErrorMessage);
                }
            }
        }

        private static void ImportLenses(IUnitOfWork unit)
        {
            var json = File.ReadAllText(LensPath);
            var lenses = JsonConvert.DeserializeObject<IEnumerable<Lens>>(json);
            foreach (var lense in lenses)
            {
                unit.Lenses.Add(lense);
                Writer.WriteLine($"Successfully imported {lense.Make} {lense.FocalLength}mm f{lense.MaxAperture}");
            }

            unit.Commit();
        }
    }
}
