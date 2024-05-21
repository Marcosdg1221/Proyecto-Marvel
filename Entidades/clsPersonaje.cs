namespace Entidades
{
    public class clsPersonaje
    {
        #region atributos

        private int idPersonaje;
        private string nombre;
        private string foto;
        private int puntuacion = 0;
        private clsPersonaje personaje;

        #endregion

        #region propiedades

        public int IdPersonaje { get; }
        public string Nombre { get; }
        public string Foto { get; }
        public int Puntuacion { get; set; }

        #endregion

        # region constructores

        public clsPersonaje() { 
        
        }

        public clsPersonaje(int idPersonaje, string nombre, string foto)
        {
            this.IdPersonaje= idPersonaje;
            this.Nombre = nombre;
            this.Foto= foto;
            this.Puntuacion=puntuacion;
        }

        public clsPersonaje(clsPersonaje personaje)
        {
            this.personaje = personaje;
        }
        #endregion
    }
}
