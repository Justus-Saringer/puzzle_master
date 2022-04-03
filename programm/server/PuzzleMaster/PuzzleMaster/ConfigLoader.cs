using System.Configuration;

namespace PuzzleMaster
{
    public class ConfigLoader
    {

        public static string loadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static string loadDatabaseName()
        {
            return ConfigurationManager.AppSettings["databaseName"];
        }

        public static string[] loadDatabasePictures()
        {
            return ConfigurationManager.AppSettings["databasePictures"].Split(",");
        }


    }
}
