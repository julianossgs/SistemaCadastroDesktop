using Cadastro1.Dados;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Cadastro1.Relatorios;

namespace Cadastro1.Views
{
    public partial class FrmClientes : Form
    {
        Conexao con = new Conexao();
        string id;
        public FrmClientes()
        {
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            ListarClientes();
            btNovo.Focus();
        } 

        public void LimparCampos()
        {
            txtCod.Clear();
            txtEmail.Clear();
            txtNome.Clear();
            txtSobrenome.Clear();
            nUDStatus.Value = 0;
        } 

        public void HabilitarCampos()
        {
            txtEmail.Enabled = true;
            txtNome.Enabled = true;
            txtSobrenome.Enabled = true;
            nUDStatus.Enabled = true;
            dtCadastro.Enabled = true;
            btSalvar.Enabled = true;
        }

        public void DesabilitarCampos()
        {
            txtEmail.Enabled = false;
            txtNome.Enabled = false;
            txtSobrenome.Enabled = false;
            nUDStatus.Enabled = false;
            dtCadastro.Enabled = false;
           
        }

        public void ListarClientes()
        {
            con.AbrirConexao();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = con.Con;
            Cmd.CommandText = "spListarClientes";
            Cmd.CommandType = CommandType.StoredProcedure;
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter();
            Da.SelectCommand = Cmd;
            Da.Fill(Dt);
            gridClientes.DataSource = Dt;
            con.FecharConexao();
        } 

        private void BuscarClienteNome()
        {
            con.AbrirConexao();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = con.Con;
            Cmd.CommandText = "spBuscarClienteNome";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@Nome", txtBuscar.Text);
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter();
            Da.SelectCommand = Cmd;
            Da.Fill(Dt);
            gridClientes.DataSource = Dt;
            con.FecharConexao();
        }

        //Método que valida o Email
        public bool ValidaEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, ("(?<user>[^@]+)@(?<host>.+)"));
        }

        private void btNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
            HabilitarCampos();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == string.Empty)
            {
                MessageBox.Show("Informe o Nome do Cliente!");
                txtNome.Focus();
                return;
            }

            if (txtSobrenome.Text == string.Empty)
            {
                MessageBox.Show("Informe o Sobrenome do Cliente!");
                txtSobrenome.Focus();
                return;
            }

            if (txtEmail.Text != string.Empty)
            {
                if (!ValidaEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email inválido! Informe um Email válido!");
                    txtEmail.Clear();
                    txtEmail.Focus();
                    return;
                }
            }

            con.AbrirConexao();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = con.Con;
            Cmd.CommandText = "spInserirClientes";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
            Cmd.Parameters.AddWithValue("@Sobrenome", txtSobrenome.Text);
            Cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            Cmd.Parameters.AddWithValue("@Ativo", nUDStatus.Value);
            Cmd.Parameters.AddWithValue("@DataCadastro",Convert.ToDateTime (dtCadastro.Text));
            Cmd.ExecuteNonQuery();

            MessageBox.Show("Registro inserido com sucesso!");
            LimparCampos();
            ListarClientes();
            DesabilitarCampos();
            btNovo.Focus();
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            //obtendo o valor da 1ª linha do grid p/ exclusão
            id = gridClientes.CurrentRow.Cells["IdCliente"].Value.ToString();
            txtNome.Text = gridClientes.CurrentRow.Cells[1].Value.ToString();

            //extraindo Cliente para exclusão
            con.AbrirConexao();
            SqlCommand CmdExtrair = new SqlCommand();
            CmdExtrair.Connection = con.Con;
            CmdExtrair.CommandText = "spSelecionarClienteNome";
            CmdExtrair.CommandType = CommandType.StoredProcedure;
            CmdExtrair.Parameters.AddWithValue("@Nome", txtNome.Text);
            SqlDataReader reader;
            reader = CmdExtrair.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Globais.nomeCliente = Convert.ToString(reader["Nome"]);
                }

            }

            var result = MessageBox.Show("Deseja Excluir o(a) Cliente - " + Globais.nomeCliente, "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                con.AbrirConexao();
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con.Con;
                Cmd.CommandText = "spExcluirCliente";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@IdCliente", id);
                Cmd.ExecuteNonQuery();
                con.FecharConexao();

                MessageBox.Show("Registro excluido com sucesso!");
                LimparCampos();
                ListarClientes();
            }
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

        private void btAlterar_Click(object sender, EventArgs e)
        {
            Globais.idcliente = gridClientes.CurrentRow.Cells[0].Value.ToString();
            Globais.nomeCliente = gridClientes.CurrentRow.Cells[1].Value.ToString();
            Globais.sobrenomeCliente = gridClientes.CurrentRow.Cells[2].Value.ToString();
            Globais.email = gridClientes.CurrentRow.Cells[3].Value.ToString();
            Globais.dataCadastro = Convert.ToDateTime(gridClientes.CurrentRow.Cells[4].Value.ToString());
            Globais.ativo = Convert.ToInt32(gridClientes.CurrentRow.Cells[5].Value.ToString());

            FrmGestaoClientes frmGestaoClientes = new FrmGestaoClientes();
            frmGestaoClientes.ShowDialog();
            this.Close();
        }

        private void gridClientes_SelectionChanged(object sender, EventArgs e)
        {
            DesabilitarCampos();
            id = gridClientes.CurrentRow.Cells[0].Value.ToString();
            txtCod.Text = gridClientes.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = gridClientes.CurrentRow.Cells[1].Value.ToString();
            txtSobrenome.Text = gridClientes.CurrentRow.Cells[2].Value.ToString();
            txtEmail.Text = gridClientes.CurrentRow.Cells[3].Value.ToString();
            dtCadastro.Text = gridClientes.CurrentRow.Cells[4].Value.ToString();
            nUDStatus.Value = Convert.ToInt32(gridClientes.CurrentRow.Cells[5].Value.ToString());
        }

        private void btImprimir_Click(object sender, EventArgs e)
        {
            FrmRelClientes frmRel = new FrmRelClientes();
            frmRel.ShowDialog();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarClienteNome();
        }
    }
}
