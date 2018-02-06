using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BussinesEntities;
using System.Data.SqlClient;

namespace BL
{
    public class EncuestasManager
    {
        private ConvertidorEntitiesToDAL convert = new ConvertidorEntitiesToDAL();
        public List<EncuestaEntityIndex> GetAllEncuestas()
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                return db.Database.SqlQuery<EncuestaEntityIndex>("exec getEncuestas").ToList();
            }
        }

        //Para mostrar el listado de encuestas asignadas en el panel de control del usuario.
        //public List<EncuestasAsignadas> GetEncuestasByUser(int? idUsuario)
        //{
        //    using (var db = new SitcomEntities())
        //    {
        //        var result = (from en in db.EncuestasAsignadas
        //                      where en.idUsuario == idUsuario
        //                      select new EncuestasAsignadas()
        //                      {
        //                          fechaAsignacion = en.fechaAsignacion,
        //                          fechaRespuesta = en.fechaRespuesta,
        //                          Encuestas = (from enc in db.Encuestas
        //                                       where enc.idEncuesta == en.idEncuesta
        //                                       select new Encuestas()
        //                                       {
        //                                           nombre = enc.nombre,
        //                                           TiposEncuesta = (from tien in db.TiposEncuesta
        //                                                            where tien.idTipoEncuesta == enc.idTipoEncuesta
        //                                                            select tien).FirstOrDefault()
        //                                       }).FirstOrDefault()
        //                      }).ToList();

        //        return result;
        //    }
        //}

        public List<EncuestaEntityIndexUsuario> GetEncuestasAsignadas(int idUsuario)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramIdUsuario = new SqlParameter("@pIdUsuario", idUsuario);

                List<EncuestaEntityIndexUsuario> encuestas = db.Database.SqlQuery<EncuestaEntityIndexUsuario>("getEncuestasAsignadas @idUsuario=@pIdUsuario", paramIdUsuario).ToList();

                return encuestas;
            }
        }

        public void AsignarEncuesta(int? idTipoEncuesta, int? idUsuario, int? idNegocio, string checkOutDate)
        {

            if (checkOutDate == null)
                checkOutDate = "0";

            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramIdTipoEncuesta = new SqlParameter("@pIdTipoEncuesta", idTipoEncuesta);
                SqlParameter paramIdUsuario = new SqlParameter("@pIdUsuario", idUsuario);
                SqlParameter paramIdNegocio = new SqlParameter("@pIdNegocio", idNegocio);
                SqlParameter paramCheckOutDate = new SqlParameter("@pCheckOutDate", checkOutDate);

                int result = db.Database.SqlQuery<Int32>("asignarEncuesta @idTipoEncuesta=@pIdTipoEncuesta, @idUsuario=@pIdUsuario, @idNegocio=@pIdNegocio, @checkOutDate=@pCheckOutDate", paramIdTipoEncuesta, paramIdUsuario, paramIdNegocio, paramCheckOutDate).FirstOrDefault();
            }
        }

        public EncuestaEntity GetEncuestaById(int? id)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var result = (from en in db.Encuestas
                              where en.idEncuesta == id
                              select new EncuestaEntity()
                              {
                                  idEncuesta = en.idEncuesta,
                                  nombre = en.nombre,
                                  descripcion = en.descripcion,
                                  idTipoEncuesta = en.idTipoEncuesta,
                                  esActiva = en.esActiva,
                                  TiposEncuesta = (from te in db.TiposEncuesta
                                                   where te.idTipoEncuesta == en.idTipoEncuesta
                                                   select te).FirstOrDefault()
                              }).FirstOrDefault();

                result.setearComoActiva = (bool)result.esActiva;

                return result;
            }
        }

        public EncuestasAsignadas GetEncuestaAsignadaById(int? id)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var result = (from ea in db.EncuestasAsignadas.Include("Negocio")
                              where ea.idEncuestaAsignada == id
                              select ea).FirstOrDefault();

                return result;
            }
        }
        
        public void CrearPregunta(PreguntasEntity preg)
        {

            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramIdEncuesta = new SqlParameter("@pIdEncuesta", preg.idEncuesta);
                SqlParameter paramIdClasifPregunta = new SqlParameter("@pIdClasifPregunta", preg.idClasifPregunta);
                SqlParameter paramIdTipoRespuesta = new SqlParameter("@pIdTipoRespuesta", preg.idTipoRespuesta);
                SqlParameter paramTextoPregunta = new SqlParameter("@pTextoPregunta", preg.textoPregunta);

                int result = db.Database.SqlQuery<Int32>("addPregunta @idEncuesta=@pIdEncuesta, @idClasifPregunta=@pIdClasifPregunta, @idTipoRespuesta=@pIdTipoRespuesta, @textoPregunta=@pTextoPregunta", paramIdEncuesta, paramIdClasifPregunta, paramIdTipoRespuesta, paramTextoPregunta).FirstOrDefault();
            }
        }

        public void EditPregunta(PreguntasEntity p)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                  var result = (from preg in db.Preguntas
                                where preg.idPregunta == p.idPregunta
                                select preg).FirstOrDefault();

                  result.textoPregunta = p.textoPregunta;
                  result.idTipoRespuesta = p.idTipoRespuesta;
                  result.idClasifPregunta = p.idClasifPregunta;               

                  db.SaveChanges();
            }
        }

        public PreguntasEntity GetPreguntaById(int idPregunta)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var result = (from preg in db.Preguntas
                              where preg.idPregunta == idPregunta
                              select new PreguntasEntity()
                              {
                                  idPregunta = preg.idPregunta,
                                  idTipoRespuesta = preg.idTipoRespuesta,
                                  idClasifPregunta = preg.idClasifPregunta,
                                  idEncuesta = preg.idEncuesta,
                                  textoPregunta = preg.textoPregunta

                              }).FirstOrDefault();


                return result;
            }
        }

        public void EditEncuesta(EncuestaEntity enc)
        {
            using (SitcomEntities db = new SitcomEntities())
            {

                //Si se selecciono setear como activa la encuesta, seteo como NO ACTIVAS la del MISMO TIPO que ya estaba anteriormente.
                if ((bool)enc.setearComoActiva)
                {
                    var result = (from e in db.Encuestas
                                  where e.idTipoEncuesta == enc.idTipoEncuesta
                                  && e.esActiva == true
                                  select e).FirstOrDefault();

                    if (result != null)
                        result.esActiva = false;

                    db.SaveChanges();
                }


                var result2 = (from en in db.Encuestas
                               where en.idEncuesta == enc.idEncuesta
                               select en).FirstOrDefault();

                result2.nombre = enc.nombre;
                result2.descripcion = enc.descripcion;
                result2.idTipoEncuesta = enc.idTipoEncuesta;
                result2.esActiva = enc.setearComoActiva;


                db.SaveChanges();
            }
        }

        public void CrearEncuesta(EncuestaEntity enc)
        {
            using (SitcomEntities db = new SitcomEntities())
            {

                //Si se selecciono setear como activa la encuesta, seteo como NO ACTIVAS la del MISMO TIPO que ya estaba anteriormente.
                if ((bool)enc.setearComoActiva)
                {
                    var result = (from e in db.Encuestas
                                  where e.idTipoEncuesta == enc.idTipoEncuesta
                                  && e.esActiva == true
                                  select e).FirstOrDefault();

                    if (result != null)
                        result.esActiva = false;

                    db.SaveChanges();
                }

                Encuestas en = new Encuestas()
                {
                    nombre = enc.nombre,
                    descripcion = enc.descripcion,
                    idTipoEncuesta = enc.idTipoEncuesta,
                    esActiva = enc.setearComoActiva
                };

                db.Encuestas.Add(en);
                db.SaveChanges();

            }
        }

        public List<PreguntasEntity> GetPreguntasEncuesta(int idEncuesta)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var result = (from preg in db.Preguntas
                              where preg.idEncuesta == idEncuesta
                              orderby preg.idClasifPregunta descending
                              select new PreguntasEntity()
                              {
                                  idPregunta = preg.idPregunta,
                                  idEncuesta = preg.idEncuesta,
                                  idTipoRespuesta = preg.idTipoRespuesta,
                                  textoPregunta = preg.textoPregunta,
                                  TiposRespuesta = (from tres in db.TiposRespuesta
                                                    where tres.idTipoRespuesta == preg.idTipoRespuesta
                                                    select new TiposRespuestaEntity()
                                                    {
                                                        idTipoRespuesta = tres.idTipoRespuesta,
                                                        nombre = tres.nombre,
                                                        descripcion = tres.descripcion,
                                                        RespuestasPosibles = (from rp in db.RespuestasPosibles
                                                                              where rp.idTipoRespuesta == tres.idTipoRespuesta
                                                                              select rp).ToList(),
                                                    }).FirstOrDefault(),
                                  idClasifPregunta = preg.idClasifPregunta,
                                  ClasifPregunta = (from clasif in db.ClasifPregunta
                                                    where clasif.idClasifPregunta == preg.idClasifPregunta
                                                    select clasif).FirstOrDefault(),
                                  Encuesta = (from encu in db.Encuestas
                                              where encu.idEncuesta == preg.idEncuesta
                                              select encu).FirstOrDefault()
                              }).ToList();


                foreach (var item in result)
                {
                    item.TiposRespuesta.RespuestasPosiblesArray = item.TiposRespuesta.RespuestasPosibles.ToArray();
                }

                return result;
            }
        }

        public int GetEncuestaVigente(int idTipoEncuesta)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var idEncuesta = (from en in db.Encuestas
                                  where en.esActiva == true
                                  && en.idTipoEncuesta == idTipoEncuesta
                                  select en.idEncuesta).FirstOrDefault();

                return idEncuesta;
            }
        }

        public void AgregarRespuestas(PreguntasEntity[] respuestas, int idEncuestaAsignada)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                //Inserto registros en RtasXEncuestasAsignadas
                foreach (var item in respuestas)
                {
                    RtasXEncuestasAsignadas rea = new RtasXEncuestasAsignadas()
                    {
                        idEncuestaAsignada = idEncuestaAsignada,
                        idPregunta = item.idPregunta,
                        respuesta = (int)item.respuesta
                    };

                    db.RtasXEncuestasAsignadas.Add(rea);
                }

                db.SaveChanges();

                //Actualizo la tabla EncuestasAsignadas con la fecha de respuesta
                var result = (from ea in db.EncuestasAsignadas
                              where ea.idEncuestaAsignada == idEncuestaAsignada
                              select ea).FirstOrDefault();

                result.fechaRespuesta = DateTime.Now;

                db.SaveChanges();
            }
        }

        public List<RespuestasEncuestaUsuarioEntity> GetRespuestaUsuarioEntity(int idEncuestaAsignada)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramIdEncuestaAsignada = new SqlParameter("@pIdEncuestaAsignada", idEncuestaAsignada);

                List<RespuestasEncuestaUsuarioEntity> respuestas = db.Database.SqlQuery<RespuestasEncuestaUsuarioEntity>("GetRespuestasEncuesta @idEncuestaAsignada=@pIdEncuestaAsignada", paramIdEncuestaAsignada).ToList();

                return respuestas;
            }
        }

        public List<EncuestasRespEntity> GetEncuestasRespondidasNegocio(int idNegocio)
        {
            using(SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramIdNegocio = new SqlParameter("pIdNegocio", idNegocio);

                return db.Database.SqlQuery<EncuestasRespEntity>("GetEncuestasRespondidasNegocio @idNegocio=@pIdNegocio", paramIdNegocio).ToList();
            }
        }

        public List<EncuestasRespEntity> GetEncuestasRespondidas()
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                return db.Database.SqlQuery<EncuestasRespEntity>("GetEncuestasRespondidas").ToList();
            }
        }
    }
}