using System;
using System.Collections.Generic;
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

namespace PuzzleMaster
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Damit das Programm in der Mitte des Bildschirms erscheint
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            // Es wird die StartScreenPage in den MainFrame geladen
            MainFrame.Source = StartScreenPage.StartScreenUri;
        }
    }
}
