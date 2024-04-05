using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour ViewToDoList.xaml
    /// </summary>
    public partial class ViewToDoList : UserControl
    {

        private DAO_ToDoList _daoToDoList;
        public ObservableCollection<ToDoList> ToDoLists { get; private set; } // Collection de ToDoLists
        public ViewToDoList()
        {
            InitializeComponent();

            this.DataContext = this; // Définition du contexte de données pour la ListView
            _daoToDoList = new DAO_ToDoList(); // Initialisation de DAO_ToDoList
            LoadToDoLists();
        }

        private void LoadToDoLists()
        {
            var toDoLists = _daoToDoList.GetAllToDoLists();
            ToDoLists = new ObservableCollection<ToDoList>(toDoLists);

        }

        private void AjouterToDoList_Click(object sender, RoutedEventArgs e)
        {
            // Vider le contenu actuel
            MainContent.Content = null;

            // Charger la nouvelle vue
            MainContent.Content = new ViewAjouterToDoList();
        }

        private void SupprimerToDoList_Click(object sender, RoutedEventArgs e)
        {
            var toDoListSelected = ListViewToDoLists.SelectedItem as ToDoList;
            if (toDoListSelected != null)
            {
                // Suppression de l'élément de la base de données
                _daoToDoList.DeleteToDoList(toDoListSelected.IdToDoList);

                // Suppression de l'élément de la collection, ce qui mettra à jour l'UI
                ToDoLists.Remove(toDoListSelected);
            }
        }

        private void ModifierToDoList_Click(object sender, RoutedEventArgs e)
        {
            var toDoListSelected = ListViewToDoLists.SelectedItem as ToDoList;// Récupérer l'élément sélectionné
            if (toDoListSelected != null)
            {
                // Vider le contenu actuel
                MainContent.Content = null;

                // Charger la nouvelle vue
                MainContent.Content = new ViewAjouterTask(toDoListSelected.IdToDoList);
            }

        }
    }
}
