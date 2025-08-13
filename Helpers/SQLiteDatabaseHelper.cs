using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1MinhasCompras.Models;
using SQLite;

namespace MauiApp1MinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper //classe para auxiliar na criação e manipulação do banco de dados SQLite
    {
        readonly SQLiteAsyncConnection _conn;   //declaração da variável de conexão com o banco de dados SQLite

        public SQLiteDatabaseHelper(string Path)    //construtor da classe SQLiteDatabaseHelper
        {
            _conn = new SQLiteAsyncConnection(Path); //inicializa a conexão com o banco de dados SQLite
            _conn.CreateTableAsync<Models.Produto>().Wait(); //instrução para criar a tabela Produto    
        }

        public Task<int> Insert(Produto p) { //método para inserir um produto no banco de dados
            return _conn.InsertAsync(p); //instrução para inserir o produto na tabela Produto
        }
        public Task<List<Produto>> Update(Produto p) { //método para atualizar um produto no banco de dados
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=? "; //declaração da instrução SQL para atualizar o produto
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id); //instrução para atualizar o produto na tabela Produto
        }
        public Task<int> Delete(int id) { //método para excluir um produto do banco de dados
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id); //instrução para excluir o produto da tabela Produto
        }
        public Task<List<Produto>> GetAll() {
            return _conn.Table<Produto>().ToListAsync(); //instrução para buscar todos os produtos na tabela Produto
        }
        public Task<List<Produto>> Search(string q) { //método para buscar produtos no banco de dados
            string sql = "SELECT * Produto WHERE descricao LIKE '%" + q + "%'";//declaração da instrução SQL para buscar produtos
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
