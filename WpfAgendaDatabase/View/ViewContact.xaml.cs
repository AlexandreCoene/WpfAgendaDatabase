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
        public ObservableCollection<Identité> Identités { get; set; } = new ObservableCollection<Identité>();
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
            DataGridContacts.ItemsSource = dAO_Contact.LoadAllContacts();
        }


        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewAjouter();
        }

        private void Button_Click_Supprimer(object sender, RoutedEventArgs e)
        {
            var item = DataGridContacts.SelectedItem as Identité;
            if (item != null)
            {
                dAO_Contact.DeleteContact(item);
                Identités.Remove(item);
                // Pas besoin de réaffecter ItemsSource, la ObservableCollection s'en charge
            }
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
                        var editedItem = e.Row.Item as Identité; // L'élément édité

                        using (var context = new AgendaAlexContext()) 
                        {
                            var item = context.Identités.FirstOrDefault(i => i.Idtable1 == editedItem.Idtable1); // L'élément à modifier
                            if (item != null)
                            {
                                // Reflection peut être utilisé ici pour rendre ce code plus générique, cela nécessite l'utilisation du nom de la propriété (bindingPath)
                                if (bindingPath == "Nom") item.Nom = editedValue;
                                else if (bindingPath == "Prenom") item.Prenom = editedValue;
                                else if (bindingPath == "Numero") item.Numero = editedValue;
                                else if (bindingPath == "Email") item.Email = editedValue;
                                else if (bindingPath == "Sexe") item.Sexe = editedValue;                              
                                else if (bindingPath == "VilleDeNaissance") item.VilleDeNaissance = editedValue;
                                else if (bindingPath == "Adresse") item.Adresse = editedValue;


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
