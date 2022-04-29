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
        /// <summary>
        /// Base de datos
        /// </summary>
        BaseDeDatos bd = BaseDeDatos.Instance;
        public void InsertarPacientes(PacienteDTO pacienteDTO) {
            try
            {
                bd.ArbolPacientes.Add(pacienteDTO);
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        
        }
        public void EliminarPacientes(PacienteDTO pacienteDTO) {
            try
            {
                bd.ArbolPacientes.Remove(pacienteDTO);
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }
        public PacienteDTO BuscarPaciente(PacienteDTO pacienteDTO) {
            try
            {
                return bd.ArbolPacientes.Find(pacienteDTO);
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

        public byte[]DescargarArchivoLog() {
            return bd.DescargarLog();
        }
    }
}
