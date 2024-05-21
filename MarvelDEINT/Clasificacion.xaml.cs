using DAL;
using Entidades;
using MarvelDEINT.ViewModels;
using Microsoft.Maui.Controls;

namespace MarvelDEINT
{
    public partial class Clasificacion : ContentPage
    {
        public Clasificacion()
        {
            InitializeComponent();
        }

        private async void PuntuarPagina(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //protected override void OnAppearing()
        //{
        //    //base.OnAppearing();
        //    var miBinding = ClasificacionViewModel.this.BindingContext;
        //    miBinding.RefrescarCommand.Execute(null);
        //}

    }
}