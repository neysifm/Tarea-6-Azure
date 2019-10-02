using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Sugerencia
    {
        [Key]
        public int SugerenciaId { get; set; }
        public DateTime Fecha { get; set; }
        public String Descripcion { get; set; }

        public Sugerencia(int sugerenciaId, DateTime fecha, string descripcion)
        {
            SugerenciaId = sugerenciaId;
            Fecha = fecha;
            Descripcion = descripcion;
        }

        public Sugerencia()
        {
            SugerenciaId = 0;
            Fecha = DateTime.Now;
            Descripcion = String.Empty;
        }
    }
}
