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
using System.Collections.ObjectModel;
using System.Linq;
using WpfAgendaDatabase.Agenda_AlexDB;

namespace WpfAgendaDatabase.View
{
    public partial class ViewContact : UserControl
    {
        public ObservableCollection<Identité> Identités { get; set; } = new ObservableCollection<Identité>();

        public ViewContact()
        {
            InitializeComponent();
            LoadData();
            DataGridContacts.ItemsSource = Identités;
            this.DataContext = this;
        }

        private void LoadData()
        {
            using (var context = new AgendaAlexContext())
            {
                Identités = new ObservableCollection<Identité>(context.Identités.ToList());
                DataGridContacts.ItemsSource = Identités;
            }
        }

        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            ViewAjouter viewAjouter = new ViewAjouter();
            MainContent.Content = viewAjouter;
        }

        private void Button_Click_Supprimer(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGridContacts.SelectedItem as Identité;
            if (selectedItem != null)
            {
                using (var context = new AgendaAlexContext())
                {
                    context.Identités.Remove(selectedItem);
                    context.SaveChanges();
                }
                Identités.Remove(selectedItem); // Supprime l'élément de l'ObservableCollection
                MessageBox.Show("Contact supprimé avec succès!");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un contact à supprimer.");
            }
        }


    }
}

