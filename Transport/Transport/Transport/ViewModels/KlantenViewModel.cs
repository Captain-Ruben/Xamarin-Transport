using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Transport.Models;
using Xamarin.Forms;

namespace Transport.ViewModels
{
    class KlantenViewModel 
    {
        #region Commands
        //Commands
        public ICommand KlantToevoegenCommand => new Command(AddKlant);
        public ICommand KlantVerwijderenCommand => new Command(DelKlant);
        public ICommand KlantBewerkenCommand => new Command(EditKlant);

        #endregion

        #region Constructor
        //Constructor
        public KlantenViewModel()
        {

        }
        #endregion


        //Database
        FirebaseClient firebaseClient = new FirebaseClient("https://extrade-681e6-default-rtdb.firebaseio.com/");
        private async void Inladen()
        {
            //VERVANGEN SQL LITE
            var firebaseList = await firebaseClient.Child("Klanten").OnceAsync<Klant>();

            foreach (var item in firebaseList)
            {
                klanten.Add(item.Object);
            }
        }

        //Klanten ListView
        private ObservableCollection<Klant> klanten = new ObservableCollection<Klant>();
        public ObservableCollection<Klant> Klanten
        {
            get
            {
                Inladen();
                return klanten;
            }
        }

        //Bewerkingen Klanten
        public string KlantNaam { get; set; }
        public string KlantPlaats { get; set; }
        public Klant KlantGeselecteerd { get; set; }

        //Toevoegen
        private async void AddKlant()
        {
            if (KlantNaam != null && KlantPlaats != null)
            {
                //VERVANGEN SQL LITE
                //await firebaseClient
                //    .Child("Klanten")
                //    .PostAsync(new Klant() { Naam = KlantNaam, Plaats = KlantPlaats });

                klanten.Add(new Klant() { Naam = KlantNaam, Plaats = KlantPlaats });

            }
        }

        //Verwijderen klanten.Add(KlantGeselecteerd);
        private void DelKlant()
        {

            klanten.Remove(KlantGeselecteerd);
            
        }

        private void EditKlant()
        {
            int plaatsGeselecteerd = klanten.IndexOf(KlantGeselecteerd);
            klanten.Remove(KlantGeselecteerd);

            if (KlantNaam == null)
            {
                KlantNaam = KlantGeselecteerd.Naam;
            }
            if (KlantPlaats == null)
            {
                KlantPlaats = KlantGeselecteerd.Plaats;
            }
            
            klanten.Add(new Klant() { Naam = KlantNaam, Plaats = KlantPlaats });
            int plaatsNieuw = klanten.Count;

            klanten.Move(plaatsNieuw - 1, plaatsGeselecteerd);

        }
    }
}
