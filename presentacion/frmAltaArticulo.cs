using datos;
using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        { 
            ArticuloDatos datos = new ArticuloDatos();

            try
            {
                if(articulo == null)
                {
                    articulo = new Articulo();
                }
                articulo.CodigoArticulo = tbxCodigo.Text;
                articulo.Nombre = tbxNombre.Text;
                articulo.Descripcion = tbxDescripcion.Text;
                articulo.Marca = (TipoMarca)cboMarca.SelectedItem;
                articulo.Categoria = (TipoCategoria)cboCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(tbxPrecio.Text);
                articulo.UrlImagen = tbxUrlImagen.Text;

                if(articulo.Id != 0)
                {
                    datos.modificar(articulo);
                    MessageBox.Show("Modificado exitosamenete");
                }
                else 
                {
                    datos.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }
                
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaDatos categoriadatos = new CategoriaDatos();
            MarcaDatos marcadatos = new MarcaDatos();
            try
            {
                cboCategoria.DataSource = categoriadatos.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descricpcion";
                cboMarca.DataSource = marcadatos.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    tbxCodigo.Text = articulo.CodigoArticulo;
                    tbxNombre.Text = articulo.Nombre;
                    tbxDescripcion.Text = articulo.Descripcion;
                    tbxUrlImagen.Text = articulo.UrlImagen;
                    cargarImagen(articulo.UrlImagen);
                    tbxPrecio.Text = articulo.Precio.ToString();
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    cboMarca.SelectedValue = articulo.Marca.Id;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void tbxUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(tbxUrlImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxArticulo.Load(url: "https://storage.googleapis.com/proudcity/mebanenc/uploads/2021/03/placeholder-image.png");
            }
        }
    }
}
