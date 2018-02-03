﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using BussinesEntities;
using DAL;
using System.IO;

namespace ProyectoFinal.Controllers
{
    public class NegociosController : Controller
    {
        private SitcomEntities db = new SitcomEntities();
        private UsuarioEntity usuarioActual;
        private NegociosManager nm = new NegociosManager();
        private HotelManager hm = new HotelManager();
        private UsuariosManager um = new UsuariosManager();
        private DomicilioManager dm = new DomicilioManager();
        private NegocioEntity neg = new NegocioEntity();
        private DomicilioEntity dom = new DomicilioEntity();
        private PromocionesManager pm = new PromocionesManager();
        private PersonasManager perm = new PersonasManager();
		private TramitesManager tm = new TramitesManager();
        public List<ReservasEntities> resultadoModelo;
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConsultarReserva()
        {
            DateTime? fecha_desde = null;
            DateTime? fecha_hasta = null;
            int? cantidad_personas = null;
            int? cantidad_habitaciones = null;
            string tipo_hospedaje = null;

            Session["fecha_desde"] = Request["fecha_desde"];
            Session["fecha_hasta"] = Request["fecha_hasta"];
            Session["cantidad_personas"] = Request["cantidad_personas"];
            Session["cantidad_habitaciones"] = Request["cantidad_habitaciones"];
            Session["tipo_hospedaje"] = Request["tipo_hospedaje"];

            if (Request["fecha_desde"] != "")
            {
                fecha_desde = Convert.ToDateTime(Request["fecha_desde"]);
            };

            if (Request["fecha_hasta"] != "")
            {
                fecha_hasta = Convert.ToDateTime(Request["fecha_hasta"]);
            };

            if (Request["cantidad_personas"] != "")
            {
                cantidad_personas = Convert.ToInt32(Request["cantidad_personas"]);
            };

            if (Request["cantidad_habitaciones"] != "")
            {
                cantidad_habitaciones = Convert.ToInt32(Request["cantidad_habitaciones"]);
            };

            if (Request["tipo_hospedaje"] != "")
            {
                tipo_hospedaje = Convert.ToString(Request["tipo_hospedaje"]);
            };

            Session["post"] = "si";

            var resultadoModelo = hm.disponiblidad(fecha_desde, fecha_hasta, cantidad_personas, cantidad_habitaciones, tipo_hospedaje);

            return View("IndexHospedajes", resultadoModelo);
          
        }

        public ActionResult Nuevo() //PANTALLA CREAR NUEVO - NEGOCIOS
        {
            ObtenerUsuarioActual();
            if(usuarioActual.idPersona != null)
            {
                /*if (ValidarPermisoVista("Negocios", "Nuevo"))
                {*/
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre");
                ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro");


                NegocioEntity neg = new NegocioEntity();//para tener un idNegocio=0 y no falle al llamar a la accion NuevoComercio.

                dom.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
                SucursalEntity suc = new SucursalEntity()
                {
                    Domicilio = dom
                };
                neg.Sucursal.Add(suc);

                return View(neg);
            }
            else
            {
                return RedirectToAction("DatosPersonales", "Persona", new { returnUrl = "../Negocios/Nuevo" });
            }
            /*}
            else
                return RedirectToAction("ErrorPermisos", "Errores");*/
        }
        public ActionResult NuevoComercio(NegocioEntity negocio,
                                          [Bind(Include = "idRubro" )] ComercioEntity comercio,
                                          [Bind(Include = "localidadSeleccionada,barrio,calle,dpto,numero")] DomicilioEntity domEn,
                                          string telefono,
                                          HttpPostedFileBase imagenPrinc,
                                          HttpPostedFileBase imagenMuestra1,
                                          HttpPostedFileBase imagenMuestra2,
                                          HttpPostedFileBase imagenMuestra3,
                                          HttpPostedFileBase imagenMuestra4,
                                          HttpPostedFileBase imagenMuestra5,
                                          HttpPostedFileBase imagenMuestra6)
        {
            ObtenerUsuarioActual();

            neg.idUsuario = usuarioActual.idUsuario;
            neg.idTipoNegocio = 2;
            neg.nombre = negocio.nombre;
            neg.descripcion = negocio.descripcion;
            neg.Comercio = new List<ComercioEntity>() { comercio };

            domEn.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
            
            neg.Sucursal.Add(new SucursalEntity()
            {
                esPrincipal = true,
                telefono = telefono,
                Domicilio = domEn
            });

            if(imagenPrinc == null)
            {
                ModelState.AddModelError("", "Debés seleccionar una imagen principal.");
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro", neg.Comercio.FirstOrDefault().idRubro);
                ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                return View("Nuevo", neg);
            }

            byte[] buffer = null;
            using (var binaryReader = new BinaryReader(imagenPrinc.InputStream))
            {
                buffer = binaryReader.ReadBytes(imagenPrinc.ContentLength);
            }

            FotosNegocio fotoPrinc = new FotosNegocio()
            {
                foto = buffer,
                esPrincipal = true
            };

            List<HttpPostedFileBase> listaHttp = new List<HttpPostedFileBase>() { imagenMuestra1, imagenMuestra2, imagenMuestra3, imagenMuestra4, imagenMuestra5, imagenMuestra6 };

            List<FotosNegocio> fotosNegocio = convertirImagenesMuestra(listaHttp);
            fotosNegocio.Add(fotoPrinc);

            neg.FotosNegocio = fotosNegocio;

            if (ModelState.IsValid)
            {
                if (nm.ValidarExisteNegocio(neg.nombre,null))
                    nm.AddNegocio(neg,usuarioActual);
                else
                {
                    ModelState.AddModelError("", "Un comercio con el mismo nombre ya está registrado. Por favor elige otro.");
                    ViewBag.Perfil = usuarioActual.idPerfil;
                    ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro", neg.Comercio.FirstOrDefault().idRubro);
                    ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                    return View("Nuevo", neg);
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult SolicitarBajaNegocio(int idNegocio)
        {
            ObtenerUsuarioActual();
            NegocioEntity neg = nm.GetNegocioById(idNegocio);

            Persona p = perm.GetPersonaByIdUsuario(usuarioActual.idUsuario);

            ViewBag.Persona = p;

            return View(neg);
           
        }
        public ActionResult SolicitarRepublicacion(int idNegocio)
        {
            ObtenerUsuarioActual();
            NegocioEntity neg = nm.GetNegocioById(idNegocio);

            Persona p = perm.GetPersonaByIdUsuario(usuarioActual.idUsuario);

            ViewBag.Persona = p;

            return View(neg);

        }
        public ActionResult VerBajaNegocio(int id, int idTramite)
        {
            ObtenerUsuarioActual();
            NegocioEntity neg = nm.GetNegocioById(id);

            Persona p = perm.GetPersonaByIdUsuario((int)neg.idUsuario);

            TramiteEntity te = tm.GetTramiteById(idTramite);

            ViewBag.Persona = p;
            ViewBag.IdTramite = idTramite;
            ViewBag.Motivo = te.comentario;

            return View(neg);
        }
        public ActionResult VerRepublicacionNegocio(int id, int idTramite)
        {
            ObtenerUsuarioActual();
            NegocioEntity neg = nm.GetNegocioById(id);

            Persona p = perm.GetPersonaByIdUsuario((int)neg.idUsuario);

            ViewBag.Persona = p;
            ViewBag.IdTramite = idTramite;

            return View(neg);
        }
        public ActionResult BajaNegocio(int idNegocio, string motivoBaja, bool aceptaCondiciones)
        {
            ObtenerUsuarioActual();

            nm.BajaNegocio(idNegocio, usuarioActual,motivoBaja);

            return RedirectToAction("NegociosUsuario");
        }
        public ActionResult RepublicarNegocio(int idNegocio)
        {
            ObtenerUsuarioActual();

            nm.RepublicarNegocio(idNegocio, usuarioActual);

            return RedirectToAction("NegociosUsuario");
        }
        public ActionResult NuevoLugarHospedaje([Bind(Include = "nombre,descripcion")] NegocioEntity negocio,
                                                [Bind(Include = "localidadSeleccionada,barrio,calle,dpto,numero")] DomicilioEntity domEn,
                                           HttpPostedFileBase imagenPrinc,
                                           HttpPostedFileBase imagenMuestra1,
                                           HttpPostedFileBase imagenMuestra2,
                                           HttpPostedFileBase imagenMuestra3,
                                           HttpPostedFileBase imagenMuestra4,
                                           HttpPostedFileBase imagenMuestra5,
                                           HttpPostedFileBase imagenMuestra6,
                                           string telefono,
                                           int TipoHospedaje)
        {
            // PARTE NEGOCIO //
            ObtenerUsuarioActual();

            neg.idUsuario = usuarioActual.idUsuario;
            neg.idTipoNegocio = 1;
            neg.nombre = negocio.nombre;
            neg.descripcion = negocio.descripcion;
            neg.Sucursal.Add(new SucursalEntity()
            {
                esPrincipal = true,
                telefono = telefono,
                Domicilio = domEn
            });

            neg.Sucursal.FirstOrDefault().Domicilio.listLocalidadesCercanas = dm.GetLocalidadesCercanas();

            if (imagenPrinc == null)
            {
                ModelState.AddModelError("", "Debés seleccionar una imagen principal.");
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);

                ViewBag.Carac = nm.GetCaracteristicas();
                ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
                ViewBag.TiposComplejo = new SelectList(db.TipoComplejo, "idTipoComplejo", "nombreTipoComplejo");

                List<TipoHabitacion> habs = nm.GetTiposHabitacion();
                List<HabitacionesEntity> habitaciones = new List<HabitacionesEntity>();
                foreach (var item in habs)
                {
                    habitaciones.Add(new HabitacionesEntity()
                    {
                        idTipoHabitacion = item.idTipoHabitacion,
                        nombre = item.nombre
                    });
                }
                ViewBag.Habitaciones = habitaciones;

                switch (TipoHospedaje)
                {
                    case 1: return View("NuevoCasaODpto", neg);

                    case 2: return View("NuevoComplejo", neg);

                    case 3: return View("NuevoHotel", neg);

                    default: break;
                }
            }
            // FIN PARTE NEGOCIO //

            // PARTE IMAGENES //
            byte[] buffer = null;
            using (var binaryReader = new BinaryReader(imagenPrinc.InputStream))
            {
                buffer = binaryReader.ReadBytes(imagenPrinc.ContentLength);
            }

            FotosNegocio fotoPrinc = new FotosNegocio()
            {
                foto = buffer,
                esPrincipal = true
            };

            List<HttpPostedFileBase> listaHttp = new List<HttpPostedFileBase>() { imagenMuestra1, imagenMuestra2, imagenMuestra3, imagenMuestra4, imagenMuestra5, imagenMuestra6 };

            List<FotosNegocio> fotosNegocio = convertirImagenesMuestra(listaHttp);
            fotosNegocio.Add(fotoPrinc);

            neg.FotosNegocio = fotosNegocio;

            // FIN PARTE IMAGENES //

            // PARTE LUGAR HOSPEDAJE //
            LugarHospedajeEntity lug = new LugarHospedajeEntity();
            lug.idTipoLugarHospedaje = TipoHospedaje;
            lug.CaracteristicasHospedaje = new List<CaracteristicasHospedajeEntity>();
            neg.LugarHospedaje = new List<LugarHospedajeEntity>() { lug };

            SelectList carac = new SelectList(db.Caracteristica, "idCaracteristica", "nombre");
            foreach (var item in carac)
            {
                string s = Request.Form[item.Text];
                bool selected = false;

                if (s != "false" && s != null)
                    selected = true;

                if (selected)
                {
                    CaracteristicasHospedajeEntity c = new CaracteristicasHospedajeEntity()
                    {
                       idCaracteristica = int.Parse(item.Value)                            
                    };
                    neg.LugarHospedaje.FirstOrDefault().CaracteristicasHospedaje.Add(c);
                }
            }
                      

            switch (neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje)
            {
                case 1: List<CasaDptoOCabana> casaDptosOCabanas = new List<CasaDptoOCabana>() { new CasaDptoOCabana() };
                        neg.LugarHospedaje.FirstOrDefault().CasaDptoOCabana = casaDptosOCabanas;
                    break;

                case 2: List<ComplejoEntity> complejos = new List<ComplejoEntity>() { new ComplejoEntity() };
                        neg.LugarHospedaje.FirstOrDefault().Complejo = complejos;
                        neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idCategoria = int.Parse(Request.Form["categ"].ToString());
                        neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idTipoComplejo = int.Parse(Request.Form["tipos"].ToString());

                        int j = 0;
                        int capacidad;
                        int cantidad;
                        string idCapacidad = "Capacidad_" + j;
                        string idCantidad = "Cantidad_" + j;
                        int k = 1; //Para los nombres de las Casas/Dptos/Cabañas

                        List<CasaDptoOCabana> casasDptosOCabanas = new List<CasaDptoOCabana>();
                    
                        while(Request.Form[idCapacidad] != null && Request.Form[idCantidad] != null)
                        {
                            capacidad = int.Parse(Request.Form[idCapacidad]);
                            cantidad = int.Parse(Request.Form[idCantidad]);

                            for (int i = 0; i < cantidad; i++)
                            {
                                casasDptosOCabanas.Add(new CasaDptoOCabana()
                                    {
                                        nombreCasaDptoOCabana = "Alojamiento " + k,
                                        capacidadMaxima = capacidad
                                    });
                                k++;
                            }
                            j++;
                            idCapacidad = "Capacidad_" + j;
                            idCantidad = "Cantidad_" + j;
                        }

                        neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().CasaDptoOCabana = casasDptosOCabanas;
                        
                    break;
                case 3: List<HotelEntity> hoteles = new List<HotelEntity>() { new HotelEntity() };
                        neg.LugarHospedaje.FirstOrDefault().Hotel = hoteles;
                        neg.LugarHospedaje.FirstOrDefault().Hotel.FirstOrDefault().idCategoria = int.Parse(Request.Form["categ"].ToString());

                        SelectList tiposHabitacion = new SelectList(db.TipoHabitacion, "idTipoHabitacion", "nombre");
                        List<Habitacion> habitaciones = new List<Habitacion>();
                        int m = 1; //Para los nombres de las habitaciones

                         foreach (var item in tiposHabitacion)
                         {
                             if(Request.Form[item.Text] != "")
                             {
                                 int num = int.Parse(Request.Form[item.Text]);
                                 for (int i = 0; i < num; i++)
                                 {
                                habitaciones.Add(new Habitacion()
                                {
                                    nombreHabitacion = "Habitación " + m,
                                    idTipoHabitacion = int.Parse(item.Value)
                                });
                                m++;
                                 }
                             }
                         }

                         neg.LugarHospedaje.FirstOrDefault().Hotel.FirstOrDefault().Habitacion = habitaciones;
                        break;

                default:
                    break;
            }

            // FIN PARTE LUGAR HOSPEDAJE //

            // PARTE INSERCIÓN NEGOCIO //
            if (ModelState.IsValid)
            {
                if (nm.ValidarExisteNegocio(neg.nombre,null))
                    nm.AddNegocio(neg, usuarioActual);
                else
                {
                    ModelState.AddModelError("", "Un comercio con el mismo nombre ya está registrado. Por favor elige otro.");

                    ViewBag.Perfil = usuarioActual.idPerfil;
                    ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro");
                    ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                    ViewBag.CaracHotel = new SelectList(db.Caracteristica, "idCaracteristica", "nombre");
                    ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");

                    return View("Nuevo", neg);
                }
            }
            else 
            {
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro");
                ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                ViewBag.CaracHotel = new SelectList(db.Caracteristica, "idCaracteristica", "nombre");
                ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
			    ViewBag.TiposComplejo = new SelectList(db.TipoComplejo, "idTipoComplejo", "nombreTipoComplejo");
                ViewBag.Carac = nm.GetCaracteristicas();
                List<TipoHabitacion> habs = nm.GetTiposHabitacion();
                List<HabitacionesEntity> habitaciones = new List<HabitacionesEntity>();
                foreach (var item in habs)
                {
                    habitaciones.Add(new HabitacionesEntity()
                    {
                        idTipoHabitacion = item.idTipoHabitacion,
                        nombre = item.nombre
                    });
                }
                ViewBag.Habitaciones = habitaciones;
		

                switch (neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje)
	            {
                    case 1: return View("NuevoCasaODpto", neg);                       

                    case 2: return View("NuevoComplejo", neg);

                    case 3: return View("NuevoHotel", neg);

                    default: break;
	            }
                
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult IndexComercios()
        {
            var result = nm.GetAllNegocios(2);
            return View(result);
        }

        public ActionResult IndexHospedajes()
        {

            Session["fecha_desde"] = null;
            Session["fecha_hasta"] = null;
            Session["cantidad_personas"] = null;
            Session["cantidad_habitaciones"] = null;
            Session["tipo_hospedaje"] = null;

            Session["post"] = null;

            var result = hm.disponiblidad(null, null, null, null, null);
            return View(result);

            
        }
     
   public string ConsultarDisponiblidad(string fecha_desde, string fecha_hasta, int cantidad_personas, int cantidad_habitaciones, int idNegocio)
        {
            //prohibido CODIGO CACA!!

            Session["fecha_desde"] = fecha_desde;
            Session["fecha_hasta"] = fecha_hasta;
            Session["cantidad_personas"] = cantidad_personas;
            Session["cantidad_habitaciones"] = cantidad_habitaciones;

            var result = hm.consultarDisponibilidadPorNegocio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), cantidad_personas, cantidad_habitaciones, idNegocio);

            return result;
        }

        [HttpPost]
        public ActionResult IndexHospedajes(List<ReservasEntities> modelo)
        {

            return View(modelo);
 
        }



        public ActionResult ObtenerImagen(int id)
        {
            var img = nm.GetFotoNegocioById(id);
            if(img != null)
            {
                byte[] buffer = img.foto;
                return File(buffer, "image/jpg", string.Format("{0}.jpg", id));
            }
            return null;
        }
        public ActionResult VerNegocio(int id)
        {
            NegocioEntity neg = nm.GetNegocioById(id);

            return View(neg);
        }
        public ActionResult EvalNegocio(int? id, int idTramite)
        {
            if (id != null)
            {
                NegocioEntity neg = nm.GetNegocioById((int)id);
                ViewBag.Tramite = idTramite;

                return View(neg);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult NegociosUsuario()
        {
            ObtenerUsuarioActual();
            List<Negocio> negs = nm.GetNegocioByUsuario(usuarioActual.idUsuario);

            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.idPerfil = usuarioActual.idPerfil;
            ViewBag.UsuarioActual = usuarioActual;

            return View(negs);
        }

 


        public ActionResult VerHospedaje(int? id)
        {
            ObtenerUsuarioActual();
            NegocioEntity neg = nm.GetNegocioById((int)id);
            if (Session["post"] != "si")
            {

                Session["fecha_desde"] = null;
                Session["fecha_hasta"] = null;
                Session["cantidad_personas"] = null;
                Session["cantidad_habitaciones"] = null;
                Session["tipo_hospedaje"] = null;


            }

            ViewBag.Perfiles = new SelectList(db.Perfiles, "idPerfil", "nombre", usuarioActual.idPerfil);
            ViewBag.Perfil = usuarioActual.idPerfil;

            neg.idPerfil = usuarioActual.idPerfil;

            string address = neg.Sucursal.FirstOrDefault().Domicilio.calle + "+" + neg.Sucursal.FirstOrDefault().Domicilio.numero + ",+" + neg.Sucursal.FirstOrDefault().Domicilio.Localidad.nombreLocalidad + ",+Cordoba,+Argentina";

            int cont = 0;
            string calle = "";
            String[] parts_calle = neg.Sucursal.FirstOrDefault().Domicilio.calle.Split(' ');
            foreach (var item in parts_calle)
            {
                if (cont == 0)
                    calle = item;
                else
                    calle = calle + "+" + item;

                cont++;
            }
            calle = calle + "+" + neg.Sucursal.FirstOrDefault().Domicilio.numero + ",+";

            cont = 0;
            string loc = "";
            String[] parts_localidad = neg.Sucursal.FirstOrDefault().Domicilio.Localidad.nombreLocalidad.Split(' ');
            foreach (var item in parts_localidad)
            {
                if (cont == 0)
                    loc = item;
                else
                    loc = loc + "+" + item;

                cont++;
            }

            address = "https://maps.googleapis.com/maps/api/geocode/json?address=" + calle + loc + ",+Cordoba,+Argentina";

            ViewBag.Address = address;
            ViewBag.NombreNegocio = neg.nombre;
            ViewBag.AddressShow = neg.Sucursal.FirstOrDefault().Domicilio.calle + " " + neg.Sucursal.FirstOrDefault().Domicilio.numero + ", " + neg.Sucursal.FirstOrDefault().Domicilio.Localidad.nombreLocalidad;


            var tieneTramiteMGR = db.Tramite.Where(t => t.idNegocio == neg.idNegocio && (t.idTipoTramite == 2 && t.idEstadoTramite == 1 || t.idEstadoTramite == 2)).FirstOrDefault();

            var tieneMGR = neg.LugarHospedaje.FirstOrDefault().moduloReservas;

            ViewBag.TieneMGR = tieneMGR;

            if (tieneTramiteMGR != null)
                ViewBag.TieneTramiteMGR = 1;
            else
                ViewBag.TieneTramiteMGR = 0;

                if (usuarioActual.idUsuario == neg.idUsuario)
                    ViewBag.EsDueno = 1;
                else
                    ViewBag.EsDueno = 0;

            return View(neg);
        }
        public ActionResult VerComercio(int? id)
        {
			ObtenerUsuarioActual();	
            NegocioEntity neg = nm.GetNegocioById((int)id);

            string address = neg.Sucursal.FirstOrDefault().Domicilio.calle + "+" + neg.Sucursal.FirstOrDefault().Domicilio.numero + ",+" + neg.Sucursal.FirstOrDefault().Domicilio.Localidad.nombreLocalidad + ",+Cordoba,+Argentina";

            int cont = 0;
            string calle = "";
            String[] parts_calle = neg.Sucursal.FirstOrDefault().Domicilio.calle.Split(' ');
            foreach (var item in parts_calle)
            {
                if (cont == 0)
                    calle = item;
                else
                    calle = calle + "+" + item;

                cont++;
            }
            calle = calle + "+" + neg.Sucursal.FirstOrDefault().Domicilio.numero + ",+";

            cont = 0;
            string loc = "";
            String[] parts_localidad = neg.Sucursal.FirstOrDefault().Domicilio.Localidad.nombreLocalidad.Split(' ');
            foreach (var item in parts_localidad)
            {
                if (cont == 0)
                    loc = item;
                else
                    loc = loc + "+" + item;

                cont++;
            }

            address = "https://maps.googleapis.com/maps/api/geocode/json?address=" + calle + loc + ",+Cordoba,+Argentina";

            ViewBag.Address = address;
            ViewBag.NombreNegocio = neg.nombre;
            ViewBag.AddressShow = neg.Sucursal.FirstOrDefault().Domicilio.calle + " " + neg.Sucursal.FirstOrDefault().Domicilio.numero + ", " + neg.Sucursal.FirstOrDefault().Domicilio.Localidad.nombreLocalidad;
            ViewBag.Perfil = usuarioActual.idPerfil;

            List<PromocionesNegocioEntity> pro = pm.getPromociones(neg.idNegocio, 1);

            neg.Promociones = pro;

            
            return View(neg);
        }
        public ActionResult NuevoCom(int? idNegocio)
        {
            ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro");

            NegocioEntity neg = new NegocioEntity();

            if(idNegocio != 0)
                 neg = nm.GetNegocioById((int)idNegocio);

            return PartialView(neg);
        } //NO LA USO - POR SI NO FUNCIONA LA VISTA PARCIAL//
        public ActionResult NuevoCasaODpto()
        {
            dom.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
            SucursalEntity suc = new SucursalEntity()
            {
                Domicilio = dom
            };
            neg.Sucursal.Add(suc);
            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            return PartialView(neg);
        }
        public ActionResult EditCasaODpto(int? idNegocio, string comentario)
        {

            if (comentario != "")
            {
                ViewBag.Comentario = comentario;
            }

            NegocioEntity negocio = nm.GetNegocioById((int)idNegocio);
            negocio.Sucursal.FirstOrDefault().Domicilio.listLocalidadesCercanas = dm.GetLocalidadesCercanas();

            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            ViewBag.IdNegocio = negocio.idNegocio;
            return View(negocio);
        }
        public ActionResult NuevoHotel()
        {
            dom.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
            SucursalEntity suc = new SucursalEntity()
            {
                Domicilio = dom
            };
            neg.Sucursal.Add(suc);
            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");

            List<TipoHabitacion> habs = nm.GetTiposHabitacion();
            List<HabitacionesEntity> habitaciones = new List<HabitacionesEntity>();
            foreach (var item in habs)
            {
                habitaciones.Add(new HabitacionesEntity()
                {
                    idTipoHabitacion = item.idTipoHabitacion,
                    nombre = item.nombre
                });
            }
            ViewBag.Habitaciones = habitaciones;

            return PartialView("NuevoHotel", neg);
        }
        public ActionResult EditHotel(int? idNegocio, string comentario)
        {
            if (comentario != "")
            {
                ViewBag.Comentario = comentario;
            }

            NegocioEntity negocio = nm.GetNegocioById((int)idNegocio);
            negocio.Sucursal.FirstOrDefault().Domicilio.listLocalidadesCercanas = dm.GetLocalidadesCercanas();

            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            ViewBag.IdNegocio = idNegocio;

            return View(negocio);
        }
        public ActionResult NuevoComplejo()
        {
            dom.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
            SucursalEntity suc = new SucursalEntity()
            {
                Domicilio = dom
            };
            neg.Sucursal.Add(suc);
            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            ViewBag.TiposComplejo = new SelectList(db.TipoComplejo, "idTipoComplejo", "nombreTipoComplejo");
            return PartialView(neg);
        }
        public ActionResult EditComplejo(int? idNegocio, string comentario)
        {        
            NegocioEntity negocio = nm.GetNegocioById((int)idNegocio);
            negocio.Sucursal.FirstOrDefault().Domicilio.listLocalidadesCercanas = dm.GetLocalidadesCercanas();

            ViewBag.Comentario = comentario;
            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            ViewBag.TiposComplejo = new SelectList(db.TipoComplejo, "idTipoComplejo", "nombreTipoComplejo");
            ViewBag.IdNegocio = negocio.idNegocio;

            return View(negocio);
        }
        public ActionResult EditHospedaje(int? idNegocio, bool? esCorreccion)
        {
            ObtenerUsuarioActual();
            NegocioEntity negocio = nm.GetNegocioById((int)idNegocio);

            string comment = "";

            if ((bool)esCorreccion)
            {
                foreach (var item in negocio.Tramite)
                {
                    if (item.idEstadoTramite == 6)
                        comment = item.comentario;
                }
            }

            switch (negocio.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje)
            {
                case 1: return RedirectToAction("EditCasaODpto", new { idNegocio = negocio.idNegocio, comentario = comment });
                        break;
                case 2: return RedirectToAction("EditComplejo", new { idNegocio = negocio.idNegocio, comentario = comment });
                        break;
                case 3: return RedirectToAction("EditHotel", new { idNegocio = negocio.idNegocio, comentario = comment });
                        break;
                default: break;
            }

            return View(neg);
        }
        [HttpPost]
        public ActionResult EditHospedaje([Bind(Include = "nombre,descripcion")] NegocioEntity negocio,
                                          [Bind(Include = "localidadSeleccionada,barrio,calle,dpto,numero")] DomicilioEntity domEn,
                                          HttpPostedFileBase imagenPrinc,
                                          HttpPostedFileBase imagenMuestra1,
                                          HttpPostedFileBase imagenMuestra2,
                                          HttpPostedFileBase imagenMuestra3,
                                          HttpPostedFileBase imagenMuestra4,
                                          HttpPostedFileBase imagenMuestra5,
                                          HttpPostedFileBase imagenMuestra6,
                                          string telefono,
                                          int TipoHospedaje,
                                          int idNegocioOrig)
        {
            // PARTE NEGOCIO //
            ObtenerUsuarioActual();

            neg.idNegocioModif = idNegocioOrig; //Id del negocio que se modifica
            neg.idUsuario = usuarioActual.idUsuario;
            neg.idTipoNegocio = 1;
            neg.nombre = negocio.nombre;
            neg.descripcion = negocio.descripcion;
            neg.Sucursal.Add(new SucursalEntity()
            {
                esPrincipal = true,
                telefono = telefono,
                Domicilio = domEn
            });

			if (imagenPrinc == null)
            {
                ModelState.AddModelError("", "Debés seleccionar una imagen principal.");
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                return View("EditHospedaje", neg);
            }
	
            // FIN PARTE NEGOCIO //

            // PARTE IMAGENES //
            byte[] buffer = null;
            using (var binaryReader = new BinaryReader(imagenPrinc.InputStream))
            {
                buffer = binaryReader.ReadBytes(imagenPrinc.ContentLength);
            }

            FotosNegocio fotoPrinc = new FotosNegocio()
            {
                foto = buffer,
                esPrincipal = true
            };

            List<HttpPostedFileBase> listaHttp = new List<HttpPostedFileBase>() { imagenMuestra1, imagenMuestra2, imagenMuestra3, imagenMuestra4, imagenMuestra5, imagenMuestra6 };

            List<FotosNegocio> fotosNegocio = convertirImagenesMuestra(listaHttp);
            fotosNegocio.Add(fotoPrinc);

            neg.FotosNegocio = fotosNegocio;

            // FIN PARTE IMAGENES //

            // PARTE LUGAR HOSPEDAJE //
            LugarHospedajeEntity lug = new LugarHospedajeEntity();
            lug.idTipoLugarHospedaje = TipoHospedaje;
            lug.CaracteristicasHospedaje = new List<CaracteristicasHospedajeEntity>();
            neg.LugarHospedaje = new List<LugarHospedajeEntity>() { lug };

            SelectList carac = new SelectList(db.Caracteristica, "idCaracteristica", "nombre");
            foreach (var item in carac)
            {
                string s = Request.Form[item.Text];
                bool selected = false;

                if (s != "false" && s != null)
                    selected = true;

                if (selected)
                {
                    CaracteristicasHospedajeEntity c = new CaracteristicasHospedajeEntity()
                    {
                        idCaracteristica = int.Parse(item.Value)
                    };
                    neg.LugarHospedaje.FirstOrDefault().CaracteristicasHospedaje.Add(c);
                }
            }


            switch (neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje)
            {
                case 1: List<CasaDptoOCabana> casaDptosOCabanas = new List<CasaDptoOCabana>() { new CasaDptoOCabana() };
                    neg.LugarHospedaje.FirstOrDefault().CasaDptoOCabana = casaDptosOCabanas;
                    break;

                case 2: List<ComplejoEntity> complejos = new List<ComplejoEntity>() { new ComplejoEntity() };
                    neg.LugarHospedaje.FirstOrDefault().Complejo = complejos;
                    neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idCategoria = int.Parse(Request.Form["categ"].ToString());
                    neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idTipoComplejo = int.Parse(Request.Form["tipos"].ToString());
                                  

                    break;
                case 3: List<HotelEntity> hoteles = new List<HotelEntity>() { new HotelEntity() };
                    neg.LugarHospedaje.FirstOrDefault().Hotel = hoteles;
                    neg.LugarHospedaje.FirstOrDefault().Hotel.FirstOrDefault().idCategoria = int.Parse(Request.Form["categ"].ToString());
                                        
                    break;

                default:
                    break;
            }

            // FIN PARTE LUGAR HOSPEDAJE //

            // PARTE INSERCIÓN NEGOCIO //
            if (ModelState.IsValid)
            {
                if (nm.ValidarExisteNegocio(neg.nombre,neg.idNegocioModif))
                    nm.AddNegocio(neg, usuarioActual);
                else
                {
                    ModelState.AddModelError("", "Un comercio con el mismo nombre ya está registrado. Por favor elige otro.");

                    ViewBag.Perfil = usuarioActual.idPerfil;
                    ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro");
                    ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                    ViewBag.CaracHotel = new SelectList(db.Caracteristica, "idCaracteristica", "nombre");
                    ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");

                    return RedirectToAction("EditHospedaje", new { idNegocio = neg.idNegocio });
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult EditComercio(int? idNegocio, bool? esCorreccion)
        {
            ObtenerUsuarioActual();
            neg = nm.GetNegocioById((int)idNegocio);

            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro", neg.Comercio.FirstOrDefault().idRubro);
            ViewBag.idNegocio = idNegocio;

            if((bool)esCorreccion)
            {
                foreach (var item in neg.Tramite)
                {
                    if (item.idEstadoTramite == 6)
                        ViewBag.Comentario = item.comentario;
                }
            }
                

            neg.Sucursal.FirstOrDefault().Domicilio.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
   
            return View(neg);
        }
        [HttpPost]
        public ActionResult EditComercio(NegocioEntity negocio,
                                  [Bind(Include = "idRubro")] ComercioEntity comercio,
                                  [Bind(Include = "localidadSeleccionada,barrio,calle,numero,dpto")] DomicilioEntity domEn,
                                  string telefono,
                                  HttpPostedFileBase imagenPrinc,
                                  HttpPostedFileBase imagenMuestra1,
                                  HttpPostedFileBase imagenMuestra2,
                                  HttpPostedFileBase imagenMuestra3,
                                  HttpPostedFileBase imagenMuestra4,
                                  HttpPostedFileBase imagenMuestra5,
                                  HttpPostedFileBase imagenMuestra6,
                                  int idNegocioOrig)
        {
            ObtenerUsuarioActual();

            neg.idNegocioModif = idNegocioOrig;
            neg.idUsuario = usuarioActual.idUsuario;
            neg.idTipoNegocio = 2;
            neg.nombre = negocio.nombre;
            neg.descripcion = negocio.descripcion;
            neg.Comercio = new List<ComercioEntity>() { comercio };

            neg.Sucursal.Add(new SucursalEntity()
            {
                esPrincipal = true,
                telefono = telefono,
                Domicilio = domEn
            });


            if (imagenPrinc == null)
            {
                ModelState.AddModelError("", "Debés seleccionar una imagen principal.");
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro", neg.Comercio.FirstOrDefault().idRubro);
                ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                return View("EditComercio", neg);
            }
            byte[] buffer = null;
            using (var binaryReader = new BinaryReader(imagenPrinc.InputStream))
            {
                buffer = binaryReader.ReadBytes(imagenPrinc.ContentLength);
            }

            FotosNegocio fotoPrinc = new FotosNegocio()
            {
                foto = buffer,
                esPrincipal = true
            };

            List<HttpPostedFileBase> listaHttp = new List<HttpPostedFileBase>() { imagenMuestra1, imagenMuestra2, imagenMuestra3, imagenMuestra4, imagenMuestra5, imagenMuestra6 };

            List<FotosNegocio> fotosNegocio = convertirImagenesMuestra(listaHttp);
            fotosNegocio.Add(fotoPrinc);

            neg.FotosNegocio = fotosNegocio;

            if (ModelState.IsValid)
            {
                nm.AddNegocio(neg, usuarioActual);
            }
            else
            {
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro", neg.Comercio.FirstOrDefault().idRubro);
                ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                return View("EditComercio/"+idNegocioOrig, neg);
            }

            return RedirectToAction("Index", "Home");
        }
        public List<FotosNegocio> convertirImagenesMuestra(List<HttpPostedFileBase> listaHttp)
        {
            List<FotosNegocio> lista = new List<FotosNegocio>();

            foreach (var item in listaHttp)
	        {
                if (item != null)
                {
                    byte[] buffer = null;
                    using (var binaryReader = new BinaryReader(item.InputStream))
                    {
                        buffer = binaryReader.ReadBytes(item.ContentLength);
                    }

                    FotosNegocio fotoMuestra = new FotosNegocio()
                    {
                        foto = buffer,
                        esPrincipal = false
                    };

                    lista.Add(fotoMuestra);
                }
	        }

            return lista;
        }
        public bool ValidarPermisoVista(string controlador, string vista) //METODO ÚNICO DEL CONTROLADOR PARA VALIDAR PERMISO DE LA VISTA (LLAMA AL MANEJADOR).
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