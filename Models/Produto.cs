using SQLite;

namespace MauiApp1MinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        double _quantidade;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("A descrição é obrigatória!");
                }
                _descricao = value;
            }
        }
        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (value == 0)
                {
                    throw new Exception("A quantidade é obrigatória!");
                }
                _quantidade = value;
            }
        }
        public double Preco { get; set; }
        public double Total { get => Quantidade * Preco; }
    }
}
