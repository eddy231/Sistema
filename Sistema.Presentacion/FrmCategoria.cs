using System;
using System.Windows.Forms;
using Sistema.Negocio;

namespace Sistema.Presentacion
{
    public partial class FrmCategoria : Form
    {

        private string nombre;
        public FrmCategoria()
        {
            InitializeComponent();
        }

        public void Listar()
        {
            try
            {
                DgvCategoria.DataSource = NCategoria.Listar();
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total de registros: " + DgvCategoria.Rows.Count;
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
                DgvCategoria.DataSource = NCategoria.Buscar(txtBuscar.Text);
                this.Formato();
                LblTotal.Text = "Total de registros: " + DgvCategoria.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        public void Formato()
        {
            DgvCategoria.Columns[0].Visible = false;
            DgvCategoria.Columns[1].Visible = false;
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

            DgvCategoria.Columns[0].Visible = false;
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

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            string respuesta = "";

            try
            {
                if(txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar algunos datos, estos serán remarcados");
                    errorValidacion.SetError(txtNombre, "Ingrese un nombre");
                }
                else
                {
                    respuesta = NCategoria.Insertar(txtNombre.Text, txtDescripcion.Text);
                    if(respuesta == "OK")
                    {
                        this.MensajeOk("Se inserto correctamente el registro");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(respuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            tabGeneral.SelectedIndex = 0;
        }

        private void DgvCategoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                btnInsertar.Visible = false;
                btnActualizar.Visible = true;
                txtId.Text = Convert.ToString(DgvCategoria.CurrentRow.Cells["ID"].Value);
                this.nombre = Convert.ToString(DgvCategoria.CurrentRow.Cells["Nombre"].Value);
                txtNombre.Text = Convert.ToString(DgvCategoria.CurrentRow.Cells["Nombre"].Value);
                txtDescripcion.Text = Convert.ToString(DgvCategoria.CurrentRow.Cells["Descripcion"].Value);
                tabGeneral.SelectedIndex = 1;
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione desde la celda nombre");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string respuesta = "";

            try
            {
                if (txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar algunos datos, estos serán remarcados");
                    errorValidacion.SetError(txtNombre, "Ingrese un nombre");
                }
                else
                {
                    respuesta = NCategoria.Actualizar(Convert.ToInt32(txtId.Text),this.nombre,txtNombre.Text, txtDescripcion.Text);
                    if (respuesta == "OK")
                    {
                        this.MensajeOk("Se actualizó correctamente el registro");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(respuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvCategoria.Columns[0].Visible = true;
                btnActivar.Visible = true;
                btnDesactivar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                DgvCategoria.Columns[0].Visible = false;
                btnActivar.Visible = false;
                btnDesactivar.Visible = false;
                btnEliminar.Visible = false;
            }
        }

        private void DgvCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvCategoria.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)DgvCategoria.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente deseas eliminar el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in DgvCategoria.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            rpta = NCategoria.Eliminar(codigo);

                            if (rpta == "OK")
                            {
                                this.MensajeOk("Se elimino el registro: " + row.Cells[2].Value.ToString());
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message  + ex.StackTrace);
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente deseas dar de baja el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in DgvCategoria.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            rpta = NCategoria.Desactivar(codigo);

                            if (rpta == "OK")
                            {
                                this.MensajeOk("Se dio de baja el registro: " + row.Cells[2].Value.ToString());
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente deseas dar de alta el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in DgvCategoria.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            rpta = NCategoria.Activar(codigo);

                            if (rpta == "OK")
                            {
                                this.MensajeOk("Se dio de alta el registro: " + row.Cells[2].Value.ToString());
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
