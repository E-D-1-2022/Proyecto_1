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
        public void EliminarPaciente(Int64 DPI) {
            if (gestorPacientes == null) { gestorPacientes = new Pacientes(); }
            try
            {
                gestorPacientes.EliminarPacientes(new PacienteDTO { DPI = DPI });

            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }
        public PacienteDTO BuscarPaciente(string Filtro) {
            if (gestorPacientes == null) { gestorPacientes = new Pacientes(); }
            try
            {
                Int64 DPI = Int64.Parse(Filtro);
                PacienteDTO Result = gestorPacientes.BuscarPaciente(new PacienteDTO { DPI = DPI });
                return Result;
            }
            catch(Exception ex)
            {
                List<PacienteDTO> Lista = gestorPacientes.ListarPacientes();
                var Element = from x in Lista where (x.Nombre_Completo == Filtro) select (x);
                if (Element.Count() > 0)
                {
                    return Element.ToList()[0];
                }
                else {
                    return new PacienteDTO();
                }
            }
        }
        public bool CambiarFecha(PacienteDTO pacienteDTO) {
                if(gestorPacientes == null) { gestorPacientes = new Pacientes(); }
            try
            {
                List<PacienteDTO> Lista = ListarPacientes();

                var element = from x in Lista where (x.Fecha_proxima_consulta.Value == pacienteDTO.Fecha_proxima_consulta) select (x);
                if (element.Count() < 8) {
                    GuardarPacientes(pacienteDTO);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        
        }
        public byte[] DescargarLog() {
            if (gestorPacientes == null) { gestorPacientes = new Pacientes(); }
            try
            {
                return gestorPacientes.DescargarArchivoLog();
            }
            catch (Exception ex)
            {

                throw(ex);
            }
        }
    }
}
