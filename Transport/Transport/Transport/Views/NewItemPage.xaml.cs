using System;
using System.Collections.Generic;
using System.ComponentModel;
using Transport.Models;
using Transport.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Transport.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}