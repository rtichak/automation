using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CadastroClientes
{
    public partial class Formulario : Form
    {
        public Formulario()
        {
            InitializeComponent();
            AtualizarComboBoxPaises();
            CriarControlesEstadosCivis();
            DesabilitarCampos();
        }

        private void Informar(string mensagem)
        {
            MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private bool Confirmar(string pergunta)
        {
            return MessageBox.Show(pergunta, "Confirmação", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void AtualizarComboBoxPaises()
        {
            cbxNacionalidade.DataSource = Pais.Listagem;
            cbxNacionalidade.DisplayMember = "";
            cbxNacionalidade.DisplayMember = "Nome";
            cbxNacionalidade.ValueMember = "Sigla";
            cbxNacionalidade.SelectedIndex = -1;
        }

        private void AtualizarComboBoxClientes()
        {
            cbxCliente.DataSource = Cliente.Listagem;
            cbxCliente.DisplayMember = "";
            cbxCliente.DisplayMember = "Nome";
            cbxCliente.ValueMember = "Id";
        }

        private void CorrigirTabStop(object sender, EventArgs e)
        {
            ((RadioButton)sender).TabStop = true;
        }

        private void CriarControlesEstadosCivis()
        {
            int iRB = 0;
            var estadosCivis = Enum.GetValues(typeof(EnumEstadoCivil));
            foreach (var ec in estadosCivis)
            {
                RadioButton rb = new RadioButton()
                {
                    Text = ec.ToString(),
                    Location = new Point(10, (iRB + 1) * 27),
                    Width = 85,
                    TabStop = true,
                    TabIndex = iRB,
                    Tag = ec
                };
                rb.TabStopChanged += new EventHandler(CorrigirTabStop);
                gbxEstadoCivil.Controls.Add(rb);
                iRB++;
            }
        }

        private EnumEstadoCivil? LerEstadoCivil()
        {
            foreach (var control in gbxEstadoCivil.Controls)
            {
                RadioButton rb = control as RadioButton;
                if (rb.Checked)
                {
                    return (EnumEstadoCivil)(rb.Tag);
                }
            }
            return null;
        }

        private void MarcarEstadoCivil (EnumEstadoCivil estadoCivil)
        {
            foreach (var control in gbxEstadoCivil.Controls)
            {
                RadioButton rb = control as RadioButton;
                if ((EnumEstadoCivil)(rb.Tag) == estadoCivil)
                {
                    rb.Checked = true;
                    return;
                }
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            mtbCpf.Clear();
            mtbCep.Clear();
            mtbTelefone.Clear();
            mtbEmail.Clear();
            dtpDataNascimento.Value = DateTime.Now.Date;
            nudRendaMensal.Value = 0;
            foreach(var control in gbxEstadoCivil.Controls)
            {
                (control as RadioButton).Checked = false;
            }
            cbxNacionalidade.SelectedIndex = -1;
        }

        private void PreencherCamposComCliente(Cliente cliente)
        {
            txtId.Text = cliente.Id.ToString();
            txtNome.Text = cliente.Nome;
            txtEndereco.Text = cliente.Endereco;
            txtBairro.Text = cliente.Bairro;
            mtbCep.Text = cliente.CEP.ToString();
            mtbCpf.Text = cliente.CPF.ToString();
            mtbTelefone.Text = cliente.Telefone.ToString();
            mtbCpf.Text = cliente.Email.ToString();
            dtpDataNascimento.Value = cliente.DataNascimento;
            nudRendaMensal.Value = cliente.RendaMensal;
            MarcarEstadoCivil(cliente.EstadoCivil);
            cbxNacionalidade.SelectedValue = cliente.Nacionalidade;
        }

        private void PreencherClienteComCampos(Cliente cliente)
        {
            cliente.Nome = txtNome.Text;
            cliente.Endereco = txtEndereco.Text;
            cliente.Bairro = txtBairro.Text;
            cliente.CEP = Convert.ToInt64(mtbCep.Text);
            cliente.CPF = Convert.ToInt64(mtbCpf.Text);
            cliente.DataNascimento = dtpDataNascimento.Value.Date;
            cliente.RendaMensal = nudRendaMensal.Value;
            cliente.EstadoCivil = LerEstadoCivil().Value;
            cliente.Nacionalidade = cbxNacionalidade.SelectedValue.ToString();
            cliente.Telefone = mtbTelefone.Text;
            cliente.Email = mtbEmail.Text;
        }

        private bool PreencheuTodosOsCampos()
        {
            if (String.IsNullOrWhiteSpace(txtNome.Text)) return false;
            if (String.IsNullOrWhiteSpace(txtEndereco.Text)) return false;
            if (String.IsNullOrWhiteSpace(txtBairro.Text)) return false;
            if (String.IsNullOrWhiteSpace(mtbCep.Text)) return false;
            if (String.IsNullOrWhiteSpace(mtbCpf.Text)) return false;
            if (dtpDataNascimento.Value.Date == DateTime.Now.Date) return false;
            if (nudRendaMensal.Value == 0) return false;
            if (LerEstadoCivil() == null) return false;
            if (cbxNacionalidade.SelectedIndex < 0) return false;
            if (String.IsNullOrWhiteSpace(mtbTelefone.Text)) return false;
            if (String.IsNullOrWhiteSpace(mtbEmail.Text)) return false;

            return true;

        }

        private bool PossuiValresNaoSalvos()
        {
            if (cbxCliente.SelectedIndex < 0)
            {
                if (!String.IsNullOrWhiteSpace(txtNome.Text)) return true;
                if (!String.IsNullOrWhiteSpace(txtEndereco.Text)) return true;
                if (!String.IsNullOrWhiteSpace(txtBairro.Text)) return true;
                if (!String.IsNullOrWhiteSpace(mtbCep.Text)) return true;
                if (!String.IsNullOrWhiteSpace(mtbCpf.Text)) return true;
                if (dtpDataNascimento.Value.Date != DateTime.Now.Date) return true;
                if (nudRendaMensal.Value > 0) return true;
                if (LerEstadoCivil() != null) return true;
                if (cbxNacionalidade.SelectedIndex >= 0) return true;
                if (String.IsNullOrWhiteSpace(mtbTelefone.Text)) return true;
                if (String.IsNullOrWhiteSpace(mtbEmail.Text)) return true;
            }
            else
            {
                Cliente cliente = cbxCliente.SelectedItem as Cliente;
                if (txtNome.Text.Trim() != cliente.Nome) return true;
                if (txtEndereco.Text.Trim() != cliente.Endereco) return true;
                if (txtBairro.Text.Trim() != cliente.Bairro) return true;
                if (mtbCep.Text != cliente.CEP.ToString()) return true;
                if (mtbCpf.Text != cliente.CPF.ToString()) return true;
                if (dtpDataNascimento.Value.Date != cliente.DataNascimento) return true;
                if (nudRendaMensal.Value != cliente.RendaMensal) return true;
                if (LerEstadoCivil() != cliente.EstadoCivil) return true;
                if (cbxNacionalidade.SelectedIndex.ToString() != cliente.Nacionalidade) return true;
                if (mtbTelefone.Text.Trim() != cliente.Telefone) return true;
                if (mtbEmail.Text.Trim() != cliente.Email) return true;
            }
            return false;
        }

        private void AlterarEstadoDosCampos(bool estado)
        {
            txtNome.Enabled = estado;
            txtEndereco.Enabled = estado;
            txtBairro.Enabled = estado;
            mtbCep.Enabled = estado;
            mtbCpf.Enabled = estado;
            dtpDataNascimento.Enabled = estado;
            nudRendaMensal.Enabled = estado;
            gbxEstadoCivil.Enabled = estado;
            cbxNacionalidade.Enabled = estado;
            btnCancelar.Enabled = estado;
            btnSalvar.Enabled = estado;
            mtbTelefone.Enabled = estado;
            mtbEmail.Enabled = estado;
        }

        private void HabilitarCampos()
        {
            AlterarEstadoDosCampos(true);
        }

        private void DesabilitarCampos()
        {
            AlterarEstadoDosCampos(false);
        }

        private void SalvarCliente()
        {
            var linha = txtId.Text + "; " +
                        txtNome.Text + "; " +
                        txtEndereco.Text + "; " +
                        txtBairro.Text + "; " +
                        mtbCpf.Text + "; " +
                        mtbCep.Text + "; " +
                        mtbTelefone.Text + "; " +
                        mtbEmail.Text + "; " +
                        dtpDataNascimento.Value.Date + "; " +
                        nudRendaMensal.Value + "; " +
                        LerEstadoCivil() + "; " +
                        cbxNacionalidade.Text ;

            using (StreamWriter arquivo = new StreamWriter(@"C:/ProjetoCuponFiscal/CadastroClientes/dados/clientes.csv", true))
                arquivo.WriteLine(linha);

        }

        private void Formulario_Load(object sender, EventArgs e)
        {

        }

        private void Formulario_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PossuiValresNaoSalvos())
                e.Cancel = !Confirmar("Há alterações não salvas. Deseja realmente sair?");
            else
                e.Cancel = !Confirmar("Deseja realmente sair?");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            cbxCliente.SelectedIndex = -1;
            LimparCampos();
            HabilitarCampos();
            txtNome.Select();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (PreencheuTodosOsCampos())
            {
                Cliente cliente = cbxCliente.SelectedIndex < 0 ?
                    new Cliente() : cbxCliente.SelectedItem as Cliente;
                PreencherClienteComCampos(cliente);
                DesabilitarCampos();

                if (cbxCliente.SelectedIndex < 0)
                {
                    cliente = Cliente.Inserir(cliente);
                    AtualizarComboBoxClientes();
                    SalvarCliente();
                    Informar("Cliente cadastrado com sucesso!");
                    
                       
                }
                else
                {
                    AtualizarComboBoxClientes();
                    Informar("Cliente alterado com sucesso");
                }
            }
            else
            {
                Informar("Só é possível salvar se todos os campos forem preenchidos. Salvamento não realizado!");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(PossuiValresNaoSalvos())
            {
                if (Confirmar("Há alterações não salvas. Deseja realmente cancelar?"))
                {
                    if (cbxCliente.SelectedIndex == -1)
                        LimparCampos();
                    else
                        PreencherCamposComCliente(cbxCliente.SelectedItem as Cliente);
                    cbxCliente.Select();
                    DesabilitarCampos();
                }
            }
            else
            {
                if (cbxCliente.SelectedIndex == -1)
                LimparCampos();
                    else
                    PreencherCamposComCliente(cbxCliente.SelectedItem as Cliente);
                cbxCliente.Select();
                DesabilitarCampos();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            HabilitarCampos();
            PreencherCamposComCliente(cbxCliente.SelectedItem as Cliente);
            txtNome.Select();
        }

        private void cbxCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCliente.SelectedIndex < 0)
            {
                btnEditar.Enabled = false;
            }
            else
            {
                PreencherCamposComCliente(cbxCliente.SelectedItem as Cliente);
                btnEditar.Enabled = true;
            }
        }

        private void nudRendaMensal_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnProduto_Click(object sender, EventArgs e)
        {
            Produtos Chamar = new Produtos();
            Chamar.ShowDialog();
        }
    }
}
