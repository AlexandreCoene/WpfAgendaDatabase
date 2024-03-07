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

namespace WpfAgendaDatabase.View
{
    public partial class ViewAjouter : UserControl
    {
        public ViewAjouter()
        {
            InitializeComponent();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            var nom = txtNom.Text;
            var prenom = txtPrenom.Text;
            var numero = txtNumero.Text;
            var email = txtEmail.Text;

            using (var context = new AgendaAlexContext())
            {
                var nouvelIdentite = new Identité { Nom = nom, Prenom = prenom, Numero = numero, Email = email };
                context.Identités.Add(nouvelIdentite);
                context.SaveChanges();
            }

            MessageBox.Show("Contact ajouté avec succès!");

            // Optionnel : Effacer les champs après l'insertion
            txtNom.Clear();
            txtPrenom.Clear();
            txtNumero.Clear();
            txtEmail.Clear();
        }
    }
}