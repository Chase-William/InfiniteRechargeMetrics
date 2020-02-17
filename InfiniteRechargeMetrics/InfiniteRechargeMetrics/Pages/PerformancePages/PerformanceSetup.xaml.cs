﻿using InfiniteRechargeMetrics.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteRechargeMetrics.ViewModels;
using InfiniteRechargeMetrics.Models;

namespace InfiniteRechargeMetrics.Pages.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerformanceSetup : ContentPage
    {
        public PerformanceSetup()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new PerformanceSetupViewModel(this);

            var teams = await DatabaseService.GetAllTeamsAsync();

            string[] teamNames = new string[teams.Count];
            for (int i = 0; i < teams.Count; i++)
            {
                teamNames[i] = teams[i].Name;
            }
            TeamPicker.ItemsSource = teamNames;
        }
    }
}