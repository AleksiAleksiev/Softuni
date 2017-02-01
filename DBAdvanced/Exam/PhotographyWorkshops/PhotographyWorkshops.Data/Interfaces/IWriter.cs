namespace PhotographyWorkshops.Data.Interfaces
{
    public interface IWriter
    {
        void WriteLine(string param, params object[] args);

        void WriteLine(object param);
    }
}
