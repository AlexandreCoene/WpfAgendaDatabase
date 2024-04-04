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
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfAgendaDatabase.View
{
    /// <summary>
    /// Logique d'interaction pour ViewAccueil.xaml
    /// </summary>
    public partial class ViewAccueil : UserControl
    {
        public ViewAccueil()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
            SetupDateTimeDisplay();

        }

        private void SetupDateTimeDisplay()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Mise à jour chaque seconde
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Formattez la date et l'heure comme vous le souhaitez ici
            DateTimeDisplay.Text = DateTime.Now.ToString("G");
        }
    }

    

    public class ViewModel
    {
        public SeriesCollection MySeries { get; set; }

        public ViewModel()
        {
            MySeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Tâches Complétées",
                    Values = new ChartValues<int> { 5, 3, 6, 7, 3, 5 }
                }
                // Ajoutez d'autres séries ici selon le besoin
            };
        }
    }

}
