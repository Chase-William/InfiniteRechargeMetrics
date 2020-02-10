using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using InfiniteRechargeMetrics.Data;
using System.Runtime.CompilerServices;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     Class that models the Xamarin.Forms.SearchBar and implements controls for querying data
    /// </summary>
    class SearchBarViewModel : Behavior<SearchBar>, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        ///// <summary>
        /////     Overriding default OnAttchedTo so we can bind our own handlers when this view is attached to a layout.
        ///// </summary>
        //protected override void OnAttachedTo(Xamarin.Forms.SearchBar bindable)
        //{
        //    base.OnAttachedTo(bindable);
        //    bindable.TextChanged += BindableTextChanged;
        //}

        ///// <summary>
        /////     When this view is removed from a layout we want to remove and handlers we attached.
        ///// </summary>
        //protected override void OnDetachingFrom(SearchBar bindable)
        //{
        //    base.OnDetachingFrom(bindable);
        //    bindable.TextChanged -= BindableTextChanged;
        //}

        //private async void BindableTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue.Length >= 1)
        //    {
        //        SearchResults = await DatabaseService.QueryTeamsByName(e.NewTextValue);                
        //    }
        //}


        //private List<Team> searchResults;
        //public List<Team> SearchResults
        //{
        //    get
        //    {
                
        //        return searchResults;
        //    }
        //    set
        //    {
        //        searchResults = value;
        //        NotifyPropertyChanged(nameof(SearchResults));
        //    }
        //}
    }
}
