using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinesEntities;
using BL;
using DAL;

namespace ProyectoFinal.Controllers
{
    public class PersonaController : Controller
    {
        SitcomEntities db = new SitcomEntities();
        DomicilioManager dm = new DomicilioManager();
        UsuariosManager um = new UsuariosManager();
        DomicilioEntity domicilioEN = new DomicilioEntity();
        UsuarioEntity usuarioActual = new UsuarioEntity();
        PersonasManager pm = new PersonasManager();
        PersonaEntity per;

        public ActionResult DatosPersonales(string returnUrl, int? idNegocio, string vienePor)
        {
                per = new PersonaEntity();
                ViewBag.Sexos = new SelectList(db.Sexo, "idSexo", "nombre");
                ViewBag.TiposDocumento = new SelectList(db.TipoDocumento, "idTipoDocumento", "nombre");
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.idNegocio = idNegocio;

               // string msj = mensaje == null ? "" : mensaje.ToString();
               // ViewBag.Mensaje = msj;

                per.idNegocio = idNegocio;

                per.Domicilio = new DomicilioEntity()
                {
                    listPaises = dm.GetAllPaises(),
                    listProvincias = new List<Provincia>(),
                    listLocalidades = new List<Localidad>()
                };

                if (vienePor != null && vienePor =="checkin")
                {
                    Session["LayoutCheckIn"] = "_LayoutPopUp.cshtml";
                    return PartialView(per);
                }
                else
                {
                    Session["LayoutCheckIn"] = "_Layout.cshtml";
                    return View(per);
                }

                
        }
        [HttpPost]
        public ActionResult DatosPersonales(PersonaEntity per, string returnUrl, int? idNegocio)
        {

            ObtenerUsuarioActual();

            per.Usuarios.Add(usuarioActual);

            if (ModelState.IsValid)
            {                
                int idPersona = pm.AddPersona(per);

                usuarioActual.idPersona = idPersona;
                Session["User"] = usuarioActual;

                if (returnUrl != null && idNegocio != null)
                    returnUrl = returnUrl + idNegocio.ToString();


                if (returnUrl==null)
                    return RedirectToAction("Index","Mensajes", new { codigo = "NEG-ALT-200"});
                else
                     return RedirectToAction(returnUrl);
            }
            else 
            {
                if (idNegocio != null)
                    returnUrl = returnUrl + idNegocio.ToString();

                ViewBag.ReturnUrl = returnUrl;
                ViewBag.IdNegocio = idNegocio;
                ViewBag.Sexos = new SelectList(db.Sexo, "idSexo", "nombre");
                ViewBag.TiposDocumento = new SelectList(db.TipoDocumento, "idTipoDocumento", "nombre");

                per.idNegocio = idNegocio;

                per.Domicilio = new DomicilioEntity()
                {
                    listPaises = dm.GetAllPaises(),
                    listProvincias = new List<Provincia>(),
                    listLocalidades = new List<Localidad>()
                };

                return View("DatosPersonales",per);
            }          
           
        }
        public ActionResult PaisSeleccionado([Bind(Include = "nombre,apellido,idTipoDocumento,documento,idSexo,email,idNegocio")] PersonaEntity persona, int? idPaisSeleccionado, string returnUrl)
        {
            
            ViewBag.Sexos = new SelectList(db.Sexo, "idSexo", "nombre", persona.idSexo);
            ViewBag.TiposDocumento = new SelectList(db.TipoDocumento, "idTipoDocumento", "nombre", persona.idTipoDocumento);
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.IdNegocio = persona.idNegocio;

            persona.Domicilio = new DomicilioEntity()
            {
                listPaises = dm.GetAllPaises(),
                listProvincias = new List<Provincia>(),
                listLocalidades = new List<Localidad>()
            };

            if (idPaisSeleccionado != null)
            {
                persona.Domicilio.paisSeleccionado = (int)idPaisSeleccionado;
                persona.Domicilio.listProvincias = dm.getProvinciaPaisSeleccionado(idPaisSeleccionado);
                persona.Domicilio.listLocalidades = new List<Localidad>();
            }

            ModelState.Clear();

            return View("DatosPersonales",persona);
        }
        public ActionResult ProvinciaSeleccionada([Bind(Include = "nombre,apellido,idTipoDocumento,documento,idSexo,email,idNegocio")] PersonaEntity persona, int? idPaisSeleccionado, int? idProvinciaSeleccionada, string returnUrl)
        {
            ViewBag.Sexos = new SelectList(db.Sexo, "idSexo", "nombre", persona.idSexo);
            ViewBag.TiposDocumento = new SelectList(db.TipoDocumento, "idTipoDocumento", "nombre", persona.idTipoDocumento);
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.IdNegocio = persona.idNegocio;
            
            if(idProvinciaSeleccionada != null)
            { 
                persona.Domicilio = new DomicilioEntity()
                {
                    listPaises = dm.GetAllPaises(),
                    listProvincias = dm.getProvinciaPaisSeleccionado(idPaisSeleccionado),
                    listLocalidades = dm.getLocalidadByProvinciaSeleccionada(idProvinciaSeleccionada),
                    paisSeleccionado = (int)idPaisSeleccionado,
                    provinciaSeleccionada = (int)idProvinciaSeleccionada
                };
            }
            else
            {
                persona.Domicilio = new DomicilioEntity()
                {
                    listPaises = dm.GetAllPaises(),
                    listProvincias = new List<Provincia>(),
                    listLocalidades = new List<Localidad>(),
                    paisSeleccionado = (int)idPaisSeleccionado
                };
            }

            ModelState.Clear();
            return View("DatosPersonales", persona);
        }
        public bool ValidarPermisoVista(string controlador, string vista) //METODO UNICO DEL CONTROLADOR PARA VALIDAR PERMISO DE LA VISTA (LLAMA AL MANEJADOR).
        {
            ObtenerUsuarioActual();
            return um.ValidarPermisoVista(usuarioActual, controlador, vista);
        }
        public void ObtenerUsuarioActual()
        {
            usuarioActual = (UsuarioEntity)Session["User"];
        }
	}
}