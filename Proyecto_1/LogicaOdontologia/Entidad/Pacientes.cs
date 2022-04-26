using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaOdontologia.DTO;    
namespace LogicaOdontologia.Entidad
{
    public class Pacientes
    {
        BaseDeDatos bd = BaseDeDatos.Instance;
        public void InsertarPacientes(PacienteDTO pacienteDTO) {
            try
            {
                BaseDeDatos.Instance.ArbolPacientes.Add(pacienteDTO);
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        
        }
        public void EliminarPacientes(PacienteDTO pacienteDTO) {
            try
            {
                BaseDeDatos.Instance.ArbolPacientes.Remove(pacienteDTO);
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }
        public PacienteDTO BuscarPaciente(PacienteDTO pacienteDTO) {
            try
            {
                return BaseDeDatos.Instance.ArbolPacientes.Find(pacienteDTO);
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }
        public List<PacienteDTO> ListarPacientes() {
            try
            {
                return bd.ArbolPacientes.InOrder();
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }
    }
}
