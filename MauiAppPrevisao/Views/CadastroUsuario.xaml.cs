using MauiAppPrevisao.Models;

namespace MauiAppPrevisao.Views
{
    public partial class CadastroUsuario : ContentPage
    {
        public CadastroUsuario()
        {
            InitializeComponent();
        }

        private async void btn_cadastrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Usuario u = new Usuario()
                {
                    Nome = txt_nome.Text,
                    DataNascimento = dt_nascimento.Date,
                    Email = txt_email.Text,
                    Senha = txt_senha.Text
                };

                string validacao = u.Validar();
                if (validacao != "")
                {
                    await DisplayAlert("Ops", validacao, "OK");
                    return;
                }

                await App.Db.InsertUsuario(u);

                await DisplayAlert("Sucesso!", "Usuário cadastrado com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
