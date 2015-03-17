using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Modelo
{
    public class ElectronicosModel
    {
        /// <summary>
        /// Obtiene todos los tipos de electronicos que existen
        /// </summary>
        /// <returns></returns>
        public List<Tipo_electronico> ObtenerTiposElectronicos()
        {            
            using (var context = new DBContolOficceContext())
            {
                return context.Tipo_electronico.ToList();                
            }
        }

        public RespuestaModel RegistrarElectronico(int tipo,int cantidad, string marca , string serie, DateTime FechaUso,int utilizado,
            int reemplazo, string idUsuario )
        {
            RespuestaModel respuesta = new Modelo.RespuestaModel();
            using (var context = new DBContolOficceContext())
            {
                try
                {                    
                  Electronicos nuevoEl = new Electronicos();
                    context.Configuration.ValidateOnSaveEnabled = false;
                    
                    //nuevoEl.Id_electronico = 1;
                    nuevoEl.Id_tipo_electronico = tipo;
                    nuevoEl.Cantidad = cantidad;                    
                    //nuevoEl.Id_marca
                    nuevoEl.NoSerie = serie;
                    if (utilizado == 1)
                    {
                        nuevoEl.FechaUso = DateTime.Now;
                    }
                    else if (utilizado == 3)
                    {
                        nuevoEl.FechaUso = FechaUso;
                    }
                    nuevoEl.Reemplazo = reemplazo;
                    nuevoEl.Usuario_registra = idUsuario;
                    nuevoEl.Imagen = "";
                    
                    context.Electronicos.Add(nuevoEl);
                    
                    context.SaveChanges();//guarda todos los datos
                    respuesta.SetRespuesta(true);
                    respuesta.alerta = "El aparato electrónico se ha registrado correctamente.";
                }
                catch(Exception ex)
                {
                    respuesta.SetRespuesta(false,"Hubo un error en la base de datos o en la conexión a esta. "+ex.Message);
                }
            }
            return respuesta;
            
        }

    }
}
