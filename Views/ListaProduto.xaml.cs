namespace MauiApp1MinhasCompras.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MauiApp1MinhasCompras.Models;
public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    public ListaProduto()
	{
		InitializeComponent();
        lst_produtos.ItemsSource = lista;
    }
	protected override async void OnAppearing()
	{
		List<Produto> tmp = await App.Db.GetAll();
		tmp.ForEach(i => lista.Add(i));
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());// faz a navegação para página
		} 
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "Ok");

		}
	}

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		string q = e.NewTextValue;
		lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

		tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);
		string msg = string.Format("Total da compra: {0:C2}", soma);
		DisplayAlert("Total dos produtos", msg, "Ok");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        // Obter o MenuItem clicado
        var menuItem = sender as MenuItem;
        var produto = menuItem?.BindingContext as Produto; // Acessa o produto vinculado ao MenuItem

        if (produto != null)
        {
            try
            {
                // Excluir o produto do banco de dados
                await App.Db.Delete(produto.Id); // Chama o método Delete no banco de dados

                // Atualizar a lista local no ListView
                var listaProdutos = lst_produtos.ItemsSource as ObservableCollection<Produto>; // Sua lista de produtos
                if (listaProdutos != null)
                {
                    listaProdutos.Remove(produto); // Remove o produto da lista local
                }

                // Mensagem de sucesso
                await DisplayAlert("Sucesso", "Produto removido com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                // Em caso de erro
                await DisplayAlert("Erro", $"Erro ao remover produto: {ex.Message}", "OK");
            }
        }
    }



}