using MauiAppPrevisao.Models;

namespace MauiAppPrevisao.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btn_login_Clicked(object sender, EventArgs e)
        {
            try
            {
                // usa o método de login do helper
                var usuario = await App.Db.Login(txt_email.Text, txt_senha.Text);

                if (usuario == null)
                {
                    await DisplayAlert("Ops", "Usuário ou senha incorretos!", "OK");
                    return;
                }

                // Se tiver sucesso, navegar para a tela de previsão 
                await Navigation.PushAsync(new PrevisaoTempo());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void btn_cadastro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroUsuario());
        }
    }
}