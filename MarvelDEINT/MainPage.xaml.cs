using System;
using Microsoft.Maui.Controls;

namespace MarvelDEINT
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void NavigateToClasificacion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clasificacion());
        }
    }
}
