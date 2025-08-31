namespace MauiApp1MinhasCompras.Views;
using System.Collections.ObjectModel;
using MauiApp1MinhasCompras.Models;
public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    private CancellationTokenSource? _cts; // usado para debounce

    public ListaProduto()
	{
		InitializeComponent();
        lst_produtos.ItemsSource = lista;
    }
	protected override async void OnAppearing()
	{
        lista.Clear();
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

        // Cancela buscas anteriores
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            // Aguarda 500ms antes de executar a busca
            await Task.Delay(500, token);

            if (token.IsCancellationRequested)
                return;

            // Atualiza lista com base no filtro
            lista.Clear();
            List<Produto> tmp = await App.Db.Search(q);
            tmp.ForEach(i => lista.Add(i));
        }
        catch (TaskCanceledException)
        {
            // ignorado porque é esperado no debounce
        }
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
                await App.Db.Delete(produto.Id);

                if (lista.Contains(produto))
                    lista.Remove(produto);

                await DisplayAlert("Sucesso", "Produto removido com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao remover produto: {ex.Message}", "OK");
            }
        }
    }



}