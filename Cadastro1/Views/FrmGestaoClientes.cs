using Cadastro1.Dados;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cadastro1.Views
{
    public partial class FrmGestaoClientes : Form
    {
        Conexao con = new Conexao();
        FrmClientes frmClientes = new FrmClientes();
        public FrmGestaoClientes()
        {
            InitializeComponent();
        }

        private void FrmGestaoClientes_Load(object sender, EventArgs e)
        {
            HabilitarCampos();
            txtNome.Focus();
        }

        private void FrmGestaoClientes_Activated(object sender, EventArgs e)
        {
           
            txtCod.Text = Globais.idcliente;
            txtNome.Text = Globais.nomeCliente;
            txtSobrenome.Text = Globais.sobrenomeCliente;
            txtEmail.Text = Globais.email;
            nUDStatus.Value = Globais.ativo;
        }
        private void HabilitarCampos()
        {
            txtEmail.Enabled = true;
            txtNome.Enabled = true;
            txtSobrenome.Enabled = true;
            nUDStatus.Enabled = true;
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            con.AbrirConexao();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = con.Con;
            Cmd.CommandText = "spAlterarClientes";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@IdCliente", txtCod.Text);
            Cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
            Cmd.Parameters.AddWithValue("@Sobrenome", txtSobrenome.Text);
            Cmd.Parameters.AddWithValue("@DataCadastro", dtCadastro.Text);
            Cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            Cmd.Parameters.AddWithValue("@Ativo", nUDStatus.Value);

            Cmd.ExecuteNonQuery();
            con.FecharConexao();

            MessageBox.Show("Registro alterado com sucesso!");
            this.Close();
            
        }

        
         //Cliquei no evento errado
        private void pCadastro_Paint(object sender, PaintEventArgs e)
        {
        }
        
        private void btFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja Cancelar a Edição?", "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Close();
            }

            else
            {
                btAlterar.Focus();
            }
        }
    }
}
