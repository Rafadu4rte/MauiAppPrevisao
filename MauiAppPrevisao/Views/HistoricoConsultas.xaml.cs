using MauiAppPrevisao.Models;
using System.Collections.ObjectModel;

namespace MauiAppPrevisao.Views
{
    public partial class HistoricoConsultas : ContentPage
    {
        ObservableCollection<HistoricoConsulta> lista = new ObservableCollection<HistoricoConsulta>();

        public HistoricoConsultas()
        {
            InitializeComponent();
            lst_historico.ItemsSource = lista;

            CarregarHistorico();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(1);
            CarregarHistorico();
        }

        private async void CarregarHistorico()
        {
            try
            {
                lista.Clear();
                int idUser = App.UsuarioLogadoId;

                var tmp = await App.Db.GetHistoricoByUsuario(idUser);
                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
            finally
            {
                lst_historico.IsRefreshing = false;
            }
        }

        private async void btn_filtrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                lista.Clear();
                int idUser = App.UsuarioLogadoId;

                DateTime inicio = dt_inicio.Date;
                DateTime fim = dt_fim.Date.AddDays(1).AddSeconds(-1);

                var tmp = await App.Db.FiltrarHistorico(idUser, inicio, fim);
                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
            finally
            {
                lst_historico.IsRefreshing = false;
            }
        }

        private async void MenuItem_Remover_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = sender as MenuItem;
                var item = mi.BindingContext as HistoricoConsulta;

                bool confirma = await DisplayAlert("Tem Certeza?", $"Remover '{item.Cidade}'?", "Sim", "Não");
                if (!confirma) return;

                await App.Db.DeleteHistorico(item.Id);
                lista.Remove(item);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void lst_historico_Refreshing(object sender, EventArgs e)
        {
            CarregarHistorico();
        }
    }
}
