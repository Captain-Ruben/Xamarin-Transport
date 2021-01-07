using AppTransport.Models;
using AppTransport.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTransport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KlantenOverzicht : ContentPage
    {
        public KlantenOverzicht()
        {
            BindingContext = new KlantenViewModel();
            InitializeComponent();
        }

    }
}