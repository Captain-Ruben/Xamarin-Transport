using AppTransport.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTransport
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new UserLogin());
        }

        public static string FilePath;
        public App(string filePath)
        {
            FilePath = filePath;
            InitializeComponent();
            MainPage = new NavigationPage(new UserLogin());
     
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
