

namespace Epicor.Infraestructure.Data
{
    public static class DBContext
    {
        private static string _connectionString =
            "Driver={SQL Server};Server=128.17.50.183;Database=EpicorITSMApplication;Uid=Indicador;Pwd=50p0rte2013;";

        public static string GetConnectionString { get { return _connectionString; } }
    }
}
