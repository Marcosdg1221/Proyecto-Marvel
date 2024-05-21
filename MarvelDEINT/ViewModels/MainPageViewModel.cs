using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Entidades;
using BL;
using DAL;
using Microsoft.Data.SqlClient;

namespace MarvelDEINT.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region atributos
        private clsPersonaje personajeSeleccionado1;
        private clsPersonaje personajeSeleccionado2;
        private int puntuacion1;
        private int puntuacion2;
        private bool sePuedeEnviarPuntuacion = false;
        #endregion

        #region propiedades

        public ObservableCollection<clsPersonaje> ListaPersonajes1 { get;  }
        public ObservableCollection<clsPersonaje> ListaPersonajes2 { get; }

        public clsPersonaje PersonajeSeleccionado1
        {
            get { return personajeSeleccionado1; }
            set
            {
                personajeSeleccionado1 = value;
                OnPropertyChanged();
                ActualizarSePuedeEnviarPuntuacion();
            }
        }

        public clsPersonaje PersonajeSeleccionado2
        {
            get { return personajeSeleccionado2; }
            set
            {
                personajeSeleccionado2 = value;
                OnPropertyChanged();
                ActualizarSePuedeEnviarPuntuacion();
            }
        }

        public int Puntuacion1
        {
            get { return puntuacion1; }
            set
            {
                puntuacion1 = value;
                OnPropertyChanged();
                ActualizarSePuedeEnviarPuntuacion();
            }
        }

        public int Puntuacion2
        {
            get { return puntuacion2; }
            set
            {
                puntuacion2 = value;
                OnPropertyChanged();
                ActualizarSePuedeEnviarPuntuacion();
            }
        }

        public bool SePuedeEnviarPuntuacion
        {
            get { return sePuedeEnviarPuntuacion; }
            set
            {
                sePuedeEnviarPuntuacion = value;
                OnPropertyChanged();
            }
        }

        public ICommand EnviarPuntuacionCommand { get; }

        #endregion

        public ViewModel()
        {
            ListaPersonajes1 = new ObservableCollection<clsPersonaje>(clsListadosBL.ObtenerListaPersonajesBL());
            ListaPersonajes2 = new ObservableCollection<clsPersonaje>(clsListadosBL.ObtenerListaPersonajesBL());
            EnviarPuntuacionCommand = new Command(EnviarPuntuacion, () => SePuedeEnviarPuntuacion);
        }

        #region métodos y funciones

        /// <summary>
        /// Habilita o no habilita el botón si en ambos sliders hay un valor y los personajes seleccionados no son nulos e iguales.
        /// </summary>
        private void ActualizarSePuedeEnviarPuntuacion()
        {
            if (PersonajeSeleccionado1 != null && PersonajeSeleccionado2 != null && PersonajeSeleccionado1.IdPersonaje != PersonajeSeleccionado2.IdPersonaje &&Puntuacion1 > 0 && Puntuacion2 > 0)        
                SePuedeEnviarPuntuacion = true;            
            else 
              SePuedeEnviarPuntuacion = false;
        (EnviarPuntuacionCommand as Command)?.ChangeCanExecute();
        }

        /// <summary>
        /// Se verifica si un combate existe en la base de datos y lo crea o lo actualiza si existe o no existe.
        /// </summary>
        private void EnviarPuntuacion()
        {

            clsCombate combateExistente = ObtenerCombateExistente();

            if (combateExistente == null)
            {
                clsCombate nuevoCombate = new clsCombate
                {
                    Fecha = DateTime.Now,
                    IdPersonaje1 = PersonajeSeleccionado1.IdPersonaje,
                    IdPersonaje2 = PersonajeSeleccionado2.IdPersonaje,
                    PuntuacionPersonaje1 = Puntuacion1,
                    PuntuacionPersonaje2 = Puntuacion2
                };
                InsertarCombate(nuevoCombate);
            }
            else
            {
                combateExistente.PuntuacionPersonaje1 += Puntuacion1;
                combateExistente.PuntuacionPersonaje2 += Puntuacion2;
                ActualizarPuntuaciones(combateExistente);
            }
            Aviso();
            
        }

        private async Task Aviso()
        {
            string aviso = "Datos guardados con éxito";
            await Application.Current.MainPage.DisplayAlert("", aviso, "Ok");
        }

        /// <summary>
        /// Se consulta en la base de datos si el combate existe buscando un combate existente entre los personajes y la fecha actual, si no existe retorna null.
        /// </summary>
        /// <returns></returns>
        private clsCombate ObtenerCombateExistente()
        {

            using (SqlConnection connection = new SqlConnection(clsConexion.CadenaConexion()))
            {
                string query = "SELECT * FROM combates WHERE (IdPersonaje1 = @IdPersonaje1 AND IdPersonaje2 = @IdPersonaje2 OR IdPersonaje1 = @IdPersonaje2 AND IdPersonaje2 = @IdPersonaje1) AND Fecha = @Fecha";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@IdPersonaje1", PersonajeSeleccionado1.IdPersonaje);
                command.Parameters.AddWithValue("@IdPersonaje2", PersonajeSeleccionado2.IdPersonaje);
                command.Parameters.AddWithValue("@Fecha", DateTime.Now.Date); // Solo la fecha sin la hora para comparar el mismo día

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Si hay filas en el resultado, significa que el combate existe
                    if (reader.HasRows)
                    {
                        reader.Read();
                        clsCombate combate = new clsCombate(
                            (DateTime)reader["Fecha"],
                            (int)reader["IdPersonaje1"],
                            (int)reader["IdPersonaje2"],
                            (int)reader["PuntuacionPersonaje1"],
                            (int)reader["PuntuacionPersonaje2"]
                            );

                        return combate;
                    }
                }
                //Si no, null
                return null;
            }
        }

        /// <summary>
        /// Inserta un nuevo combate en la base de datos.
        /// </summary>
        /// <param name="nuevoCombate">El combate a insertar.</param>
        public static void InsertarCombate(clsCombate combate)
        {
            using (SqlConnection connection = new SqlConnection(clsConexion.CadenaConexion()))
            {
                string query = "INSERT INTO combates (Fecha, IdPersonaje1, IdPersonaje2, PuntuacionPersonaje1, PuntuacionPersonaje2) VALUES (@Fecha, @IdPersonaje1, @IdPersonaje2, @PuntuacionPersonaje1, @PuntuacionPersonaje2)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Fecha", combate.Fecha);
                command.Parameters.AddWithValue("@IdPersonaje1", combate.IdPersonaje1);
                command.Parameters.AddWithValue("@IdPersonaje2", combate.IdPersonaje2);
                command.Parameters.AddWithValue("@PuntuacionPersonaje1", combate.PuntuacionPersonaje1);
                command.Parameters.AddWithValue("@PuntuacionPersonaje2", combate.PuntuacionPersonaje2);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Actualiza las puntuaciones haciendo un update en la base de datos
        /// </summary>
        /// <param name="combate"></param>
        public static void ActualizarPuntuaciones(clsCombate combate)
        {
            using (SqlConnection connection = new SqlConnection(clsConexion.CadenaConexion()))
            {
                string query = "UPDATE combates SET PuntuacionPersonaje1 = @PuntuacionPersonaje1, PuntuacionPersonaje2 = @PuntuacionPersonaje2 WHERE Fecha = @Fecha AND IdPersonaje1 = @IdPersonaje1 AND IdPersonaje2 = @IdPersonaje2";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PuntuacionPersonaje1", combate.PuntuacionPersonaje1);
                command.Parameters.AddWithValue("@PuntuacionPersonaje2", combate.PuntuacionPersonaje2);
                command.Parameters.AddWithValue("@Fecha", combate.Fecha);
                command.Parameters.AddWithValue("@IdPersonaje1", combate.IdPersonaje1);
                command.Parameters.AddWithValue("@IdPersonaje2", combate.IdPersonaje2);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == nameof(SePuedeEnviarPuntuacion))
            {
                (EnviarPuntuacionCommand as Command)?.ChangeCanExecute();
            }
        }
        #endregion
    }
}
