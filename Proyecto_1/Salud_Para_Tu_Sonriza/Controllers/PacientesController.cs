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
        public IActionResult GuardarPacientes() {
            try
            {
                PacienteDTO _pacientesdto = new PacienteDTO();
                _pacientesdto.Nombre_Completo = Request.Form["txtNombre"].ToString();
                _pacientesdto.DPI = Convert.ToInt64(Request.Form["txtDPI"].ToString());
                _pacientesdto.Edad = Convert.ToInt32(Request.Form["txtEdad"].ToString());
                _pacientesdto.TelefonoContacto = Convert.ToInt64(Request.Form["txtTelefono"].ToString());
                _pacientesdto.Fecha_ultima_consulta = Convert.ToDateTime(Request.Form["dtpFechaUltima"].ToString());
                _pacientesdto.Fecha_proxima_consulta = Request.Form["dtpFechaProxima"].ToString()==string.Empty? null : Convert.ToDateTime(Request.Form["dtpFechaProxima"].ToString());
                string UltimoDiagnostico = Request.Form["txtultimoDiagnostico"].ToString();
                string Tratamiento = Request.Form["txtultimoDiagnostico"].ToString();
                if (UltimoDiagnostico != string.Empty || Tratamiento != UltimoDiagnostico)
                {
                    _pacientesdto.Descripciones_Tratamiento.Add(new DescripcionTratamientoDTO { Descripcion_Ultimo_Diagonisto = UltimoDiagnostico, Tratamiento_en_curso = Tratamiento });
                }
                ViewData["Mensaje"] = "El paciente con DPI" + _pacientesdto.DPI.ToString() + " Registrado exitosamente";
               (new GestorPacientes()).GuardarPacientes(_pacientesdto);
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
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

            if (data.Descripciones_Tratamiento.Find(x => x.Tratamiento_en_curso.ToLower().Contains("Ortodoncia".ToLower()) || x.Descripcion_Ultimo_Diagonisto.Contains("Ortodoncia".ToLower()) && x.Tratamiento_en_curso.ToLower().Contains("Caries".ToLower()) || x.Descripcion_Ultimo_Diagonisto.Contains("Caries".ToLower())) != null)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public IActionResult ListarPacientes(string Tipo_Lista) {
            try
            {
                GestorPacientes gestorPacientes = new GestorPacientes();
                List<PacienteDTO> LISTAPACIENTES = new List<PacienteDTO>();
                    LISTAPACIENTES= gestorPacientes.ListarPacientes();
                switch (Tipo_Lista) {
                    case "Lipieza Dental": {
                            var Filtro = from x in LISTAPACIENTES where (x.Descripciones_Tratamiento.Count == 0 && Math.Abs((DateTime.Now.Month - x.Fecha_ultima_consulta.Month) + 12 * (DateTime.Now.Year - x.Fecha_ultima_consulta.Year))>=6) select (x);
                            ViewData["ListaPacientes"] = Filtro.ToList();
                            break;
                        }
                    case "Seguimiento Ortodoncia":
                        {
                            var Filtro = from x in LISTAPACIENTES where (ContienePalabra("Ortodoncia",x) && Math.Abs((DateTime.Now.Month - x.Fecha_ultima_consulta.Month) + 12 * (DateTime.Now.Year - x.Fecha_ultima_consulta.Year)) >= 2) select (x);
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
                            var Filtro = from x in LISTAPACIENTES where (NoContienePalabra(x) && Math.Abs((DateTime.Now.Month - x.Fecha_ultima_consulta.Month) + 12 * (DateTime.Now.Year - x.Fecha_ultima_consulta.Year)) >= 6 && x.Descripciones_Tratamiento.Count>0) select (x);
                            ViewData["ListaPacientes"] = Filtro.ToList();
                            break;
                        }
                    default: {
                            ViewData["ListaPacientes"] = LISTAPACIENTES;
                            break;
                        }
                }
                
                return View();
            }
            catch (Exception ex)
            {

                throw(ex);
            }  
            
        }
    }
}

