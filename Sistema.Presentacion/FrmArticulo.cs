using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema.Negocio;

namespace Sistema.Presentacion
{
    public partial class FrmArticulo : Form
    {

        private string rutaOrigen;
        private string rutaDestino;
        private string directorio = "C:\\Sistema\\";
        public FrmArticulo()
        {
            InitializeComponent();
        }


        public void Listar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Listar();
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total de registros: " + DgvListado.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        public void Buscar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Buscar(txtBuscar.Text);
                this.Formato();
                LblTotal.Text = "Total de registros: " + DgvListado.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        public void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[3].HeaderText = "Cateogoría";
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Código";
            DgvListado.Columns[5].Width = 150;
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Precio venta";
            DgvListado.Columns[7].Width = 60;
            DgvListado.Columns[8].Width = 200;
            DgvListado.Columns[8].HeaderText = "Descripción";
            DgvListado.Columns[9].Width = 100;
            DgvListado.Columns[10].Width = 100;
        }

        public void Limpiar()
        {
            txtDescripcion.Clear();
            txtNombre.Clear();
            txtId.Clear();
            txtBuscar.Clear();
            errorValidacion.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;

            DgvListado.Columns[0].Visible = false;
            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            btnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;
        }

        public void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CargarCategoria()
        {
            cbxCategoria.DataSource = NArticulo.Seleccionar();
            cbxCategoria.ValueMember = "idcategoria";
            cbxCategoria.DisplayMember = "nombre";
        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarCategoria();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (file.ShowDialog() == DialogResult.OK)
            {
                picImagen.Image = Image.FromFile(file.FileName);
                txtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
                this.rutaOrigen = file.FileName;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            panelCodigo.BackgroundImage = codigo.Encode(BarcodeLib.TYPE.CODE128, txtCodigo.Text,Color.Black,Color.White,400,100);
            btnGuardarCodigo.Enabled = true;
        }

        private void btnGuardarCodigo_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)panelCodigo.BackgroundImage.Clone();

            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.Filter = "Image PNG (*.png)|*.png";
            dialogoGuardar.FileName = txtCodigo.Text;
            dialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(dialogoGuardar.FileName))
            {
                imgFinal.Save(dialogoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }
    }
}
