using MauiApp1MinhasCompras.Helpers;

namespace MauiApp1MinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper? _db; // Variável estática para armazenar a instância do banco de dados
        public static SQLiteDatabaseHelper Db // Propriedade de leitura para acessar o banco de dados
        {
            get
            {
                if (_db == null) // verifica se a instância do banco de dados já foi criada
                {
                    string path = Path.Combine(Environment.GetFolderPath // gera o caminho do arquivo do banco de dados, combinando diferentes partes do caminho
                        (Environment.SpecialFolder.LocalApplicationData), // retorna o caminho para a pasta de dados locais da aplicação
                        "banco_sqlite_compras.db3"); //nome do arquivo do banco de dados SQLite,o arquivo será criado ou aberto nesse local.

                    _db = new SQLiteDatabaseHelper(path); // Inicializa a instância do banco de dados se ainda não estiver criada
                }
                return _db;
            }
        }
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState) // Método para criar a janela principal da aplicação
        {
            return new Window(new NavigationPage(new Views.ListaProduto())); // MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
