using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace PuzzleMaster
{
    // Diese ganze Klasse hat der Patrick gemacht
    class Gamelogic
    {

        public PuzzlePiece[,] puzzlepieces;

        public Point FreeField;

        public uint currentMoves = 0;

        public Gamelogic(PuzzlePiece[,] IncomingArray, Point IncomingField)
        {
            this.puzzlepieces = IncomingArray;
            this.FreeField = IncomingField;
        }

        public Gamelogic() { }

        public bool IsSwapable(Point PieceToSwap)
            //überprüft ob die Puzzleteile nebeneinander liegen und überhaut getauscht werden können => falls ja gibt TRUE zurück
        {
            Debug.WriteLine($"{FreeField}");
            if (FreeField.X >= 0 && FreeField.Y >= 0 && PieceToSwap.X >= 0 && PieceToSwap.Y >= 0)
            {
                // vergleichen die X/Y-Werte voneinander -> wenn == 0 dann liegen die Felder in der Gleichen Spalte/Zeile
                //                                       -> wenn == 1 dann liegen die Felder aneinander angrenzenden Spalten/Zeile
                //                                       -> wenn > 1 dann liegen die Felder zu weit auseinander
                // die Felder müssen entweder in der gleichen Zeile oder Spalte liegen und beim Anderen genau einen Abstand von 1 haben muss
                if (Math.Abs(FreeField.X - PieceToSwap.X) + Math.Abs(FreeField.Y - PieceToSwap.Y) == 1)
                {
                    Debug.WriteLine("Tausch möglich");
                    return true;
                }
            }
            Debug.WriteLine("Tausch nicht möglich");
            return false;
        }

        public bool SwapPuzzlepiece(Point newFreeField)       // newFreeField ist das Puzzleteil auf das der Anwender clickt
            //tauscht die Puzzleteile und gibt nach dem Tausch TRUE zurück
        {
            if (IsSwapable(newFreeField)) // nur nocheinmal überprüfen Redundanz schaffen (besser ein mal mehr geprüft als einmal zu wenig)
            {
                // Zwischenspeicherobjekt(cache) erzeugen
                PuzzlePiece cache = new PuzzlePiece();
                    // cache Attribute von newFreeField zuweisen (wäre auch in nem einzeiler gegangen und gleich mit den richtigen Attributen zu erzeugen so ist aber übersichtlicher
                    cache.setPicture(puzzlepieces[(int)newFreeField.X, (int)newFreeField.Y].getPicture());
                    cache.setOriginalPos(puzzlepieces[(int)newFreeField.X, (int)newFreeField.Y].getOriginalPos());
                // überschreiben von newFreeField mit FreeField
                puzzlepieces[(int)newFreeField.X, (int)newFreeField.Y].setPicture(puzzlepieces[(int)FreeField.X, (int)FreeField.Y].getPicture());
                puzzlepieces[newFreeField.X, newFreeField.Y].setOriginalPos(puzzlepieces[FreeField.X, FreeField.Y].getOriginalPos());
                // Überschreiben von FreeField mit cache(enthält die Daten vom vorher dort plazierten PuzzlePiece)
                puzzlepieces[FreeField.X, FreeField.Y].setPicture(cache.getPicture());
                puzzlepieces[FreeField.X, FreeField.Y].setOriginalPos(cache.getOriginalPos());
                FreeField.X = newFreeField.X;
                FreeField.Y = newFreeField.Y;
                this.currentMoves += 1;
                //Debug.WriteLine($"FreeField {FreeField} - newFreeField {newFreeField}");
                //Debug.WriteLine("Array geändert");
                return true;
            }
            return false;
        }
        public bool IsGameFinished()
            //überprüft ob alle Teile am richtigen Ort sind um zu bestimmen on das Spiel fertig ist
        {
            foreach (PuzzlePiece Piece in puzzlepieces) // durchläuft jedes Element des Arrays
            {
                // überprüft ob an der OrginalPosition des PuzzlePieces, das PuzzlePiece liegt 
                // fals nicht gibt false zurück da nich alle PuzzlePieces an der richtigen Position sind
                // falls die Schleife komplett durchlaufen wird sind alle an der richtigen Position -> return true am ende der Funktion
                if (!(puzzlepieces[Piece.getOriginalPos().X, Piece.getOriginalPos().Y] == Piece))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
