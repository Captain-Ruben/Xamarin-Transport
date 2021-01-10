using AppTransport.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTransport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserLogin : ContentPage
    {
        public ICommand AccountButton => new Command(AccountBeheer);
        public ICommand LoginButton => new Command(LoginToApp);

        public UserLogin()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void AccountBeheer()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new UserRegistreren()));
        }

        private async void LoginToApp()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                if (UserNaam.Text == null || UserWachtwoord.Text == null)
                {
                    UserNaam.Text = "";
                    UserWachtwoord.Text = "";
                }

                User user = new User
                {
                    UserNaam = UserNaam.Text,
                    UserWachtwoord = UserWachtwoord.Text,
                };

                FirebaseClient firebaseClient = new FirebaseClient("https://extrade-681e6-default-rtdb.firebaseio.com/");

                var GetUser = (await firebaseClient
                                     .Child("Users")
                                     .OnceAsync<User>()).Where(x => x.Object.UserNaam == user.UserNaam).Where(x => x.Object.UserWachtwoord == user.UserWachtwoord);


                if (GetUser.Count() > 0)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new RitVerslag());
                }
                else
                {
                    await DisplayAlert("Login Fout", "De Inloggegevens kloppen niet probeer het opnieuw", "OK");
                }
                
            }
            else
            {
                await DisplayAlert("Login Fout", "Geen Internetverbinding", "OK");
            }
            
        }
    }


}