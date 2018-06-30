using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultaCEP.Servico.Modelo;
using App01_ConsultaCEP.Servico;

namespace App01_ConsultaCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender ,EventArgs args)
        {
            RESULTADO.Text = "";
            string cep = CEP.Text;
            if (isValid(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                        RESULTADO.Text = string.Format("Endereço: {0}, {1} - {2}", end.logradouro, end.uf, end.localidade);
                    else
                        DisplayAlert("Erro", "Cep não localizado " + cep, "Ok");
                }catch(Exception ex)
                {
                    DisplayAlert("Erro", ex.Message, "Ok");
                }
            }
        }

        private bool isValid(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "O campo de cep deve ter oito caracteres", "Ok");
                valido = false;
            }

            int NovoCep = 0;
            if(!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("Erro", "O campo de cep deve ter somente numeros", "Ok");
                valido = false;
            }

            return valido;
        }
	}
}
