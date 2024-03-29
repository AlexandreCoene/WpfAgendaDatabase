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
using WpfAgendaDatabase.Agenda_AlexDB;
using WpfAgendaDatabase.Service.DAO;


namespace WpfAgendaDatabase.View
{
    /// <summary>
    /// Logique d'interaction pour ViewAjouterToDoList.xaml
    /// </summary>
    public partial class ViewAjouterToDoList : UserControl
    {
        private DAO_ToDoList _daoToDoList;

        public ViewAjouterToDoList()
        {
            InitializeComponent();
            _daoToDoList = new DAO_ToDoList();
        }

        private void AjouterTDL_Click(object sender, RoutedEventArgs e)
        {
            var titre = textBoxTitre.Text;
            var dateString = textBoxDate.Text;
            DateTime date;

            bool isValidDate = DateTime.TryParse(dateString, out date);

            if (!string.IsNullOrEmpty(titre) && isValidDate)
            {
                var toDoList = new ToDoList { Titre = titre, Date = date.ToString("yyyy-MM-dd") };
                _daoToDoList.AddToDoList(toDoList); // Utilisez la méthode d'ajout ici

                // Affichez un message de confirmation ou fermez cette vue après l'ajout
                MessageBox.Show("ToDoList ajoutée avec succès.");

                textBoxTitre.Clear();
                textBoxDate.Clear();
            }
            else
            {
                MessageBox.Show("Veuillez saisir un titre et une date valide.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
