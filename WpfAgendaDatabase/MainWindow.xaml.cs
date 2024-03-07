using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAgendaDatabase.View;

namespace WpfAgendaDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAccueil_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewAccueil(); // Changez simplement le contenu de MainContent
        }


        private void BtnCalendrier_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewCalendrier();
        }

        private void BtnContact_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewContact();
        }

        private void BtnGestion_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewGestion();
        }


        // Ajoutez d'autres méthodes de gestion des clics si nécessaire.
    }
}