using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroClientes
{
    public enum EnumEstadoCivil
    {
        Solteiro,
        Casado,
        Divorciado,
        Viuvo
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public long CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal RendaMensal { get; set; }
        public EnumEstadoCivil EstadoCivil { get; set; }
        public string Nacionalidade { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public object CEP { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Cliente(string nome, long cPF, DateTime dataNascimento, decimal rendaMensal,
            EnumEstadoCivil estadoCivil, string nacionalidade, string telefone, string email, long cep)
        {
            this.Id = 0;
            this.Nome = nome;
            this.CPF = cPF;
            this.DataNascimento = dataNascimento;
            this.RendaMensal = rendaMensal;
            this.EstadoCivil = estadoCivil;
            this.Nacionalidade = nacionalidade;
            this.CEP = cep;
            this.Telefone = telefone;
            this.Email = email;
        }

        public Cliente()
        {
            this.Id = 0;
        }

        public static List<Cliente> Listagem { get; set; }

        static Cliente()
        {
            Cliente.Listagem = new List<Cliente>();
        }

        public static Cliente Inserir(Cliente cliente)
        {
            int id = Cliente.Listagem.Count > 0 ?
                Cliente.Listagem.Max(c => c.Id) + 1 : 1;
            cliente.Id = id;
            Cliente.Listagem.Add(cliente);
            return cliente;
        }
    }
}
