using MauiApp1MinhasCompras.Models;

namespace MauiApp1MinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto //cria uma nova instância da classe Produto, reserva espaço na memória para um novo objeto do tipo Produto e pode definir os valores de suas propriedades.
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
            };
            await App.Db.Insert(p);
            await DisplayAlert("Sucesso", "Produto adicionado com sucesso!", "Ok");
        }
        catch (Exception ex)
        {
            _ = DisplayAlert("Ops", ex.Message, "Ok");

        }
    }

    private  async void Voltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); //Esse comando faz com que a página atual seja removida da pilha de navegação, retornando para a página anterior.
    }
}