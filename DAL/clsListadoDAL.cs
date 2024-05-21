using Entidades;
using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.Design;

namespace DAL
{
    public class clsListadoDAL
    {
        /// <summary>
        /// Obtiene de la bbdd los personajes y devuelve una lista
        /// </summary>
        /// <returns>la lista de personajes</returns>
        public static List<clsPersonaje> ObtenerListaPersonajesDAL()
        {
            List<clsPersonaje> lista = new List<clsPersonaje>();
            SqlConnection miConexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;
            clsPersonaje Personaje;
            miConexion.ConnectionString = clsConexion.CadenaConexion();

            miConexion.Open();
            // Creamos el comando (Creamos el comando, le pasamos la sentencia y la conexion, y lo ejecutamos)
            miComando.CommandText = "SELECT * FROM personajes";
            miComando.Connection = miConexion;
            miLector = miComando.ExecuteReader();
            // Si hay lineas en el lector
            if (miLector.HasRows)
            {
                while (miLector.Read())
                {
                    clsPersonaje personaje = new clsPersonaje(
                            (int)miLector["IdPersonaje"],
                            (string)miLector["nombre"],
                            (string)miLector["foto"]
                        );
                    lista.Add(personaje);
                }
            }
            miLector.Close();
            miConexion.Close();
            return lista;
        }
        /// <summary>
        /// Obtiene la lista de los combates de la bbdd y la devuelve.
        /// </summary>
        /// <returns>la lista de combates</returns>
        public static List<clsCombate> ObtenerListaCombatesDAL()
        {
            List<clsCombate> lista = new List<clsCombate>();
            SqlConnection miConexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;
            clsCombate Combate;
            miConexion.ConnectionString = clsConexion.CadenaConexion();

            miConexion.Open();
            // Creamos el comando (Creamos el comando, le pasamos la sentencia y la conexion, y lo ejecutamos)
            miComando.CommandText = "SELECT * FROM combates";
            miComando.Connection = miConexion;
            miLector = miComando.ExecuteReader();
            // Si hay lineas en el lector
            if (miLector.HasRows)
            {
                while (miLector.Read())
                {
                    clsCombate combate = new clsCombate(
                            (DateTime)miLector["Fecha"],
                            (int)miLector["IdPersonaje1"],
                            (int)miLector["IdPersonaje2"],
                            (int)miLector["IdPersonaje1"],
                            (int)miLector["IdPersonaje2"]
                        );
                    lista.Add(combate);
                }
            }
            miLector.Close();
            miConexion.Close();
            return lista;
        }
        /// <summary>
        /// Calcula las puntuaciones de los personajes en base a los combates registrados.
        /// </summary>
        /// <returns>La lista de los personajes de mayor a menor puntuación</returns>
        public static List<clsPersonaje> Puntuaciones()
        {
            List<clsPersonaje> listaPersonajes = ObtenerListaPersonajesDAL();
            List<clsCombate> listaCombates = ObtenerListaCombatesDAL();

            foreach (clsCombate combate in listaCombates)
            {
                clsPersonaje personaje1 = listaPersonajes.Find(p => p.IdPersonaje == combate.IdPersonaje1);
                clsPersonaje personaje2 = listaPersonajes.Find(p => p.IdPersonaje == combate.IdPersonaje2);

                if (personaje1 != null)
                    personaje1.Puntuacion += combate.PuntuacionPersonaje1;

                if (personaje2 != null)
                    personaje2.Puntuacion += combate.PuntuacionPersonaje2;
            }
            return listaPersonajes.OrderByDescending(p => p.Puntuacion).ToList();
        }

    }
}