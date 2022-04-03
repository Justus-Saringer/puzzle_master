using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Office2013.Word;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page = System.Windows.Controls.Page;

namespace PuzzleMaster
{
    /// <summary>
    /// Interaktionslogik für HighscorePage.xaml
    /// </summary>
    public partial class StartScreenPage : Page
    {
        public static Uri StartScreenUri = new Uri("/Pages/StartScreen.xaml", UriKind.Relative);
        public StartScreenPage()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(playerName.Text))
            {
                Gamecontrol.PlayersName = playerName.Text;
            }
            this.NavigationService.Navigate(GameSettings.GameSettingsUri);
        }

        private void HighscoreListBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(HighscorePage.HighscoreUri);
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
