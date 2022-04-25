using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaOdontologia.DTO
{
   public class DescripcionTratamientoDTO
    {
        public string Descripcion_Ultimo_Diagonisto { get; set; }
        public DateTime FechaDiagnostico { get; set; }
        public string Tratamiento_en_curso { get; set; }

    }
}
