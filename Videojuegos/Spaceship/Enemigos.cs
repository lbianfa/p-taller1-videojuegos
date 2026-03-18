namespace Videojuegos;

public abstract class Enemigos
{
    public int PosicionX { get; set; }
    public int PosicionY { get; set; }
    public bool Vivo { get; protected set; } = true;
    public abstract char AparienciaEnemigo { get; }
    public abstract int Puntos { get; }

    protected Enemigos(int x, int y)
    {
        PosicionX = x;
        PosicionY = y;
    }

    public abstract void MoverAutomatico(int direccion, int anchoPantalla);
    public abstract Bala? Disparar();

    public void Morir() => Vivo = false;
}
