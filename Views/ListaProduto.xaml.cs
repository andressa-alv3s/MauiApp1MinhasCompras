namespace MauiApp1MinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	public ListaProduto()
	{
		InitializeComponent();
	}

    private void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());// faz a navega��o para p�gina
		} 
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "Ok");

		}
	}
}