using AppTransport.Models;
using AppTransport.Views;
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
    class RitVerslagViewModel : INotifyPropertyChanged
    {
        public RitVerslagViewModel()
        {
            Inladen();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        
        private string searchbarText;
        public string SearchbarText
        {
            get { return searchbarText; }
            set 
            { 
                searchbarText = value;
                listViewRitten = null;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListViewRitten))); }
        }

        private List<Rit> listViewRitten;
        public List<Rit> ListViewRitten
        {
            get
            {
                if (listViewRitten == null)
                {
                    Inladen(searchbarText);
                    return listViewRitten;
                }
                else
                {
                    //Inladen();
                    return listViewRitten;
                }

            }

            set
            {
                listViewRitten = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListViewRitten)));
            }
        }


        public ICommand CommandToevoegenRit => new Command(RitToevoegenButtonAsync);
        private async void RitToevoegenButtonAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RitToevoegen());
        }
        public ICommand CommandMailRitVerslag => new Command(MailButton);
        private void MailButton() 
        {
            
        }
        public ICommand CommandNieuwRitVerslag => new Command(DeleteRitVerslag);
        private void DeleteRitVerslag()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.DropTable<Rit>();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListViewRitten)));
            }
        }


        private void Inladen()
        {
            
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Rit>();

                conn.Table<Rit>();
                listViewRitten = conn.Table<Rit>()
                    .OrderBy(x => x.RitId)
                    .ToList();
            }
        }
        private void Inladen(string searchbarText)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Table<Rit>();
                listViewRitten = conn.Table<Rit>()
                    .Where(x => x.KlantNaam.Contains(searchbarText))
                    .OrderBy(x => x.RitId)
                    .ToList();

                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListViewRitten)));
            }
        }
    }
}
