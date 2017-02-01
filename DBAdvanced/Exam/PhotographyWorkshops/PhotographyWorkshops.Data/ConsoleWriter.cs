namespace PhotographyWorkshops.Data
{
    using System;

    using PhotographyWorkshops.Data.Interfaces;

    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string param, params object[] args)
        {
            Console.WriteLine(param, args);
        }

        public void WriteLine(object param)
        {
            Console.WriteLine(param);
        }
    }
}
