using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entidades;
using DAL;
using BL;

namespace MarvelDEINT.ViewModels
{
    public class ClasificacionViewModel
    {
        //Ya que estamos, ¿Es buena práctica poner los region aún si solo hay uno o también estaría bien dejarlo sin region en este caso?
        #region atributos 
        private ObservableCollection<clsPersonaje> clasificacion;
        DelegateCommand refrescar;
        private bool estaRefrescando = false;
        #endregion

        #region propiedades
        public ObservableCollection<clsPersonaje> Clasificacion
        {
            get { return clasificacion; } 
        }

        public DelegateCommand Refrescar
        {
            get { return refrescar; }
        }

        public bool

        #endregion
        public ClasificacionViewModel()
        {

            clasificacion = new ObservableCollection<clsPersonaje>();
            CargarClasificacion();
        }
        #region métodos y funciones
        /// <summary>
        /// Función que carga la clasificación de los personajes por orden; lo agrega a la prop.
        /// </summary>
        private void CargarClasificacion()
        {
            List<clsPersonaje> listaPersonajes = clsListadosBL.ObtenerPuntuacionesBL().OrderByDescending(p => p.Puntuacion).ToList();

            clasificacion.Clear();
            foreach (clsPersonaje personaje in listaPersonajes)
            {
                clasificacion.Add(personaje);
            }
        }

        public void IsRefreshingExecute()
        {
            estaRefrescando = true;
            //clasificacion = clsListadoBL.ObtenerListaPersonajesBL();
            estaRefrescando = false;
            //estaRefrescando = new DelegateCommand(refrescarExecute) <- esto en el set
            //TODO; pedir el listado a la base de datos,  estaRefrescando = false y notificar el cambio
        }
        #endregion
    }
}