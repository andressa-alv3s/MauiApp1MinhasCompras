using MauiApp1MinhasCompras.Models;

namespace MauiApp1MinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto? produto_anexado = BindingContext as Produto; //recupera o objeto Produto que está vinculado ao contexto de dados da página.
            Produto p = new Produto //cria uma nova instância da classe Produto, reserva espaço na memória para um novo objeto do tipo Produto e pode definir os valores de suas propriedades.
            {
                Id = produto_anexado.Id, //atribui o valor da propriedade Id do objeto produto_anexado à propriedade Id do novo objeto p.
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
            };
            await App.Db.Update(p);
            await DisplayAlert("Sucesso", "Produto atualizado com sucesso!", "Ok");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            _ = DisplayAlert("Ops", ex.Message, "Ok");

        }
    }
}