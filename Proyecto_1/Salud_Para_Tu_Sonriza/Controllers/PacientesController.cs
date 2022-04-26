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
                _pacientesdto.Fecha_proxima_consulta = Request.Form["dtpFechaProxima"].ToString()==string.Empty? DateTime.Now: Convert.ToDateTime(Request.Form["dtpFechaProxima"].ToString());
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
        public IActionResult ListarPacientes() {
            try
            {
                GestorPacientes gestorPacientes = new GestorPacientes();
                ViewData["ListaPacientes"] = gestorPacientes.ListarPacientes();
                return View();
            }
            catch (Exception ex)
            {

                throw(ex);
            }  
            
        }
    }
}
