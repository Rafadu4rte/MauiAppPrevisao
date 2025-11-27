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
                var usuario = await App.Db.Login(txt_email.Text, txt_senha.Text);

                if (usuario == null)
                {
                    await DisplayAlert("Ops", "Usuário ou senha incorretos!", "OK");
                    return;
                }

                App.UsuarioLogadoId = usuario.Id;

                await DisplayAlert("Sucesso!", "Login realizado com sucesso.", "Ok");

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
