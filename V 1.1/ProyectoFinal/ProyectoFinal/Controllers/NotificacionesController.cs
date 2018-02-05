using BL;
using BussinesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class NotificacionesController : Controller
    {

        public UsuarioEntity usuarioActual;
        // GET: Notificaciones
        public ActionResult BuscaNotificacion()
        {
            NotificacionesManager nm = new NotificacionesManager();
            ObtenerUsuarioActual();

            if (usuarioActual.idUsuario != 0)
            {
                //Retornar lo del SP en nl pasando los parametros que corresponda. seguramente dependera del perfil.
                List<NotificacionesEntity> nl = new List<NotificacionesEntity>();

                nl = nm.consultarNotificaciones(usuarioActual.idUsuario, usuarioActual.idPerfil);

                ////Iterar el listado que viene desde la DB.
                //for (int i = 0; i < 3; i++)
                //{
                //    NotificacionesEntity n1 = new NotificacionesEntity();
                //    n1.Descripcion = "Solicitudes";
                //    n1.Cantidad = 20;
                //    n1.Url = Url.Content("~/Reservas/ReservasUsuario");
                //    nl.Add(n1);
                //}
      
                return Json(nl, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Usuarios");
            }
            
        }

        public void ObtenerUsuarioActual()
        {
            usuarioActual = (UsuarioEntity)Session["User"];
        }
    }
}