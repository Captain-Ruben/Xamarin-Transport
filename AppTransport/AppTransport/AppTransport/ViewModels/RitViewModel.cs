using AppTransport.Models;
using AppTransport.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTransport.ViewModels
{
    
    class RitViewModel : INotifyPropertyChanged
    {
        #region Commands
        public ICommand ButtonOpslaan => new Command(RitOpslaan);
        private async void RitOpslaan()
        {
                try
                {
                Rit rit = new Rit()
                {
                    KlantNaam = SelectedKlantItem.Naam,
                    Plaats = SelectedKlantItem.Plaats,
                    Kilometers = Convert.ToInt32(EntryKilometers),
                    Aankomst = DateTime.Parse(EntryAankomst),
                    Vertrek = DateTime.Parse(EntryVertrek),
                    Werktijd = (Convert.ToDateTime(EntryVertrek).ToUniversalTime() - Convert.ToDateTime(EntryAankomst).ToUniversalTime()).ToString(),
                    Paletten = Convert.ToInt32(EntryPaletten),
                    };
                    
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.Table<Rit>();
                    List<Rit> ritList = new List<Rit>();
                    ritList = conn.Table<Rit>().ToList();


                    rit.Rijtijd = RijTijdInlade(rit, ritList);
                    Console.WriteLine(rit);
                    conn.CreateTable<Rit>();
                    conn.Insert(rit);

                    Console.WriteLine(rit);

                    await Application.Current.MainPage.Navigation.PushAsync(new RitVerslag());
                }
            }

                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert("ERROR", e.Message, "Ok"); ;
                }
        }
        private string RijTijdInlade(Rit rit, List<Rit> ritList)
        {
     
            if (ritList.Count() == 0)
            {
                return "00:00:00";
            }
            else
            {
                ritList.OrderBy(x => x.RitId);
                Rit laatsteRit = ritList.Last();
                return (rit.Aankomst - laatsteRit.Vertrek).ToString();
            }
        }
        public ICommand ButtonTimeNowAankomst => new Command(TimeNowAankomst);
        private void TimeNowAankomst()
        {
            EntryAankomst = (DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00"));
        }
        public ICommand ButtonTimeNowVertrek => new Command(TimeNowVertrek);
        private void TimeNowVertrek()
        {
            EntryVertrek = (DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00"));
        }
        public ICommand ButtonKlanten => new Command(KlantenBeheren);
        private async void KlantenBeheren()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new KlantenOverzicht());
        }
        public ICommand ButtonKlantenUpdate => new Command(KlantenUpdaten);
        private void KlantenUpdaten()
        {
            Inladen();
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(KlantenList)));
            }
        }
        #endregion

        #region Props
        public event PropertyChangedEventHandler PropertyChanged;
        private List<Klant> klantenList { get; set; } = new List<Klant>();
        public  List<Klant> KlantenList
        {
            get
            {
                Inladen();
                return klantenList.OrderBy(x => x.Naam).ToList();
            }
            set
            {
                klantenList = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(KlantenList)));
            }
        }
        private void Inladen()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Table<Klant>();
                klantenList = conn.Table<Klant>().ToList();
            }
        }
        public string EntryKlant { get; set; }
        public string EntryKilometers { get; set; }
        public string EntryPaletten { get; set; } = "0"; 

        private string enteryAankomst;
        public string EntryAankomst
        {
            get
            {
                return enteryAankomst;
            }
            set
            {
                enteryAankomst = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(EntryAankomst)));
            }
        }

        private string enteryVertrek; 
        public string EntryVertrek
        {
            get
            {
                return enteryVertrek;
            }
            set
            {
                enteryVertrek = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(EntryVertrek)));
            }
        }
        public Klant SelectedKlantItem { get; set; }
        #endregion

    }


}
