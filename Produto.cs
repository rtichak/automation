using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroClientes
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double Estoque { get; set; }
        public DateTime DataValidade { get; set; }
        public bool EmPromocao { get; set; }
        public EmCategoria Categoria { get; set; }

        public Produto(int codigo, string nome, double estoque, DateTime dataValidade, bool emPromocao, EmCategoria categoria)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Estoque = estoque;
            this.DataValidade = dataValidade;
            this.EmPromocao = emPromocao;
            this.Categoria = categoria;
        }

        public static List<Produto> Listagem { get; set; }

        static Produto()
        {
            Produto.Listagem = new List<Produto>();
            Produto.Listagem.Add(new Produto(1, "Celular", 100, DateTime.Today.AddDays(365), false,
                EmCategoria.Eletrônicos));
            Produto.Listagem.Add(new Produto(2, "Televisão", 10, DateTime.Today.AddDays(365), false,
                EmCategoria.Eletrônicos));
            Produto.Listagem.Add(new Produto(3, "Tablet", 200, DateTime.Today.AddDays(365), false,
                EmCategoria.Eletrônicos));
            Produto.Listagem.Add(new Produto(4, "Fone de ouvido", 1000, DateTime.Today.AddDays(365), false,
                EmCategoria.Eletrônicos));
            Produto.Listagem.Add(new Produto(5, "Chocolate", 1200, DateTime.Today.AddDays(30), true,
                EmCategoria.Alimentos));
            Produto.Listagem.Add(new Produto(6, "Balas", 2200, DateTime.Today.AddDays(90), false,
                EmCategoria.Alimentos));
            Produto.Listagem.Add(new Produto(7, "Chiclete", 1200, DateTime.Today.AddDays(90), false,
                EmCategoria.Alimentos));
            Produto.Listagem.Add(new Produto(8, "Maça", 20, DateTime.Today.AddDays(5), false,
                EmCategoria.HortiFruti));
        }

    }

    public enum EmCategoria
    {
        Alimentos,
        Eletrônicos,
        HortiFruti

    }
}
