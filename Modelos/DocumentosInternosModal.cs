using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Modelos
{
    public class DocumentosInternosModal
    {
        public List<Tipos_documento_interno> ObtenerTiposDocInternos()
        {
            using (var context = new DBControlOfficeContext())
            {
                return context.Tipos_documento_interno.ToList();
            }
        }

        public RespuestaModel RegistrarNuevoTipoDocInt(string tipoDocumento)
        {
            RespuestaModel respuesta = new RespuestaModel();
            using (var contex = new DBControlOfficeContext())
            {
                if (!ExisteTipoDocumento(tipoDocumento))
                {
                    try
                    {
                        Tipos_documento_interno doc = new Tipos_documento_interno();
                        doc.Tipo_documento = tipoDocumento;
                        contex.Tipos_documento_interno.Add(doc);
                        contex.SaveChanges();
                        respuesta.SetRespuesta(true);
                        return respuesta;
                    }
                    catch
                    {
                        respuesta.SetRespuesta(false, "Error al ingresar el registro, verifique información o conexión");
                        return respuesta;
                    }
                }
                else
                {
                    respuesta.SetRespuesta(false, "El tipo de documento -" + tipoDocumento + "- ya existe");
                    return respuesta;
                }
            }
        }

        public bool ExisteTipoDocumento(string tipoDocumento)
        {
            using (var context = new DBControlOfficeContext())
            {
                Tipos_documento_interno doc = context.Tipos_documento_interno.Where(x =>
                                                x.Tipo_documento == tipoDocumento
                                                 ).FirstOrDefault();
                if (doc == null)//no existe
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public RespuestaModel registrarDocumentoEstandar(string asuntoEstandar, int tipoDocumentoEstandar, string folioEstandar , string imagenEstandar, string idUsuario)
        {
            RespuestaModel respuesta = new RespuestaModel();
            using (var context = new DBControlOfficeContext())
            {
                try
                {
                    context.Configuration.ValidateOnSaveEnabled = false;
                    Documentos_internos doc = new Documentos_internos();   
                                     
                    doc.Descripcion = asuntoEstandar;
                    doc.Folio = folioEstandar;
                    if (tipoDocumentoEstandar != 0)
                    {
                        doc.Id_tipo_documento = tipoDocumentoEstandar;
                    }
                    doc.Imagen = imagenEstandar;
                    doc.Usuario_registra = idUsuario;
                    context.Documentos_internos.Add(doc);
                    context.SaveChanges();
                    respuesta.SetRespuesta(true);
                    respuesta.alerta = "El documento se ha resgistrado correctamente.";
                }
                catch (Exception ex)
                {
                    respuesta.SetRespuesta(false, "Hubo un error en la base de datos o en la conexión a esta. "+ex);
                }
                return respuesta;
            }
        }

        public JqGridModel<Virtual_documentos_internos> ObtenerDocumentosInternos(JqGrid jq)
        {
            JqGridModel<Virtual_documentos_internos> jqm = new JqGridModel<Virtual_documentos_internos>();

            using (var ctx = new DBControlOfficeContext())
            {
                // Traemos la cantidad de registros
                jq.count = ctx.Documentos_internos.Count();

                // Configuramos el JqGridModel
                jqm.Config(jq);

                //Esta consulta solo sirve para Sql serve 2012 en adelante
                // consulta ="SELECT * FROM Electronicos ORDER BY  Id_electronico  OFFSET 10 ROWS FETCH NEXT 3 ROWS ONLY;",
                //try
                {
                    string consulta = "select top " + jqm.limit + " * from (select *, ROW_NUMBER() over (order by " + jqm.sord +
                           " ) as limites from Documentos_internos ) xx where limites >=" + jqm.start;
                    List<Documentos_internos> l = ctx.Database.SqlQuery<Documentos_internos>(consulta).ToList();
                    List<Virtual_documentos_internos> lista = new List<Virtual_documentos_internos>();
                    int t = 0;
                    for (int i = 0; i < l.Count(); i++)
                    {//obtengo relaciones por cada electronico. Este algoritmo no es nada recomendado, debido al tiempo que toma,
                        //pero en un mapeo directo habria que modificar la entidad del modelo e ingresar los datos directamente
                        Virtual_documentos_internos doc = new Virtual_documentos_internos();
                        t = (int)(l[i].Id_tipo_documento == null ? -1 : l[i].Id_tipo_documento);
                        doc.Tipo_documento = ctx.Tipos_documento_interno.Where(x => x.Id_tipo_documento_interno == t).SingleOrDefault();                        
                        doc.Descripcion = l[i].Descripcion;
                        doc.Folio = l[i].Folio;
                        doc.Id_documento = l[i].Id_documento;
                        doc.Id_tipo_documento = l[i].Id_tipo_documento;
                        doc.Imagen = l[i].Imagen;
                        doc.Usuario_registra = l[i].Usuario_registra;
                        lista.Add(doc);
                    }

                    jqm.DataSource(lista);//ctx.Database.SqlQuery<Electronicos>(consulta).ToList());
                    /*jqm.DataSource(ctx.Database.SqlQuery<Electronicos>(consulta,
                            new SqlParameter("OFFSET", jqm.start),
                            new SqlParameter("FETCH", jqm.limit)).ToList());*/
                }
                //catch(Exception ex)
                {

                }
            }

            return jqm;
        }
    }
}
