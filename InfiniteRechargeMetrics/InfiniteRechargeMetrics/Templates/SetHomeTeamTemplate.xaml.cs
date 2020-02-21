using InfiniteRechargeMetrics.ViewModels.HomeVM;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetHomeTeamTemplate : ContentView
    {
        public SetHomeTeamTemplate()
        {
            InitializeComponent();
            BindingContext = new SetHomeTeamViewModel();            
        }

        /// <summary>
        ///     Once the binding context is being changed this is called.
        ///     Basically using this since we can't have async modifier on a contructor.
        /// </summary>
        protected async override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            TeamPicker.ItemsSource = await Data.DatabaseService.Provider.GetAllTeamsIdPlusNameAsync();
            var test = TeamPicker.SelectedItem;
        }

        /// <summary>
        ///     Assigns the selected item to a property in our binding context class.
        /// </summary>
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((SetHomeTeamViewModel)BindingContext).SelectedTeamId = (string)TeamPicker.SelectedItem;           
        }
    }
}