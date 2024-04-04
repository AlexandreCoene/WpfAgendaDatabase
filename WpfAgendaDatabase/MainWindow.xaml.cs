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
using WpfAgendaDatabase.Service.DAO;

namespace WpfAgendaDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DAO_Contact dAO_Contact;
        public MainWindow()
        {
            InitializeComponent();
            dAO_Contact = new DAO_Contact(); // Initialisation de DAO_Contact
            if (dAO_Contact.checkdatabaseconnect()) 
            {
                MessageBox.Show("Database connected");
            }
            else
            {
                MessageBox.Show("Database not connected");
            }
            MainContent.Content = new ViewAccueil(); // Cette ligne charge ViewAccueil par défaut
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
            MainContent.Content = new ViewToDoList();
        }

        // Ajoutez d'autres méthodes de gestion des clics si nécessaire.
    }
}