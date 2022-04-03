using Dapper;
using PuzzleMaster.model;
using PuzzleMaster.repository;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace PuzzleMaster
{
    public class DatabaseUtil
    {

        public static void createDatabaseIfNotExist()
        {
            String databaseName = ConfigLoader.loadDatabaseName();

            if (!File.Exists(databaseName))
            {
                SQLiteConnection.CreateFile(databaseName);
                using (IDbConnection con = new SQLiteConnection(ConfigLoader.loadConnectionString()))
                {
                    con.Execute("CREATE TABLE 'highscore' ('id' INTEGER NOT NULL, 'name' TEXT NOT NULL, 'moves' INTEGER NOT NULL, 'size' INTEGER NOT NULL, PRIMARY KEY('id' AUTOINCREMENT))");
                    con.Execute("CREATE TABLE 'picture' ('id' INTEGER NOT NULL, 'picture' BLOB NOT NULL, PRIMARY KEY('id' AUTOINCREMENT))");
                }
                initializeDatabaseWithPictures();
            }
        }

        /** <summary>Initialize the database with predefined Pictures, that will be loaded with the URLs,
         * stored in the configuration file.</summary> */
        private static void initializeDatabaseWithPictures()
        {
            PictureRepository pictureRepository = new PictureRepository();
            foreach(String url in ConfigLoader.loadDatabasePictures())
            {
                byte[] image = loadBase64ImageFromURL(url);
                Picture picture = new Picture(image);
                pictureRepository.addPicture(picture.picture);
            }
        }

        /** <summary>Loads the Image from a URL and coverts it into a Base64 string.
         * Make sure the URL point directly to the ressource, otherwise the Image will be corrupted.</summary> */
        private static byte[] loadBase64ImageFromURL(String url)
        {
            using (WebClient client = new WebClient())
            {
                Stream stream = client.OpenRead(url);
                Bitmap bitmap = new Bitmap(stream);
                if (bitmap != null)
                {
                    MemoryStream os = new MemoryStream();
                    bitmap.Save(os, ImageFormat.Jpeg);
                    return os.ToArray();
                }
            }
            return null;
        }

    }
}
