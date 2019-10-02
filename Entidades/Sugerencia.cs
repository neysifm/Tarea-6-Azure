using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Sugerencia
    {
        [Key]
        public int SugerenciaID { get; set; }
        public DateTime Fecha { get; set; }
        public String Descripcion { get; set; }

        public Sugerencia(int sugerenciaId, DateTime fecha, string descripcion)
        {
            SugerenciaID = sugerenciaId;
            Fecha = fecha;
            Descripcion = descripcion;
        }

        public Sugerencia()
        {
            SugerenciaID = 0;
            Fecha = DateTime.Now;
            Descripcion = String.Empty;
        }
    }
}
