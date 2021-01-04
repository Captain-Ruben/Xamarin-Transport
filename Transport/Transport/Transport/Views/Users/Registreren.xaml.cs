using Firebase.Database;
using Firebase.Database.Query;
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
    public partial class Registreren : ContentPage
    {
        public ICommand RegistrerenButton => new Command(RegistrerenDatabase);
        public Registreren()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void RegistrerenDatabase()
        {
            if (await VallidationRegistrerenAsync())
            {
                UserName_R.Text = "";
                Wachtwoord_R.Text = "";
                Email_R.Text = "";
                Wachtwoord_R2.Text = "";
            }
            else
            {
                FirebaseClient firebaseClient = new FirebaseClient("https://extrade-681e6-default-rtdb.firebaseio.com/");
                await firebaseClient
                    .Child("Users")
                    .PostAsync(new User() { UserNaam = UserName_R.Text, UserWachtwoord = Wachtwoord_R.Text, Email = Email_R.Text });

                UserName_R.Text = "";
                Wachtwoord_R.Text = "";
                Email_R.Text = "";
                Wachtwoord_R2.Text = "";


                await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new Login()));
            }


        }

        public async Task<bool> VallidationRegistrerenAsync()
        {
            bool error = false;
            string melding = "";

            //Username
            if (UserName_R.Text == null) { melding += "Username: ERROR \r\n"; error = true; }
            else { melding += "Username: CORRECT \r\n"; }

            //Email
            if (Email_R.Text == null) { melding += "Email: ERROR \r\n"; error = true; }
            else if (!Email_R.Text.Contains("@")) { melding += "Email: ERROR \r\n Email bevat geen @ \r\n"; }
            else { melding += "Email: CORRECT \r\n"; }

            //Wachtwoord
            if (Wachtwoord_R.Text == null) { melding += "Wachtwoord: ERROR \r\n"; error = true; }
            else if (Wachtwoord_R.Text != Wachtwoord_R2.Text) { melding += "Wachtwoord: ERROR \r\n Wachtwoorden komen niet overeen \r\n"; error = true; }
            else if (Wachtwoord_R.Text.Length < 5) { melding += "Wachtwoord: ERROR \r\n Wachtwoord moet minstens 5 tekens hebben \r\n"; error = true; }
            else if (!Wachtwoord_R.Text.Any(c => char.IsDigit(c))) {melding += "Wachtwoord: ERROR \r\n Minstens 1 cijfer hebben \r\n"; error = true; }
            else { melding += "Wachtwoord: CORRECT \r\n";}

            if (error == true)
            {
                await DisplayAlert("Registratie Overzicht", melding, "OK");
            }

            return error;
        }
    }

}