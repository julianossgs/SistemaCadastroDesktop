using Cadastro1.Dados;
using Cadastro1.Relatorios;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cadastro1.Views
{
    public partial class FrmProdutos : Form
    {
        Conexao con = new Conexao();
        string id;
        public FrmProdutos()
        {
            InitializeComponent();
        }

        private void FrmProdutos_Load(object sender, EventArgs e)
        {
            CarregarCBClientes();
            ListarProdutos();
            btNovo.Focus();
        }

        private void LimparCampos()
        {
            txtCod.Clear();
            txtProduto.Clear();
            txtValor.Clear();
            cBClientes.Text = "";
            nUDStatus.Value = 0;
        }

        private void HabilitarCampos()
        {
            txtProduto.Enabled = true;
            txtValor.Enabled = true;
            cBClientes.Enabled = true;
            nUDStatus.Enabled = true;
            btSalvar.Enabled = true;
        }

        private void DesabilitarCampos()
        {
            txtProduto.Enabled = false;
            txtValor.Enabled = false;
            cBClientes.Enabled = false;
            nUDStatus.Enabled = false;
            btSalvar.Enabled = false;
        }

        //Carregar combo Box Clientes
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

        public void ListarProdutos()
        {
            con.AbrirConexao();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = con.Con;
            Cmd.CommandText = "spListarProdutos";
            Cmd.CommandType = CommandType.StoredProcedure;
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter();
            Da.SelectCommand = Cmd;
            Da.Fill(Dt);
            gridProdutos.DataSource = Dt;
            con.FecharConexao();
        }

        private void btNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
            HabilitarCampos();
            txtProduto.Focus();

        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProduto.Text == string.Empty)
                {
                    MessageBox.Show("Informe o Produto!");
                    txtProduto.Focus();
                    return;
                }

                if (txtValor.Text == string.Empty)
                {
                    MessageBox.Show("Informe o Valor!");
                    txtValor.Focus();
                    return;
                }

                con.AbrirConexao();
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con.Con;
                Cmd.CommandText = "spInserirProdutos";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Nome", txtProduto.Text);
                Cmd.Parameters.AddWithValue("@Valor",Convert.ToDecimal(txtValor.Text));
                Cmd.Parameters.AddWithValue("@Id_Cliente", cBClientes.SelectedValue);
                Cmd.Parameters.AddWithValue("@Disponivel", nUDStatus.Value);

                Cmd.ExecuteNonQuery();

                MessageBox.Show("Registro inserido com sucesso!");
                LimparCampos();
                HabilitarCampos();
                ListarProdutos();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro no Método de inserir: " + ex.Message);
            }
           
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            //obtendo o valor da 1ª linha do grid p/ exclusão
            id = gridProdutos.CurrentRow.Cells["IdProduto"].Value.ToString();
            txtProduto.Text = gridProdutos.CurrentRow.Cells[1].Value.ToString();

            //extraindo Produto para exclusão
            con.AbrirConexao();
            SqlCommand CmdExtrair = new SqlCommand();
            CmdExtrair.Connection = con.Con;
            CmdExtrair.CommandText = "spSelecionarProdutoNome";
            CmdExtrair.CommandType = CommandType.StoredProcedure;
            CmdExtrair.Parameters.AddWithValue("@Nome", txtProduto.Text);
            SqlDataReader reader;
            reader = CmdExtrair.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Globais.nomeProduto = Convert.ToString(reader["Nome"]);
                }

            }

            var result = MessageBox.Show("Deseja Excluir o Produto - " + Globais.nomeProduto, "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                con.AbrirConexao();
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con.Con;
                Cmd.CommandText = "spExcluirProduto";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@IdProduto", id);
                Cmd.ExecuteNonQuery();
                con.FecharConexao();

                MessageBox.Show("Registro excluido com sucesso!");
                LimparCampos();
                ListarProdutos();
            }
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            Globais.idproduto = gridProdutos.CurrentRow.Cells[0].Value.ToString();
            Globais.nomeProduto = gridProdutos.CurrentRow.Cells[1].Value.ToString();
            Globais.valorProduto = Convert.ToDecimal(gridProdutos.CurrentRow.Cells[2].Value.ToString());
            Globais.disponivelProduto = Convert.ToInt32(gridProdutos.CurrentRow.Cells[3].Value.ToString());
           
            Globais.nomeCliente = gridProdutos.CurrentRow.Cells[4].Value.ToString();

            FrmGestaoProdutos frmGestaoProdutos = new FrmGestaoProdutos();
            frmGestaoProdutos.ShowDialog();
            this.Close();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            DesabilitarCampos();
            btNovo.Focus();
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void gridProdutos_SelectionChanged(object sender, EventArgs e)
        {
            DesabilitarCampos();
            id = gridProdutos.CurrentRow.Cells[0].Value.ToString();
            txtCod.Text = gridProdutos.CurrentRow.Cells[0].Value.ToString();
            txtProduto.Text = gridProdutos.CurrentRow.Cells[1].Value.ToString();
            txtValor.Text = gridProdutos.CurrentRow.Cells[2].Value.ToString();
            cBClientes.Text = gridProdutos.CurrentRow.Cells[4].Value.ToString();
           
            nUDStatus.Value = Convert.ToInt32(gridProdutos.CurrentRow.Cells[3].Value.ToString());
        }

        private void btImprimir_Click(object sender, EventArgs e)
        {
            FrmRelProdutos frmRelProdutos = new FrmRelProdutos();
            frmRelProdutos.ShowDialog();
        }
    }
}
