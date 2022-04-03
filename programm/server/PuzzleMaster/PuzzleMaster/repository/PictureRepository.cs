using Dapper;
using PuzzleMaster.model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace PuzzleMaster.repository
{
    public class PictureRepository : IPictureRepository
    {

        private string connectionString;

        public PictureRepository()
        {
            connectionString = ConfigLoader.loadConnectionString();
        }

        public void addPicture(byte[] picture)
        {
            using (IDbConnection con = new SQLiteConnection(connectionString))
            {
                con.Execute("INSERT INTO picture (picture) VALUES (@picture)", new { picture });
            }
        }

        public List<Picture> getAllPictures()
        {
            using (IDbConnection con = new SQLiteConnection(connectionString))
            {
                var output = con.Query<Picture>("SELECT * FROM picture");
                return output.ToList();
            }
        }

        public Picture getPictureById(uint pictureId)
        {
            using (IDbConnection con = new SQLiteConnection(connectionString))
            {
                var output = con.Query<Picture>("SELECT * FROM picture WHERE id = @pictureId", new { pictureId });
                if(output.Count() > 0)
                    return output.Single();
                return null;
            }
        }
    }
}
