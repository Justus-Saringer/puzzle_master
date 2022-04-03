using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PuzzleMaster
{
    /// <summary>
    /// Interaktionslogik für GameSettings.xaml
    /// </summary>
    public partial class GameSettings : Page
    {
        public static Uri GameSettingsUri = new Uri("/Pages/GameSettings.xaml", UriKind.Relative);
        private static bool ImageSelected = false;
        public uint PictureID = 0;
        public uint Size = 0;
        public GameSettings()
        {
            InitializeComponent();
            LoadGamesettings();
        }


        // Die Funktion kann leider nicht ausgelagert werden da sonst hier eine Instanz dieser Klasse erstellt werden müsste um auf die 
        // Informationen dieser Klasse zugreifen zu können --> was zu einem Stackoverflow führen würde
        // Daher die Funktionalitäte des Darstellen der Bilder in den Einstellungen hier:
        // Diese Funktion des zeigen der Bilder in der GameSettings hat der Patrick gemacht, auch den Komentar geschrieben ;) -->
        // Darstellen der Bilder in den Einstellungen
        void LoadGamesettings()
        {
            List<Picture> MiniPictureList = new List<Picture>(Gamecontrol.restRequest.pictureAll());  // holen der Bilder vom Server

            int MiniPictureNr = 0;
            for (int i = 0; i < BildauswahlGrid.Children.Count; i++)
            //foreach (var item in BildauswahlGrid.Children)
            {
                if (BildauswahlGrid.Children[i].GetType() == typeof(Button))
                //if(item.GetType() == typeof(Button))
                {
                    ImageBrush brush = new ImageBrush();
                    // setzt ein BitmapImage im bursh //Das Bild kommt vom Server, wird in ein BitmapImage Umgewandelt und wird dann gesetzt  
                    brush.ImageSource = MiniPictureList[MiniPictureNr].getPictureAsBitmapImage();
                    ((Button)BildauswahlGrid.Children[i]).Background = brush;
                    ((Button)BildauswahlGrid.Children[i]).Tag = MiniPictureList[MiniPictureNr].id;
                    MiniPictureNr += 1;
                }
            }
        }

        // Zurückknopf
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(StartScreenPage.StartScreenUri);
        }

        // Knopf zum Starten des Spiels
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (puzzleSizeInput.SelectedItem != null)
            {
                if (freeFieldColumn.SelectedItem != null)
                {
                    if (freeFieldRow.SelectedItem != null)
                    {
                        if (ImageSelected == true)
                        {
                            this.NavigationService.Navigate(GamePage.GamePageUri);
                            // Parameter müssen übergeben werden --> Jup hab ich gemacht 
                            // Das hat der Peter(das is nen insider) gemacht :-)
                            Size = (uint)puzzleSizeInput.Text[0] - 48;
                            uint XOfFreeField = (uint)freeFieldColumn.Text[0] - 49;  // -49 weil: 47 ASCII Zeichen vor der 0,48 in ASCII
                            uint YOfFreeField = (uint)freeFieldRow.Text[0] - 49;     // und wenn User 1 eingibt --> wir wollen 0 haben --> -49

                            System.Drawing.Point FreeField = new System.Drawing.Point((int)XOfFreeField, (int)YOfFreeField);
                            Gamecontrol.LoadGame(Size, FreeField, PictureID);
                            GamePage.GeneralSize = Gamecontrol.currentGame.puzzlepieces.GetLength(0);
                        }
                        else
                        {
                            MessageBox.Show("Bitte ein Bild auswählen");
                        }
                    }
                    else
                    {
                        freeFieldRow.Text = "Dieses Feld darf nicht leer sein";
                    }
                }
                else
                {
                    freeFieldColumn.Text = "Dieses Feld darf nicht leer sein";
                }
            }
            else
            {
                //puzzleSizeInput.Text = "Dieses Feld darf nicht leer sein";
                MessageBox.Show("Bitte Feldgröße auswählen");
            }
        }

        // Passt die ComboBoxen für die freie Feldauswahl an
        private void puzzleSizeInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setComboBox(uint.Parse(((string)((ComboBoxItem)e.AddedItems[0]).Content)[0].ToString()));
        }

        private void setComboBox(uint size)
        {
            // leert die aktuellen ComboBoxen
            this.freeFieldRow?.Items.Clear();
            this.freeFieldColumn?.Items.Clear();
            // Es werden die ComboBoxen für das freie Feld entsprechend der Größe befüllt
            for (int times = 1; times <= size; times++)
            {
                freeFieldRow?.Items.Add((uint)times);
                freeFieldColumn?.Items.Add((uint)times);
            }
        }

        private void clickSelect()
        {
            // Es wird von allen RadioButtons die Auswahl entfernt
            foreach (var btn in BildauswahlGrid.Children)
            {
                if (btn is RadioButton)
                {
                    ((RadioButton)btn).IsChecked = false;
                }
            }
            // Es wurde ein Bild ausgewählt, eine weitere Startbedingung ist erfüllt
            ImageSelected = true;
        }



        private void fieldBtn0x0_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn0x0.Tag;
            clickSelect();
            fieldRadioBtn0x0.IsChecked = true;
        }
        private void fieldRadioBtn0x0_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn0x0.Tag;
            clickSelect();
            fieldRadioBtn0x0.IsChecked = true;
        }



        private void fieldBtn1x0_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn1x0.Tag;
            clickSelect();
            fieldRadioBtn1x0.IsChecked = true;
        }
        private void fieldRadioBtn1x0_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn1x0.Tag;
            clickSelect();
            fieldRadioBtn1x0.IsChecked = true;
        }



        private void fieldBtn2x0_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn2x0.Tag;
            clickSelect();
            fieldRadioBtn2x0.IsChecked = true;
        }
        private void fieldRadioBtn2x0_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn2x0.Tag;
            clickSelect();
            fieldRadioBtn2x0.IsChecked = true;
        }



        private void fieldBtn3x0_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn3x0.Tag;
            clickSelect();
            fieldRadioBtn3x0.IsChecked = true;
        }
        private void fieldRadioBtn3x0_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn3x0.Tag;
            clickSelect();
            fieldRadioBtn3x0.IsChecked = true;
        }



        private void fieldBtn0x1_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn0x1.Tag;
            clickSelect();
            fieldRadioBtn0x1.IsChecked = true;
        }
        private void fieldRadioBtn0x1_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn0x1.Tag;
            clickSelect();
            fieldRadioBtn0x1.IsChecked = true;
        }



        private void fieldBtn1x1_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn1x1.Tag;
            clickSelect();
            fieldRadioBtn1x1.IsChecked = true;
        }
        private void fieldRadioBtn1x1_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn1x1.Tag;
            clickSelect();
            fieldRadioBtn1x1.IsChecked = true;
        }



        private void fieldBtn2x1_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn2x1.Tag;
            clickSelect();
            fieldRadioBtn2x1.IsChecked = true;
        }
        private void fieldRadioBtn2x1_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn2x1.Tag;
            clickSelect();
            fieldRadioBtn2x1.IsChecked = true;
        }



        private void fieldBtn3x1_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn3x1.Tag;
            clickSelect();
            fieldRadioBtn3x1.IsChecked = true;
        }
        private void fieldRadioBtn3x1_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn3x1.Tag;
            clickSelect();
            fieldRadioBtn3x1.IsChecked = true;
        }



        private void fieldBtn0x2_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn0x2.Tag;
            clickSelect();
            fieldRadioBtn0x2.IsChecked = true;
        }
        private void fieldRadioBtn0x2_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn0x2.Tag;
            clickSelect();
            fieldRadioBtn0x2.IsChecked = true;
        }



        private void fieldBtn1x2_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn1x2.Tag;
            clickSelect();
            fieldRadioBtn1x2.IsChecked = true;
        }
        private void fieldRadioBtn1x2_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn1x2.Tag;
            clickSelect();
            fieldRadioBtn1x2.IsChecked = true;
        }



        private void fieldBtn2x2_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn2x2.Tag;
            clickSelect();
            fieldRadioBtn2x2.IsChecked = true;
        }
        private void fieldRadioBtn2x2_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn2x2.Tag;
            clickSelect();
            fieldRadioBtn2x2.IsChecked = true;
        }



        private void fieldBtn3x2_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn3x2.Tag;
            clickSelect();
            fieldRadioBtn3x2.IsChecked = true;
        }
        private void fieldRadioBtn3x2_Click(object sender, RoutedEventArgs e)
        {
            PictureID = (uint)fieldBtn3x2.Tag;
            clickSelect();
            fieldRadioBtn3x2.IsChecked = true;
        }
    }
}
