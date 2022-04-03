using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Data;
using System.Windows;
using System.Windows.Media;
using Point = System.Drawing.Point;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.Windows.Navigation;
using System.Diagnostics;
using System.IO;

namespace PuzzleMaster
{
    // Diese ganze Klasse hat der Patrick gemacht
    class Gamecontrol
    {

        // Globale Variablen
        public static Gamelogic currentGame = new Gamelogic();
        public static string PlayersName = "DuHastVergessenDeinenNamenAnzugeben123";
        // Einmaliges erstellen von Objekten um  auf die Funktionen zuzugreifen da die Funktionen nicht statisch (damit nicht immer neue Objekte erzeigt werden müssen)
        public static RestRequest restRequest = new RestRequest();
        public static GameSettings gs = new GameSettings();
        public static GamePage gp = new GamePage();


        public static List<HighscoreEntry> LoadHighscoreList(object sender, EventArgs e, uint size)
        // sendet Anfrage an RestRequest für HighScoreListe und ruft Funktion zum darstellen auf
        {
            return restRequest.highscoreGetAllFromFieldSize(size);
        }


        public static void LoadGame(uint size, Point FreeField, uint ImageID)
        // Funktion wird an Objekt(Game), das in der Main von der Klasse Gamekcontrol erstellt wurde, aufgerufen
        // danach lässt es die einzelnen Bilder sichtbar machen
        {
            // übergibt Spieleinstellungen an den Server (bekommt PuzzlePiece[,] zrück) und setzt Gamelogic-Eigenschaften von Game 
            currentGame.puzzlepieces = restRequest.puzzleGet(size, ImageID, FreeField);
            // zurücksetzten der Züge
            currentGame.currentMoves = 0;
            // setzten der Position und Bild des Freien Feldes
            currentGame.FreeField = FreeField;
        }


        public static void LoadMove(Point PieceToMove)
        {
            //Debug.WriteLine("Load Move");
            if (currentGame.IsSwapable(PieceToMove)) currentGame.SwapPuzzlepiece(PieceToMove);
            gp.MoveCounter.Text = currentGame.currentMoves.ToString();
            if(currentGame.IsGameFinished()) EndGame();
        }

        public static void EndGame()
        // ruft überprüfende Funktion ob spiel fertig
        // dann regelt das was geschieht wenn das Spiel fertig gespielt wurde
        // die Funktion des Highscore eintragens ist acuh hier integriert
        {
            int Size = currentGame.puzzlepieces.GetLength(0);
            restRequest.highscoreAdd(currentGame.currentMoves, PlayersName, (uint)Size);
            MessageBox.Show($"Herzlichen Glückwunsch \n{PlayersName} hat das Puzzle der Größe {Size}x{Size} in {currentGame.currentMoves} Zügen gelöst.");
        }

        // Die Funktion VizualizeMiniPicture wurde in Gamesettings.xaml.cs als __void LoadGamesettings()__ ausgelagert
        // Die Funktion VizualizePuzzlePieces wurde in Gamepage.xaml.cs als __public void VizualizePuzzle()__ ausggelagert
        // Die Funktion VizualizeHighscoreList wurde in HighscorePage.xaml.cs als
        //    __private void HighscoreCBoxPSize_SelectionChanged(object sender, SelectionChangedEventArgs e)__ ausgelagert
    }
}


