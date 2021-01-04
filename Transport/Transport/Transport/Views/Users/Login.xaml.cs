﻿using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Transport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Transport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {


        public ICommand AccountButton => new Command(AccountBeheer);
        public ICommand LoginButton => new Command(LoginToApp);

        public Login()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void AccountBeheer()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new AccountBeheer()));
        }

        private async void LoginToApp()
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
                await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new AccountBeheer()));
            }
            else
            {
                await DisplayAlert("Login Fout", "De Inloggegevens kloppen niet probeer het opnieuw", "OK");
            }
        }
    }

  
}