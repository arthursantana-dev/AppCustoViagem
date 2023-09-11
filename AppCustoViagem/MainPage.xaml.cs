using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AppCustoViagem
{
    public partial class MainPage : ContentPage
    {
        public App PropriedadesApp;
        public MainPage()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;
        }



        private async void btnPedagio_Clicked(object sender, EventArgs e)
        {
            try
            {
                int qntPedagios = PropriedadesApp.ListaPedagios.Count;

                PropriedadesApp.ListaPedagios.Add(new Model.Pedagio
                {
                    NroPedagio = qntPedagios + 1,
                    Valor = Convert.ToDecimal(entryValorP.Text)
                });

                await DisplayAlert("Adicionado!", "Veja na Lista de Pedágios", "OK");

                entryValorP.Text = "";

            } catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }

            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ListaPedagios());
            } catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                decimal valorTotalPedagios = PropriedadesApp.ListaPedagios.Sum(item => item.Valor);

                if (string.IsNullOrEmpty(entryDistância.Text)) throw new Exception("Por favor, preencha a distância");

                if (string.IsNullOrEmpty(entryConsumo.Text)) throw new Exception("Por favor, preencha o consumo do veículo");

                if (string.IsNullOrEmpty(entryValorC.Text)) throw new Exception("Por favor, preencha o valor do combustível");

                decimal consumo = Convert.ToDecimal(entryConsumo.Text);
                decimal precoCombustivel = Convert.ToDecimal(entryValorC.Text);
                decimal distancia = Convert.ToDecimal(entryDistância.Text);

                decimal consumoCarro = (distancia / consumo) * precoCombustivel;

                decimal custoTotal = consumoCarro + valorTotalPedagios;

                string origem = entryOrigem.Text;
                string destino = entryDestino.Text;

                string mensagem = string.Format("Viagem de {0} a {1} custará {2}", origem, destino, custoTotal);

                await DisplayAlert("Resultado final", mensagem, "OK");

            } catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            try
            {
                bool confirmar = await DisplayAlert("Tem Certeza?", "Limpar todos os Dados?", "S", "N");

                if (confirmar)
                {
                    entryConsumo.Text = "";
                    entryDestino.Text = string.Empty;
                    entryDistância.Text = "";
                    entryOrigem.Text = "";
                    entryValorC.Text = "";
                    entryValorP.Text = "";

                    PropriedadesApp.ListaPedagios.Clear();
                }
            } catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }
        }
    }
}
