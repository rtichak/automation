using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroClientes
{
    public partial class Produtos : Form
    {
        public Produtos()
        {
            InitializeComponent();
            dgvProdutos.DataSource = Produto.Listagem;
        }

        private void Produtos_Load(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnVenda_Click(object sender, EventArgs e)
        {
            frmPedido Chamar = new frmPedido();
            Chamar.ShowDialog();
        }
    }
}
