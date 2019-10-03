using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tarea6_Azure.Utilidades;

namespace Tarea6_Azure.Registros
{
    public partial class RegistroSugerencias : System.Web.UI.Page
    {
        readonly string KeyViewState = "Segurencia";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState[KeyViewState] = new Sugerencia();
            }
        }
        private void Limpiar()
        {
            SugerenciaID.Text = 0.ToString();
            FechaTextBox.Text = DateTime.Now.ToString();
            DescripcionTextBox.Text = string.Empty;
            MostrarMensajes.Visible = false;
            MostrarMensajes.Text = string.Empty;
            ViewState[KeyViewState] = new Sugerencia();
        }

        private bool Validar()
        {
            bool paso = true;
            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
                paso = false;
            if (string.IsNullOrEmpty(FechaTextBox.Text))
                paso = false;
            if (GridView.Rows.Count <= 0)
                paso = false;
            return paso;
        }

        private Sugerencia LlenaClase()
        {
            Sugerencia sugerencia = new Sugerencia();
            DateTime.TryParse(FechaTextBox.Text, out DateTime result);
            sugerencia = (Sugerencia)ViewState[KeyViewState];
            sugerencia.SugerenciaID = SugerenciaID.Text.ToInt();
            sugerencia.Descripcion = DescripcionTextBox.Text;
            return sugerencia;
        }

        private void LlenaCampo(Sugerencia sugerencia)
        {
            SugerenciaID.Text = sugerencia.SugerenciaID.ToString();
            FechaTextBox.Text = sugerencia.Fecha.ToString();
            DescripcionTextBox.Text = sugerencia.Descripcion.ToString();            
            ViewState[KeyViewState] = sugerencia;
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Sugerencia> repositorio = new RepositorioBase<Sugerencia>();
            Sugerencia sugerencia = new Sugerencia();
            sugerencia = LlenaClase();
            bool paso = false;

            if (sugerencia.SugerenciaID <= 0)
            {
                sugerencia.Fecha = DateTime.Now;
                paso = repositorio.Guardar(sugerencia);
                if (paso)
                {
                    Limpiar();
                }
            }
            else
                paso = repositorio.Modificar(sugerencia);

            if (paso)
            {
                Limpiar();
                MostrarMensajes.Text = "Registro Guardado Correctamente";
                MostrarMensajes.CssClass = "alert-success";
                MostrarMensajes.Visible = true;
            }
            else
            {
                MostrarMensajes.Text = "No Se pudo Guardar el Registro";
                MostrarMensajes.CssClass = "alert-warning";
                MostrarMensajes.Visible = true;
            }

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            int id = Utils.ToInt(Request.QueryString["SugerenciaID"]);
            if (id > 0)
            {
                RepositorioBase<Sugerencia> repositorio = new RepositorioBase<Sugerencia>();
                var sugerencia = repositorio.Buscar(id);

                if (sugerencia == null)
                {
                    MostrarMensajes.Visible = true;
                    MostrarMensajes.Text = "Registro No encontrado";
                    MostrarMensajes.CssClass = "alert-danger";
                    return;
                }
                else
                {
                    Limpiar();
                    LlenaCampo(sugerencia);
                }
            }
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (string.IsNullOrEmpty(this.SugerenciaID.Text) || string.IsNullOrWhiteSpace(SugerenciaID.Text))
            {
                MostrarMensajes.Visible = true;
                MostrarMensajes.Text = "Registro No encontrado";
                MostrarMensajes.CssClass = "alert-danger";
                return;
            }
            id = Utils.ToInt(SugerenciaID.Text);
            RepositorioBase<Sugerencia> repositorio = new RepositorioBase<Sugerencia>();

            if (repositorio.Buscar(id) == null)
            {
                MostrarMensajes.Visible = true;
                MostrarMensajes.Text = "Registro No encontrado";
                MostrarMensajes.CssClass = "alert-danger";
                return;
            }
            bool eliminado = repositorio.Eliminar(id);
            if (eliminado)
            {
                Limpiar();
                MostrarMensajes.Visible = true;
                MostrarMensajes.Text = "Registro Eliminado Correctamente";
                MostrarMensajes.CssClass = "alert-danger";
            } 
        }
    }
}