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
        try
        {
            lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");

        }
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
        try
        {
            // Obter o MenuItem clicado
            MenuItem selecionado = sender as MenuItem;
            Produto p = selecionado?.BindingContext as Produto; // Acessa o produto vinculado ao MenuItem
            bool confirm = await DisplayAlert("Atenção", $"Confirma a exclusão do produto {p.Descricao}?", "Sim", "Não");
            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto; // Obtém o produto selecionado na lista

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}