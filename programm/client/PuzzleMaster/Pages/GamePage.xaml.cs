using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using Point = System.Drawing.Point;

namespace PuzzleMaster
{
    /// <summary>
    /// Interaktionslogik für GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public static Uri GamePageUri = new Uri("/Pages/GamePage.xaml", UriKind.Relative);
        public static int GeneralSize = 0;
        public Dictionary<int, Point> ChildPositionToPoint = new Dictionary<int, Point>();
        
        public GamePage()
        {
            InitializeComponent();

            //CreateGameField(4);
            //FillDictionary();
            //VizualizePuzzle();
        }

        public void CreateGameField()
        {
            GameField.Children.Clear();
            //Anzahl der Felder
            int NumberOfFields = GeneralSize * GeneralSize;
            // Breite und Höhe eines Feldes
            int OneFieldHeight = (int)Math.Abs(RowHeight.ActualHeight) / GeneralSize;
            
            double positionX = 0;
            double positionY = 0;
            
            // Die einzelnen Bilder werden innerhalb des Canvas positioniert
            for (int Field = 0; Field < NumberOfFields; Field++)
            {
                Image SubImage = new Image();

                // Breite des Feldes wird eingestellt
                SubImage.Width = OneFieldHeight;
                // Höhe des Feldes wird eingestellt
                SubImage.Height = OneFieldHeight;
                // Bild passt sich der Größe des Image an
                SubImage.Stretch = Stretch.Uniform;
                // Image wird dem Canvas hinzugefügt
                GameField.Children.Add(SubImage);
                // Wird positioniert
                Canvas.SetTop(SubImage, positionY);
                Canvas.SetLeft(SubImage, positionX);

                // Es wird die erste Spalte gefüllt dann die Zweite etc.
                if (positionY > Math.Floor(GameField.ActualHeight) - (OneFieldHeight + 10))
                {
                    positionY = 0;
                    positionX += OneFieldHeight;
                }
                else
                {
                    positionY += OneFieldHeight;
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(GameSettings.GameSettingsUri);
            // Es müssen alle bereits vorhandenen Daten bereinigt werden
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void FillDictionary()
        {
            ChildPositionToPoint.Clear();
            int position = 0;
            Point coordinates = new Point(0, 0);
            
            for (int Column = 0; Column < GeneralSize; Column++)
            {
                for (int Row = 0; Row < GeneralSize; Row++)
                {
                    coordinates.X = Column;
                    coordinates.Y = Row;
                    ChildPositionToPoint.Add(position, coordinates);
                    position++;
                }
            }
        }

        //Erkennung welches Feld geklickt wurde
        private void GameField_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int OneFieldHeight = (int)GameField.ActualHeight / GeneralSize;
            int[] SpalteZeile = new int[GeneralSize];
            SpalteZeile[0] = OneFieldHeight;
            Point Position = new Point();

            // Es werden die Bereiche definiert
            for (int i = 1; i < GeneralSize; i++)
            {
                SpalteZeile[i] = OneFieldHeight * (i + 1);
            }
            // Hier wird geguckt in welchem Bereich die geklickte X Koordinate liegt
            for (int Kontrollposition = 0; Kontrollposition < GeneralSize; Kontrollposition++)
            {
                if ((int)(e.GetPosition(GameField).X) < SpalteZeile[Kontrollposition])
                {
                    // Geklickte X Koordinate des Feldes
                    Position.X = Kontrollposition;
                    break;
                }
            }
            // Hier wird geguckt in welchem Bereich die geklickte Y Koordinate liegt
            for (int Kontrollposition = 0; Kontrollposition < GeneralSize; Kontrollposition++)
            {
                if ((int)(e.GetPosition(GameField).Y) < SpalteZeile[Kontrollposition])
                {
                    // Geklickte Y Koordinte des Feldes
                    Position.Y = Kontrollposition;
                    break;
                }
            }
            Debug.WriteLine($"{Position.X} - {Position.Y}");
            Gamecontrol.LoadMove(Position);
            VizualizePuzzle();
        }


        // Diese Funktion hat der Patrick gemacht -->
        public void VizualizePuzzle()
        {
            CreateGameField();
            FillDictionary();
            for (int i = 0; i < GameField.Children.Count; i++)
            {
                BitmapImage PictureToShow = Gamecontrol.currentGame.puzzlepieces[ChildPositionToPoint[i].X, ChildPositionToPoint[i].Y].getPictureAsBitmapImage();
                ((Image)GameField.Children[i]).Source = PictureToShow;
            }
            MoveCounter.Text = Gamecontrol.currentGame.currentMoves.ToString();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GameField.Height = Math.Floor(RowHeight.ActualHeight);
            GameField.Width = Math.Floor(RowHeight.ActualHeight);
            VizualizePuzzle();
        }

        private void GameField_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GameField.Height = Math.Floor(RowHeight.ActualHeight);
            GameField.Width = Math.Floor(RowHeight.ActualHeight);
            VizualizePuzzle();
        }
    }
}