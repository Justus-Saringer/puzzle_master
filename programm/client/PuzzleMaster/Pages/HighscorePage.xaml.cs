using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PuzzleMaster
{
    /// <summary>
    /// Interaktionslogik für HighscorePage.xaml
    /// </summary>
    public partial class HighscorePage : Page
    {
        public static Uri HighscoreUri = new Uri("/Pages/HighscorePage.xaml", UriKind.Relative);

        public HighscorePage()
        {

            //Debug.WriteLine("Test!");
            InitializeComponent();
            ObservableCollection<HighscoreEntry> Highscorelistentries;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(StartScreenPage.StartScreenUri);
        }

        
        // Diese Funktion hat der Patrick gemacht -->
        private void HighscoreCBoxPSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        // Das ist quasi die Vizualize HighscoreList Funktion aus Gamecontrol nur hier her ausgelagert
        {
            string ContentAsString = HighscoreCBoxPSize.SelectedItem.ToString();
            char character = ContentAsString[ContentAsString.Count() - 1];
            uint ContentAsUint = (uint)character - 48;   // Durch ASCII Code 0, ist das 48te ASCII Zeichen

            RestRequest Warum = new RestRequest();
            List<HighscoreEntry> Highscores = Warum.highscoreGetAllFromFieldSize(ContentAsUint);

            HighscoreListView.Items.Clear();
            if (Highscores.Count == 0)
            {
                HighscoreEntry Dieter = new HighscoreEntry(0, 0, "Dieter_Tja Entwicklervorteil halt");
                HighscoreEntry Domme = new HighscoreEntry(0, 0, "Domme_Tja Entwicklervorteil halt");
                HighscoreEntry Peter = new HighscoreEntry(0, 0, "Peter_Tja Entwicklervorteil halt");
                Highscores.Add(Dieter); Highscores.Add(Domme); Highscores.Add(Peter);
            }
            for (int i = 0; i < Highscores.Count; i++)
            {
                Highscores[i].position = i;
                HighscoreListView.Items.Add(Highscores[i]);
            }
        }
    }
}
