using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PuzzleMaster.model
{
    public class PuzzleBuilder
    {

        /** <summary>Creates all puzzlepieces with the given picture.</summary>
         *  <param name="size">One-dimensional lenght of the squared gamefield.</param>
         *  <param name="picture">Picture that will be used to create the puzzle pieces</param> 
         *  <param name="freeField">X/Y position of the puzzle piece which does not contain a picture. X/Y count starts at 0.</param> */
        public static Puzzlepiece[][] createPuzzle(uint size, Picture picture, Point freeField)
        {
            Puzzlepiece[][] puzzle = cropPictureInPieces(picture, (int)size);

            puzzle = shuffelPieces(puzzle);
            puzzle[freeField.X][freeField.Y].picture = null;

            return puzzle;
        }

        /** <summary>Cropes a picture into pieces</summary>
         *  <param name="piecesPerDirection">Number of pieces that will be created for every direction.</param> */
        private static Puzzlepiece[][] cropPictureInPieces(Picture picture, int piecesPerDirection)
        {
            Puzzlepiece[][] pieces = new Puzzlepiece[piecesPerDirection][];

            Image image = picture.getPictureAsImage();
            int pieceSize = image.Width / piecesPerDirection;
            for(int x = 0; x < piecesPerDirection; x++)
            {
                pieces[x] = new Puzzlepiece[piecesPerDirection];
                for (int y = 0; y < piecesPerDirection; y++)
                {
                    Picture cropedPicture = picture.copy();
                    cropedPicture.cropPicture(new Rectangle(pieceSize * x, pieceSize * y, pieceSize, pieceSize));
                    Puzzlepiece piece = new Puzzlepiece(new Point(x, y), cropedPicture.picture);
                    pieces[x][y] = piece;
                }
            }

            return pieces;
        }

        /** <summary>Shuffels the given array</summary> */
        private static Puzzlepiece[][] shuffelPieces(Puzzlepiece[][] puzzlePieces)
        {
            List<Puzzlepiece> sortedPieces = puzzlePieces.SelectMany(T => T).ToList();
            Random rnd = new Random();

            for (int x = 0; x < puzzlePieces.Length; x++)
            {
                for (int y = 0; y < puzzlePieces[x].Length; y++)
                {
                    int pos = rnd.Next(sortedPieces.Count);
                    puzzlePieces[x][y] = sortedPieces.ElementAt(pos);
                    sortedPieces.RemoveAt(pos);
                }
            }

            return puzzlePieces;
        }

    }
}
