namespace Videojuegos;

public abstract class ElJugador
{
    public int Vida { get; protected set; }
    public int PosicionX { get; set; }
    public int PosicionY { get; set; }
    public int Velocidad { get; protected set; }

    protected ElJugador(int x, int y, int vida = 3, int velocidad = 1)
    {
        PosicionX = x;
        PosicionY = y;
        Vida = vida;
        Velocidad = velocidad;
    }

    public abstract void Mover(ConsoleKey direccion);
    public abstract Bala? Disparar();

    public void RecibirDaño(int daño = 1)
    {
        Vida -= daño;
    }

    public bool EstaVivo => Vida > 0;
}
