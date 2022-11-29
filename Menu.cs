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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPedido Pedido = new frmPedido();
            Pedido.MdiParent = this;
            Pedido.Show();
        }
    }
}
