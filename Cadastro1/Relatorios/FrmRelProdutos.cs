using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro1.Relatorios
{
    public partial class FrmRelProdutos : Form
    {
        public FrmRelProdutos()
        {
            InitializeComponent();
        }

        private void FrmRelProdutos_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'cadastro1DataSet.ListarProdutos'. Você pode movê-la ou removê-la conforme necessário.
            this.listarProdutosTableAdapter.Fill(this.cadastro1DataSet.ListarProdutos);

            this.reportViewer1.RefreshReport();
        }
    }
}
