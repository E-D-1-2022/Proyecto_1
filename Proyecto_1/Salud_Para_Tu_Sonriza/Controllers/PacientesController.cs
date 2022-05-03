using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogicaOdontologia.DTO;
using LogicaOdontologia.Session;
namespace Salud_Para_Tu_Sonriza.Controllers
{
    public class PacientesController : Controller
    {
        public IActionResult InsertarPacientes()
        {
            return View();
        }
        public IActionResult GuardarPacientes()
        {
            try
            {
                PacienteDTO _pacientesdto = new PacienteDTO();
                _pacientesdto.Nombre_Completo = Request.Form["txtNombre"].ToString();
                _pacientesdto.DPI = Convert.ToInt64(Request.Form["txtDPI"].ToString());
                _pacientesdto.Edad = Convert.ToInt32(Request.Form["txtEdad"].ToString());
                _pacientesdto.TelefonoContacto = Convert.ToInt64(Request.Form["txtTelefono"].ToString());
                _pacientesdto.Fecha_ultima_consulta = Convert.ToDateTime(Request.Form["dtpFechaUltima"].ToString());
                _pacientesdto.Fecha_proxima_consulta = Request.Form["dtpFechaProxima"].ToString() == string.Empty ? DateTime.MinValue.Date : Convert.ToDateTime(Request.Form["dtpFechaProxima"].ToString());
                string UltimoDiagnostico = Request.Form["txtultimoDiagnostico"].ToString();
                string Tratamiento = Request.Form["txtultimoDiagnostico"].ToString();
                if (UltimoDiagnostico != string.Empty || Tratamiento != UltimoDiagnostico)
                {
                    _pacientesdto.Descripciones_Tratamiento.Add(new DescripcionTratamientoDTO { Descripcion_Ultimo_Diagonisto = UltimoDiagnostico, Tratamiento_en_curso = Tratamiento });
                }
                if (_pacientesdto.Fecha_proxima_consulta.Value.Date != DateTime.MinValue.Date)
                {

                    List<PacienteDTO> Lista = (new GestorPacientes()).ListarPacientes();
                    var Element = from x in Lista where (x.Fecha_proxima_consulta == _pacientesdto.Fecha_proxima_consulta.Value.Date) select (x.Fecha_proxima_consulta);
                    if (Element.Count() < 8)
                    {
                        ViewData["Mensaje"] = "El paciente con DPI " + _pacientesdto.DPI.ToString() + " Registrado exitosamente";
                        (new GestorPacientes()).GuardarPacientes(_pacientesdto);
                    }
                    else
                    {
                        throw (new Exception("La fecha seleccionada ya tiene el limite de agendados"));
                     
                    }
                }
                ViewData["Mensaje"] = "El paciente con DPI " + _pacientesdto.DPI.ToString() + " Registrado exitosamente";
                (new GestorPacientes()).GuardarPacientes(_pacientesdto);

            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
                return View("../Home/Error");
            }
            return View();
        }
        public bool ContienePalabra(string Filtro, PacienteDTO data)
        {

            if (data.Descripciones_Tratamiento.Find(x => x.Tratamiento_en_curso.ToLower().Contains(Filtro.ToLower()) || x.Descripcion_Ultimo_Diagonisto.Contains(Filtro.ToLower())) != null)
            {
                return true;
            }
            return false;
        }
        public bool NoContienePalabra(PacienteDTO data)
        {

            if (data.Descripciones_Tratamiento.Find(x => x.Tratamiento_en_curso.ToLower().Contains("Ortodoncia".ToLower()) || x.Descripcion_Ultimo_Diagonisto.Contains("Ortodoncia".ToLower()) || x.Tratamiento_en_curso.ToLower().Contains("Caries".ToLower()) || x.Descripcion_Ultimo_Diagonisto.Contains("Caries".ToLower())) != null)
            {
                return true;
            }
            return false;
        }
        [HttpGet]
        public IActionResult ListarPacientes(string Tipo_Lista)
        {
            try
            {
                GestorPacientes gestorPacientes = new GestorPacientes();
                List<PacienteDTO> LISTAPACIENTES = new List<PacienteDTO>();
                LISTAPACIENTES = gestorPacientes.ListarPacientes();
                switch (Tipo_Lista)
                {
                    case "Lipieza Dental":
                        {
                            var Filtro = from x in LISTAPACIENTES where (x.Descripciones_Tratamiento.Count == 0 && Math.Abs((DateTime.Now.Month - x.Fecha_ultima_consulta.Month) + 12 * (DateTime.Now.Year - x.Fecha_ultima_consulta.Year)) >= 6) select (x);
                            ViewData["ListaPacientes"] = Filtro.ToList();
                            break;
                        }
                    case "Seguimiento Ortodoncia":
                        {
                            var Filtro = from x in LISTAPACIENTES where (ContienePalabra("Ortodoncia", x) && Math.Abs((DateTime.Now.Month - x.Fecha_ultima_consulta.Month) + 12 * (DateTime.Now.Year - x.Fecha_ultima_consulta.Year)) >= 2) select (x);
                            ViewData["ListaPacientes"] = Filtro.ToList();
                            break;
                        }
                    case "Lipieza Caries":
                        {
                            var Filtro = from x in LISTAPACIENTES where (ContienePalabra("Caries", x) && Math.Abs((DateTime.Now.Month - x.Fecha_ultima_consulta.Month) + 12 * (DateTime.Now.Year - x.Fecha_ultima_consulta.Year)) >= 4) select (x);
                            ViewData["ListaPacientes"] = Filtro.ToList();
                            break;
                        }
                    case "Tratamiento Especifico":
                        {
                            var Filtro = from x in LISTAPACIENTES where (!NoContienePalabra(x) && Math.Abs((DateTime.Now.Month - x.Fecha_ultima_consulta.Month) + 12 * (DateTime.Now.Year - x.Fecha_ultima_consulta.Year)) >= 6 && x.Descripciones_Tratamiento.Count > 0) select (x);
                            ViewData["ListaPacientes"] = Filtro.ToList();
                            break;
                        }
                    default:
                        {
                            ViewData["ListaPacientes"] = LISTAPACIENTES;
                            break;
                        }
                }

                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
                return View("../Home/Error");
            }

        }
        public IActionResult BuscarPaciente()
        {
            string Filtro = Request.Form["txtFiltro"].ToString();
            GestorPacientes gestorPacientes = new GestorPacientes();
            PacienteDTO Element = gestorPacientes.BuscarPaciente(Filtro);

            if (Element == null) { ViewData["Paciente"] = null; } else { ViewData["Paciente"] = Element; }

            return View();
        }
        public IActionResult ActualizarPaciente()
        {
            string TipoEnvio = Request.Form["TipoEnvio"].ToString();
            switch (TipoEnvio)
            {
                case "FormularioActualizacion":
                    {
                        GestorPacientes gestorPacientes = new GestorPacientes();
                        string DPI = (Request.Form["txtDPI"].ToString());
                        PacienteDTO pacienteDTO = gestorPacientes.BuscarPaciente(DPI);
                        ViewData["DataPaciente"] = pacienteDTO;
                        return View("./InsertarPacientes");
                        break;
                    }
                case "FormularioProceso":
                    {
                        GestorPacientes gestorPacientes = new GestorPacientes();
                        string DPI = (Request.Form["txtDPI"].ToString());
                        PacienteDTO pacienteDTO = gestorPacientes.BuscarPaciente(DPI);
                        DescripcionTratamientoDTO Descripcion = new DescripcionTratamientoDTO();
                        Descripcion.Descripcion_Ultimo_Diagonisto = Request.Form["txtDiagnostico"].ToString();
                        Descripcion.FechaDiagnostico = DateTime.Today;
                        Descripcion.Tratamiento_en_curso = Request.Form["txtTratamiento"].ToString();
                        pacienteDTO.Descripciones_Tratamiento.Add(Descripcion);
                        gestorPacientes.GuardarPacientes(pacienteDTO);
                        ViewData["Mensaje"] = "El paciente con DPI " + pacienteDTO.DPI.ToString() + " Actualizado exitosamente";
                        return View("./GuardarPacientes");
                        break;
                    }
                case "ReagendarCita":{
                        GestorPacientes gestorPaciente = new GestorPacientes();
                        string DPI = (Request.Form["txtDPI"].ToString());
                        PacienteDTO paciente = gestorPaciente.BuscarPaciente(DPI);
                        paciente.Fecha_proxima_consulta = Convert.ToDateTime(Request.Form["txtFechaNueva"].ToString()).Date;
                        if (gestorPaciente.CambiarFecha(paciente))
                        {

                            ViewData["Mensaje"] = "La fecha de la cita siguiente del paciente con DPI " + paciente.DPI.ToString() + "fue Actualizado exitosamente";
                            return View("./GuardarPacientes");
                        }
                        else {
                            ViewBag.Message = "El dia con la fecha seleccionada ya esta llena";
                            return View("../Home/Error");

                        }

                        break;
                    }
            
        }
            return View();


    }
        [HttpGet]
        public IActionResult EliminarPaciente(Int64 DPI)
    {
        try
        {
            GestorPacientes gestorPacientes = new GestorPacientes();
            gestorPacientes.EliminarPaciente(DPI);
            ViewData["Mensaje"] = "Paciente con el # de DPI" + DPI.ToString() + " fue eliminado exitosamente";
        }
        catch (Exception ex)
        {

            throw (ex);
        }
        return View("./GuardarPacientes");
    }
        public FileResult DescargarLog()
    {
        GestorPacientes gestorPacientes = new GestorPacientes();
        return File(gestorPacientes.DescargarLog(), "plain/text", "LogArbol.txt");
    }
}
}

