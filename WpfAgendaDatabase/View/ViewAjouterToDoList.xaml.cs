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
            _daoToDoList = new DAO_ToDoList(); // Initialisation de DAO_ToDoList
        }

        private void AjouterTDL_Click(object sender, RoutedEventArgs e)
        {
            var titre = textBoxTitre.Text; 
            var selectedDate = datePickerDate.SelectedDate; 

            if (!string.IsNullOrEmpty(titre) && selectedDate.HasValue)
            {
                // Formattez la date sélectionnée en chaîne de caractères
                string formattedDate = selectedDate.Value.ToString("yyyy-MM-dd");

                var toDoList = new ToDoList
                {
                    Titre = titre,
                    Date = formattedDate
                };

                _daoToDoList.AddToDoList(toDoList); // Utilisez la méthode d'ajout ici

                // Affichez un message de confirmation et gestion d'erreur
                MessageBox.Show("ToDoList ajoutée avec succès.");

                textBoxTitre.Clear();
                datePickerDate.SelectedDate = null; // Réinitialisez le DatePicker si nécessaire
            }
            else
            {
                MessageBox.Show("Veuillez saisir un titre et sélectionner une date valide.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
