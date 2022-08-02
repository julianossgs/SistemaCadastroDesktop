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
    public partial class FrmRelClientes : Form
    {
        public FrmRelClientes()
        {
            InitializeComponent();
        }

        private void FrmRelClientes_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'cadastro1DataSet.ListarClientes'. Você pode movê-la ou removê-la conforme necessário.
            this.listarClientesTableAdapter.Fill(this.cadastro1DataSet.ListarClientes);

            this.reportViewer1.RefreshReport();
        }
    }
}
