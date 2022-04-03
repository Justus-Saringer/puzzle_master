using System.Drawing;

namespace PuzzleMaster.model
{
    public class Puzzlepiece
    {

        public int originalPosX { get; set; }

        public int originalPosY { get; set; }

        public byte[] picture { get; set; }

        public Puzzlepiece() { }

        public Puzzlepiece(int originalPosX, int originalPosY, byte[] picture)
        {
            this.originalPosX = originalPosX;
            this.originalPosY = originalPosY;
            this.picture = picture;
        }

        public Puzzlepiece(Point originalPos, byte[] picture)
        {
            this.originalPosX = originalPos.X;
            this.originalPosY = originalPos.Y;
            this.picture = picture;
        }
    }
}
