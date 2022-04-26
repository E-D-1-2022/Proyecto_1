using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaOdontologia.DTO
{
    public class PacienteDTO
    {
        public PacienteDTO() {
            this.Descripciones_Tratamiento = new List<DescripcionTratamientoDTO>();
        }
        public string Nombre_Completo { get; set; }

        public Int64 DPI { get; set; }

        public int Edad { get; set; }
        public Int64 TelefonoContacto { get; set; }
        public DateTime Fecha_ultima_consulta { get; set; }
        public DateTime Fecha_proxima_consulta { get; set; }
        public List<DescripcionTratamientoDTO> Descripciones_Tratamiento { get; set; }
    }
}
