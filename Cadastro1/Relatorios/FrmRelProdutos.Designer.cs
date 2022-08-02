
namespace Cadastro1.Relatorios
{
    partial class FrmRelProdutos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cadastro1DataSet = new Cadastro1.Cadastro1DataSet();
            this.listarProdutosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listarProdutosTableAdapter = new Cadastro1.Cadastro1DataSetTableAdapters.ListarProdutosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.cadastro1DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listarProdutosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ListarProdutos";
            reportDataSource1.Value = this.listarProdutosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Cadastro1.Relatorios.RelProdutos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(757, 480);
            this.reportViewer1.TabIndex = 0;
            // 
            // cadastro1DataSet
            // 
            this.cadastro1DataSet.DataSetName = "Cadastro1DataSet";
            this.cadastro1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // listarProdutosBindingSource
            // 
            this.listarProdutosBindingSource.DataMember = "ListarProdutos";
            this.listarProdutosBindingSource.DataSource = this.cadastro1DataSet;
            // 
            // listarProdutosTableAdapter
            // 
            this.listarProdutosTableAdapter.ClearBeforeFill = true;
            // 
            // FrmRelProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 480);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmRelProdutos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório de Produtos";
            this.Load += new System.EventHandler(this.FrmRelProdutos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cadastro1DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listarProdutosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Cadastro1DataSet cadastro1DataSet;
        private System.Windows.Forms.BindingSource listarProdutosBindingSource;
        private Cadastro1DataSetTableAdapters.ListarProdutosTableAdapter listarProdutosTableAdapter;
    }
}