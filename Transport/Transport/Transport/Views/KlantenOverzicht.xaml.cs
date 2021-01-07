using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Models;
using Transport.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Transport.Views
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