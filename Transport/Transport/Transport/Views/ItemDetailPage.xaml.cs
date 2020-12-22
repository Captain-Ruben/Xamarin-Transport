using System.ComponentModel;
using Transport.ViewModels;
using Xamarin.Forms;

namespace Transport.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}