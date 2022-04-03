using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleMaster
{
    // Das hat der Patrick gemacht
    public class HighscoreEntry
    {
        public int position { get; set; }

        public int moves { get; set; }

        public int size { get; set; }

        public string name { get; set; }

        public HighscoreEntry(int moves, int size, string name)
        {
            this.moves = moves;
            this.size = size;
            this.name = name;
        }
    }
}
