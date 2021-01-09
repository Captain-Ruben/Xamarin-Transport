﻿using AppTransport.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTransport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RitVerslag : ContentPage
    {
        public RitVerslag()
        {
            InitializeComponent();
            BindingContext = new RitVerslagViewModel();
        }
    }
}