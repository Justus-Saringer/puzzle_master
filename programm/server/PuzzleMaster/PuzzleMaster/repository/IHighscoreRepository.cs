using PuzzleMaster.model;
using System.Collections.Generic;

namespace PuzzleMaster.repository
{
    public interface IHighscoreRepository
    {

        public void addHighscore(string name, uint moveCount, uint fieldSize);

        public List<Highscore> getAllHighscores();

        public List<Highscore> getHighscoreBySize(uint FieldSize);

    }
}
