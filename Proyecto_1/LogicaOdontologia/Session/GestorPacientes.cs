using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaOdontologia.Entidad;
using LogicaOdontologia.DTO;
namespace LogicaOdontologia.Session
{
    public class GestorPacientes
    {
        Pacientes gestorPacientes;
        public List<PacienteDTO> ListarPacientes() {
            try
            {
                if (gestorPacientes == null) { gestorPacientes = new Pacientes(); }
                return gestorPacientes.ListarPacientes();
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }
        public void GuardarPacientes(PacienteDTO _PacientesDto) {
            try
            {
                if (gestorPacientes == null) { gestorPacientes = new Pacientes(); }
                gestorPacientes.InsertarPacientes(_PacientesDto);
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }
    }
}
