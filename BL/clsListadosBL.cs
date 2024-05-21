using Entidades;

namespace BL
{
    public class clsListadosBL
    {

        /// <summary>
        /// Función estática que llama a la DAL para obtener un listado completo de los personajes
        /// se aplicarán las reglas de negocio necesarias para este listado.
        /// </summary>
        /// <returns>retorna un listado de personas</returns>
        public static List<clsPersonaje> ObtenerListaPersonajesBL()
        {
            List<clsPersonaje> listaPersonajesBL = DAL.clsListadoDAL.ObtenerListaPersonajesDAL();
            return listaPersonajesBL;
        }

        /// <summary>
        /// Funcion estática que devuelve la lista de combates de la DAL
        /// </summary>
        /// <returns>La lista de combates</returns>
        public static List<clsCombate> ObtenerListaCombatesBL()
        {
            List<clsCombate> listaCombatesBL = DAL.clsListadoDAL.ObtenerListaCombatesDAL();
            return listaCombatesBL;
        }
        /// <summary>
        /// Función estática que devuelve el listado de los personajes sin las fotos con las puntuaciones
        /// </summary>
        /// <returns>Listado de las puntuaciones</returns>
        public static List<clsPersonaje> ObtenerPuntuacionesBL() {

            List<clsPersonaje> listaPuntuaciones = DAL.clsListadoDAL.Puntuaciones();
            return listaPuntuaciones;       
        }

    }
}