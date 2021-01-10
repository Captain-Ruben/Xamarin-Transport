using AppTransport.Models;
using AppTransport.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTransport.ViewModels
{
    class RitEditViewModel
    {
        private Rit rit = new Rit();

        public RitEditViewModel(object id)
        {
            Inladen(id);

        }

        private void Inladen(object id)
        {
            int Id = Convert.ToInt32(id);

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Table<Rit>();
                rit = conn.Table<Rit>().Where(x => x.RitId == Id).FirstOrDefault();    
            };
        }


        public string RitId
        {
            get { return rit.RitId.ToString(); }
            set { }
        }
        
        public string KlantNaam
        {
            get { return rit.KlantNaam; }
            set {}
        }
        public string Plaats
        {
            get { return rit.Plaats; }
            set {}
        }
        public string Kilometers
        {
            get { return rit.Kilometers.ToString(); }
            set { kilometers = value; }
        }
        public string Aankomst
        {
            get { return rit.Aankomst.ToString(); }
            set { aankomst = value; }
        }
        public string Vertrek
        {
            get { return rit.Vertrek.ToString(); }
            set { vertrek = value; }
        }
        public string Werktijd
        {
            get { return rit.Werktijd; }
            set { }
        }
        public string Rijtijd
        {
            get { return rit.Rijtijd; }
            set { }
        }
        public string Paletten
        {
            get { return rit.Paletten.ToString(); }
            set { paletten = value; }
        }

        public ICommand CommandBewerkenRit => new Command(RitBewerken);
        public ICommand CommandAnullerenRit => new Command(RitBewerken);

        private string kilometers;
        private string aankomst;
        private string vertrek;
        private string paletten;

        private async void RitBewerken()
        {
            try
            {
                Rit newRit = new Rit
                {
                    RitId = rit.RitId,
                    KlantNaam = rit.KlantNaam,
                    Plaats = rit.Plaats,
                    Kilometers = Convert.ToInt32(kilometers),
                    Aankomst = Convert.ToDateTime(aankomst),
                    Vertrek = Convert.ToDateTime(vertrek),
                    Werktijd = rit.Werktijd,
                    Rijtijd = rit.Rijtijd,
                    Paletten = Convert.ToInt32(paletten)
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    string update = $"UPDATE Rit Set Kilometers='{newRit.Kilometers}', Aankomst='{newRit.Aankomst}', Vertrek='{newRit.Aankomst}', Paletten='{newRit.Paletten}' WHERE RitId={newRit.RitId}";
                    conn.CreateTable<Rit>();
                    conn.Execute(update);

                    await Application.Current.MainPage.Navigation.PushAsync(new RitVerslag());
                }

            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", e.Message, "Ok"); ;
            }
            

            
        }
        private async void RitAnnulleren()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RitVerslag());
        }
    }
}
