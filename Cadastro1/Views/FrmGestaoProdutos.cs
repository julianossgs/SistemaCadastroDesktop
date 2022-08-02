using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cadastro1.Dados;
using System.Data.SqlClient;

namespace Cadastro1.Views
{
    public partial class FrmGestaoProdutos : Form
    {
        Conexao con = new Conexao();
        public FrmGestaoProdutos()
        {
            InitializeComponent();
        }

        private void FrmGestaoProdutos_Load(object sender, EventArgs e)
        {
            HabilitarCampos();
            CarregarCBClientes();
            txtProduto.Focus();
        }

        private void FrmGestaoProdutos_Activated(object sender, EventArgs e)
        {
            txtCod.Text = Globais.idproduto;
            txtProduto.Text = Globais.nomeProduto;
            cBClientes.Text = Globais.nomeCliente;
            nUDStatus.Value = Globais.disponivelProduto;
            txtValor.Text = Globais.valorProduto.ToString();
        }

        private void HabilitarCampos()
        {
            txtProduto.Enabled = true;
            txtValor.Enabled = true;
            cBClientes.Enabled = true;
            nUDStatus.Enabled = true;
        }

        private void CarregarCBClientes()
        {
            con.AbrirConexao();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = con.Con;
            Cmd.CommandText = "spListarClientes";
            Cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = Cmd;
            Da.Fill(Dt);
            cBClientes.DataSource = Dt;
            cBClientes.ValueMember = "IdCliente";
            cBClientes.DisplayMember = "Nome";
            con.FecharConexao();
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            con.AbrirConexao();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = con.Con;
            Cmd.CommandText = "spAlterarProdutos";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@IdProduto", txtCod.Text);
            Cmd.Parameters.AddWithValue("@Nome", txtProduto.Text);
            Cmd.Parameters.AddWithValue("@Id_Cliente", cBClientes.SelectedValue);
            Cmd.Parameters.AddWithValue("@Disponivel", nUDStatus.Value);
            Cmd.Parameters.AddWithValue("@Valor", Convert.ToDecimal(txtValor.Text));
            Cmd.ExecuteNonQuery();
            con.FecharConexao();

            MessageBox.Show("Registro alterado com sucesso!");
            this.Close();
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

        private void btFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
