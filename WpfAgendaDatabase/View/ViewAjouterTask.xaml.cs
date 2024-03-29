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
    /// Logique d'interaction pour ViewAjouterTask.xaml
    /// </summary>
    public partial class ViewAjouterTask : UserControl
    {
        private int _toDoListId;
        private DAO_ToDoList _daoToDoList;
        private ObservableCollection<Tache> _taches;
        public ViewAjouterTask(int toDoListId)
        {
            InitializeComponent();
            _toDoListId = toDoListId;

            _daoToDoList = new DAO_ToDoList();

            ChargerTaches();

            _taches = new ObservableCollection<Tache>(/* Chargez les tâches ici depuis le DAO */);
            this.DataContext = this;
        }

        private void AjouterTache_Click(object sender, RoutedEventArgs e)
        {
            var nomTache = NomTacheTextBox.Text;
            var tips = TipsTextBox.Text;

            // Créer et ajouter la tâche seulement si le nom n'est pas vide
            if (!string.IsNullOrWhiteSpace(nomTache))
            {
                var task = new Tache
                {
                    Nom = nomTache,
                    Tips = tips,
                    ToDoListIdToDoList = _toDoListId,
                    Check = 0 // supposons 0 pour non-coché
                };

                var daoToDoList = new DAO_ToDoList();
                daoToDoList.AddTask(task);

                // Afficher une boîte de dialogue de confirmation
                MessageBox.Show("La tâche a bien été ajoutée.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

                // Effacer les TextBoxes
                NomTacheTextBox.Clear();
                TipsTextBox.Clear();

                // Optionnel : Mettre à jour la liste de tâches dans l'interface utilisateur
                _taches.Add(task);
            }
            else
            {
                // Afficher une erreur si le nom de la tâche est vide
                MessageBox.Show("Veuillez entrer le nom de la tâche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChargerTaches()
        {
            // Supposons que DAO_ToDoList a une méthode pour obtenir toutes les tâches pour une ToDoList spécifique
            var taches = _daoToDoList.GetTachesFromToDoList(_toDoListId);
            _taches = new ObservableCollection<Tache>(taches);
            ComboBoxTaches.ItemsSource = _taches;
        }

        private void SupprimerTache_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = (Tache)ComboBoxTaches.SelectedItem;
            if (selectedTask != null)
            {
                _daoToDoList.DeleteTask(selectedTask.IdTasks);
                _taches.Remove(selectedTask);
                MessageBox.Show("La tâche a bien été supprimée.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une tâche à supprimer.");
            }
        }
    }
}
