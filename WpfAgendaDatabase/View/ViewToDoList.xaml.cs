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
        public ObservableCollection<ToDoList> ToDoLists { get; private set; }
        public ViewToDoList()
        {
            InitializeComponent();

            this.DataContext = this;

            _daoToDoList = new DAO_ToDoList();
            LoadToDoLists();
        }

        private void LoadToDoLists()
        {
            var toDoLists = _daoToDoList.GetAllToDoLists();
            ToDoLists = new ObservableCollection<ToDoList>(toDoLists);

        }
    }
}
