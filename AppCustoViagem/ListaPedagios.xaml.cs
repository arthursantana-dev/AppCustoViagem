using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCustoViagem;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCustoViagem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPedagios : ContentPage
    {

        private App PropriedadesApp;
        public ListaPedagios()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;

            lstListaPedagios.ItemsSource = PropriedadesApp.ListaPedagios;

            if (PropriedadesApp.ListaPedagios.Count ==  0)
            {
                lstListaPedagios.IsVisible = false;
                lblMsgListaVazia.IsVisible = true;
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem menuItem = sender as MenuItem;

                var pedagioSelecionado = (Model.Pedagio)menuItem.BindingContext;

                if (await DisplayAlert("Tem certeza?", "Deseja remover este Pedágio?", "S", "N"))
                {
                    PropriedadesApp.ListaPedagios.RemoveAll(i => i.NroPedagio == pedagioSelecionado.NroPedagio);

                    lstListaPedagios.ItemsSource = new List<Model.Pedagio>();

                    lstListaPedagios.ItemsSource = PropriedadesApp.ListaPedagios;
                }
            } catch (Exception ex) {
                await DisplayAlert("Ops", "Ocorreu um erro " + ex.Message, "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var total = PropriedadesApp.ListaPedagios.Sum(i => i.Valor).ToString("C");

                await DisplayAlert("Resultado Final", String.Format("O total dos pedágios é {0}", total), "OK");
            } catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }
        }
    }
}