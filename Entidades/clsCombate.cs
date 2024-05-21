namespace Entidades
{
    public class clsCombate
    {
        #region atributos
        private DateTime fecha;
        private int idPersonaje1;
        private int idPersonaje2;
        private int puntuacionPersonaje1;
        private int puntuacionPersonaje2;
        #endregion

        #region propiedades
        public DateTime Fecha { get { return fecha; } set { fecha = value; } }
        public int IdPersonaje1 { get { return idPersonaje1; } set { idPersonaje1 = value; } }
        public int IdPersonaje2 { get { return idPersonaje2; } set { idPersonaje2 = value; } }
        public int PuntuacionPersonaje1 { get { return puntuacionPersonaje1; } set { puntuacionPersonaje1 = value; } }
        public int PuntuacionPersonaje2 { get { return puntuacionPersonaje2; } set { puntuacionPersonaje2 = value; } }
        #endregion

        #region constructores
        public clsCombate() { }

        public clsCombate(DateTime fecha, int idPersonaje1, int idPersonaje2, int puntuacionPersonaje1, int puntuacionPersonaje2)
        {
            this.fecha = fecha;
            this.idPersonaje1 = idPersonaje1;
            this.idPersonaje2 = idPersonaje2;
            this.puntuacionPersonaje1 = puntuacionPersonaje1;
            this.puntuacionPersonaje2 = puntuacionPersonaje2;
        }
        #endregion
    }
}