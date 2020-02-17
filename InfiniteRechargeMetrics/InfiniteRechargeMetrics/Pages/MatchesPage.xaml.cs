﻿using SQLite;
using System;
using InfiniteRechargeMetrics.Data;
using System.Threading.Tasks;
using InfiniteRechargeMetrics.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchesPage : ContentPage
    {
        private ObservableCollection<Match> matches = new ObservableCollection<Match>();
        public ObservableCollection<Match> Matches
        {
            get => matches;
            set
            {
                matches = value;
            }
        }

        public MatchesPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Run(() =>
            {
                // Creating a connection to the db
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseFilePath))
                {
                    // Creates a table if it doesn't already exist
                    connection.CreateTable<Match>();
                    // Returns all the Teams from the db
                    Matches = new ObservableCollection<Match>(connection.Table<Match>().ToList());
                }       
            });
            MatchesListView.ItemsSource = Matches;
        }

        private void OnNewMatch(object sender, EventArgs e)
        {
            DatabaseService.SaveToDatabase(new Match() { Title = "Match" });
        }
    }
}