namespace PuzzleMaster.model
{
    public class Highscore
    {

        public uint id { get; set; }

        public string name { get; set; }

        public uint moves { get; set; }

        public uint size { get; set; }

        public Highscore() { }

        public Highscore(string name, uint moves, uint size)
        {
            this.name = name;
            this.moves = moves;
            this.size = size;
        }

        public Highscore(uint id, string name, uint moves, uint size)
        {
            this.id = id;
            this.name = name;
            this.moves = moves;
            this.size = size;
        }
    }
}
