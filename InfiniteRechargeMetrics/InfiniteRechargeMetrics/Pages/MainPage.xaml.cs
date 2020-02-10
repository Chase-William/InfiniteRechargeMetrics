using InfiniteRechargeMetrics.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            masterPage.MasterPageNavListView.ItemTapped += OnMasterPageListView_ItemTapped;
            masterPage.MasterPageActions.ItemTapped += OnMasterPageListView_ItemTapped;
        }

        /// <summary>
        ///     Handler for both the main listview for navigating and the listview for actions
        /// </summary>
        void OnMasterPageListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MasterPageImageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.MasterPageNavListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
