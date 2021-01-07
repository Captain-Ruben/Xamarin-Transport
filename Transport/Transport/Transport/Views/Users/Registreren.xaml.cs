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
                Console.WriteLine("Error: Input not Valid");
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

            bool usernameBool = true;
            bool emailBool = true;
            bool wachtwoordBool = true; 

            //Username
            if (UserName_R.Text == null) { melding += "Username: ERROR \r\n"; usernameBool = false; }
            else { melding += "Username: CORRECT \r\n"; }

            //Email
            if (Email_R.Text == null) { melding += "Email: ERROR \r\n"; emailBool = false; }
            else if (!Email_R.Text.Contains("@")) { melding += "Email: ERROR \r\n Email bevat geen @ \r\n"; emailBool = false; }
            else { melding += "Email: CORRECT \r\n"; }

            //Wachtwoord
            if (Wachtwoord_R.Text == null) { melding += "Wachtwoord: ERROR \r\n"; wachtwoordBool = false; }
            else if (Wachtwoord_R.Text != Wachtwoord_R2.Text) { melding += "Wachtwoord: ERROR \r\n Wachtwoorden komen niet overeen \r\n"; wachtwoordBool = false; }
            else if (Wachtwoord_R.Text.Length < 5) { melding += "Wachtwoord: ERROR \r\n Wachtwoord moet minstens 5 tekens hebben \r\n"; wachtwoordBool = false; }
            else if (!Wachtwoord_R.Text.Any(c => char.IsDigit(c))) {melding += "Wachtwoord: ERROR \r\n Minstens 1 cijfer hebben \r\n"; wachtwoordBool = false; }
            else { melding += "Wachtwoord: CORRECT \r\n";}

            if (usernameBool == false || emailBool == false || wachtwoordBool == false)
            {
                error = true; 
                if (usernameBool == false)
                {
                    UserName_R.Text = "";
                }
                if (emailBool == false)
                {
                    Email_R.Text = "";
                }
                if (wachtwoordBool == false)
                {
                    Wachtwoord_R.Text = "";
                    Wachtwoord_R2.Text = "";
                }

                await DisplayAlert("Registratie Overzicht", melding, "OK");
            }

            return error;
        }
    }

}