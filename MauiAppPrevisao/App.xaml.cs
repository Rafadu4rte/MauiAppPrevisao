using MauiAppPrevisao.Helpers;
using System.Globalization;

namespace MauiAppPrevisao
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelpers _db;

        public static SQLiteDatabaseHelpers Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_previsao.db3");

                    _db = new SQLiteDatabaseHelpers(path);
                }

                return _db;
            }
        }

        public static int UsuarioLogadoId { get; set; }

        public App()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            MainPage = new NavigationPage(new Views.LoginPage());
        }
    }
}
