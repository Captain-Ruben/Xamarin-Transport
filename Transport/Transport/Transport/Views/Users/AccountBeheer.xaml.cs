using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Transport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountBeheer : ContentPage
    {
        public ICommand RegistrerenButton => new Command(RegistrerenPage);
        public ICommand LoginButton => new Command(LoginPage);
        
        public AccountBeheer()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void RegistrerenPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Registreren());
        }

        private async void LoginPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new Login()));
        }

    }
}