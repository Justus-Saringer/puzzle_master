using Dapper;
using PuzzleMaster.model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace PuzzleMaster.repository
{
    public class HighscoreRepository : IHighscoreRepository
    {

        private string connectionString;

        public HighscoreRepository()
        {
            connectionString = ConfigLoader.loadConnectionString();
        }

        public void addHighscore(string name, uint moveCount, uint fieldSize)
        {
            addHighscore(new Highscore(name, moveCount, fieldSize));
        }

        public void addHighscore(Highscore highscore)
        {
            using (IDbConnection con = new SQLiteConnection(connectionString))
            {
                con.Execute("INSERT INTO Highscore (name, moves, size) VALUES (@name, @moves, @size)", highscore);
            }
        }

        public List<Highscore> getAllHighscores()
        {
            using (IDbConnection con = new SQLiteConnection(connectionString))
            {
                var output = con.Query<Highscore>("SELECT * FROM Highscore ORDER BY moves");
                return output.ToList();
            }
        }

        public List<Highscore> getHighscoreBySize(uint fieldSize)
        {
            using (IDbConnection con = new SQLiteConnection(connectionString))
            {
                var output = con.Query<Highscore>("SELECT * FROM Highscore WHERE size = @fieldSize ORDER BY moves", new { fieldSize });
                return output.ToList();
            }
        }
    }
}
