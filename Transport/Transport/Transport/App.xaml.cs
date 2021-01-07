using System;
using Transport.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Transport
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new KlantenOverzicht());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
