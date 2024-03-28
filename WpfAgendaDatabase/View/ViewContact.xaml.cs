﻿using System;
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
using WpfAgendaDatabase.Agenda_AlexDB;
using WpfAgendaDatabase.Service.DAO;

namespace WpfAgendaDatabase.View
{
    public partial class ViewContact : UserControl
    {
        public ObservableCollection<Identite> Identites { get; set; } = new ObservableCollection<Identite>();
        public DAO_Contact dAO_Contact;


        public ViewContact()
        {
            InitializeComponent();

            dAO_Contact = new DAO_Contact();

            LoadData();
            this.DataContext = this;
        }

        private void LoadData()
        {

            DataGridContacts.ItemsSource = dAO_Contact.LoadAllContacts_Status();  // Chargez les contacts depuis la base de données ainsi que le status associés
        }


        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewAjouter();
        }

        private void Button_Click_Supprimer(object sender, RoutedEventArgs e)
        {
            var item = DataGridContacts.SelectedItem as Identite;
            if (item != null)
            {
                dAO_Contact.DeleteContact(item);
                Identites.Remove(item);
                // Pas besoin de réaffecter ItemsSource, la ObservableCollection s'en charge
            }
        }

        private void Button_Click_Rechercher(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchBox.Text;
            var filteredContacts = dAO_Contact.RechercherContacts(searchTerm);
            Identites.Clear();
            foreach (var contact in filteredContacts)
            {
                Identites.Add(contact);
            }
            DataGridContacts.ItemsSource = Identites; // Cette ligne peut être omise si Identites est déjà lié à ItemsSource dans XAML
        }

        private void FilterByRelation_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
           DAO_Contact dAO_Contact = new DAO_Contact();
            var filteredContacts = dAO_Contact.GetContactsByRelation(button.Content.ToString());
            Identites.Clear();
            foreach (var contact in filteredContacts)
            {
                Identites.Add(contact);
            }
            DataGridContacts.ItemsSource = Identites; // Cette ligne peut être omise si Identites est déjà lié à ItemsSource dans XAML
        }

        private void DataGridContacts_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn; // La colonne éditée
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path; // Le nom de la propriété liée à la colonne
                    var textBox = e.EditingElement as TextBox; // La cellule éditée
                    if (textBox != null)
                    {
                        var editedValue = textBox.Text; // La valeur éditée
                        var editedItem = e.Row.Item as Identite; // L'élément édité

                        using (var context = new AgendaAlexContext())
                        {
                            var item = context.Identites.FirstOrDefault(i => i.Idtable1 == editedItem.Idtable1); // L'élément à modifier
                            if (item != null)
                            {
                                // Reflection peut être utilisé ici pour rendre ce code plus générique, cela nécessite l'utilisation du nom de la propriété (bindingPath)
                                if (bindingPath == "Nom") item.Nom = editedValue;
                                else if (bindingPath == "Prenom") item.Prenom = editedValue;
                                else if (bindingPath == "Numero") item.Numero = editedValue;
                                else if (bindingPath == "Email") item.Email = editedValue;
                                else if (bindingPath == "Sexe") item.Sexe = editedValue;
                                else if (bindingPath == "VilleDeNaissance") item.VilleDeNaissance = editedValue;
                                else if (bindingPath == "DateDeNaissance") item.DateDeNaissance = DateOnly.Parse(editedValue);
                                else if (bindingPath == "Relation") item.Relation = editedValue;

                                // Vous devez implémenter la logique pour les autres propriétés
                                // var Lst = new List<Status>();

                                // else if (bindingPath == "Statuses") item.Statuses = new List<Status>(); // Vous devez implémenter la logique pour ajouter des statuts

                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
        // Le bouton Modifier n'est pas nécessaire si les modifications sont faites directement dans le DataGrid
    }
}
