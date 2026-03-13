namespace Videojuegos
{
    public class batallaRPG
    {
        public string nombreJugador;
        public int nivelJugador;
        public int saludJugador;
        public string nombreEnemigo;
        public int nivelEnemigo;
        public int saludEnemigo;


        // constructor para inicializar los atributos
        public batallaRPG(string nombreJugador, int nivelJugador, int saludJugador, string nombreEnemigo, int nivelEnemigo, int saludEnemigo)
        {
            this.nombreJugador = nombreJugador;
            this.nivelJugador = nivelJugador;
            this.saludJugador = saludJugador;
            this.nombreEnemigo = nombreEnemigo;
            this.nivelEnemigo = nivelEnemigo;
            this.saludEnemigo = saludEnemigo;
        }
    }
}
