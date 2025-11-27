using MauiAppPrevisao.Models;
using MauiAppPrevisao.Services;

namespace MauiAppPrevisao.Views
{
    public partial class PrevisaoTempo : ContentPage
    {
        readonly PrevisaoService _service;

        public PrevisaoTempo()
        {
            InitializeComponent();
            _service = new PrevisaoService();
        }

        private async void BuscarPrevisao_Clicked(object sender, EventArgs e)
        {
            try
            {
                string cidade = txtCidade.Text?.Trim();

                if (string.IsNullOrWhiteSpace(cidade))
                {
                    await DisplayAlert("Ops", "Digite a cidade.", "OK");
                    return;
                }

                var previsao = _service.Buscar(cidade);

                if (previsao == null)
                {
                    resultadoFrame.IsVisible = false;
                    await DisplayAlert("Ops", "Cidade não encontrada (offline).", "OK");
                    return;
                }

                resultadoFrame.IsVisible = true;
                lblCidade.Text = previsao.Cidade;
                lblTemp.Text = $"{previsao.Temperatura}°C";
                lblUmidade.Text = $"{previsao.Umidade}%";
                lblCondicao.Text = previsao.Condicao;

                HistoricoConsulta h = new HistoricoConsulta
                {
                    Cidade = previsao.Cidade,
                    Temperatura = previsao.Temperatura,
                    Clima = previsao.Condicao,
                    DataConsulta = DateTime.Now,
                    UsuarioId = App.UsuarioLogadoId
                };

                await App.Db.InsertHistorico(h);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void VerHistorico_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new HistoricoConsultas());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
