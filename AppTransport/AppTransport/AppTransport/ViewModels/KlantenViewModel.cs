using AppTransport.Models;
using Firebase.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTransport.ViewModels
{
    class KlantenViewModel : INotifyPropertyChanged
    {
        #region Commands
        //Commands
        public ICommand KlantToevoegenCommand => new Command(ToevoegenKlant);
        public ICommand KlantVerwijderenCommand => new Command(VerwijderenKlant);
        public ICommand KlantBewerkenCommand => new Command(EditKlant);

        #endregion

        #region Constructor
        //Constructor
        public KlantenViewModel()
        {

        }
        #endregion

        #region Prop

        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Klant> klanten = new ObservableCollection<Klant>();
        public ObservableCollection<Klant> Klanten
        {
            get
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    List<Klant> klantenList = new List<Klant>();
                    conn.Table<Klant>();
                    klantenList = conn.Table<Klant>().ToList();
                    foreach (var item in klantenList)
                    {
                        klanten.Add(item);
                    }  
                }
                return klanten;
            }
        }

        public string KlantNaam { get; set; }
        public string KlantPlaats { get; set; }
        public Klant KlantGeselecteerd { get; set; }

        #endregion

        #region Bewerkingen

        //Toevoegen
        private void ToevoegenKlant()
        {
            if (KlantNaam != null && KlantNaam != "" && KlantPlaats != null && KlantPlaats != "")
            {
                //LIST
                klanten.Add(new Klant() { Naam = KlantNaam, Plaats = KlantPlaats });

                //DATABASE
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Klant>();
                    conn.Insert(new Klant() { Naam = KlantNaam, Plaats = KlantPlaats });
                }

                KlantNaam = "";
                KlantPlaats = "";
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(KlantNaam)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(KlantPlaats)));


            }

        }

        //Verwijderen
        private void VerwijderenKlant()
        {
            if (KlantGeselecteerd != null)
            {
                //LIST
                klanten.Remove(KlantGeselecteerd);

                //DATABASE
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Klant>();
                    conn.Table<Klant>().Where(x => x.Id == KlantGeselecteerd.Id).Delete();
                }
            } 
        }

        //Bewerken
        private void EditKlant()
        {
            //LIST
            int plaatsGeselecteerd = klanten.IndexOf(KlantGeselecteerd);
            if (plaatsGeselecteerd == -1)
            {
                KlantNaam = "";
                KlantPlaats = "";
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(KlantNaam)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(KlantPlaats)));

            }
            else
            {
                klanten.Remove(KlantGeselecteerd);

                klanten.Add(new Klant() { Naam = KlantNaam, Plaats = KlantPlaats });
                int plaatsNieuw = klanten.Count;

                klanten.Move(plaatsNieuw - 1, plaatsGeselecteerd);

                //DATABASE
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Klant>();
                    conn.Table<Klant>().Where(x => x.Id == KlantGeselecteerd.Id).Delete();

                    conn.CreateTable<Klant>();
                    conn.Insert(new Klant() { Id = KlantGeselecteerd.Id, Naam = KlantNaam, Plaats = KlantPlaats });
                }

                KlantNaam = "";
                KlantPlaats = "";
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(KlantNaam)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(KlantPlaats)));

            }

        }

        #endregion


    }
}
