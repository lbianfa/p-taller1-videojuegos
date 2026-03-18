namespace Videojuegos;

// Enemigo rápido, poco resistente
public class Caza : Enemigos
{
    public override char AparienciaEnemigo => 'V';
    public override int Puntos => 10;

    public Caza(int x, int y) : base(x, y) { }

    public override void MoverAutomatico(int direccion, int anchoPantalla)
    {
        PosicionX += direccion;
    }

    public override Bala? Disparar()
    {
        // Dispara con 10% de probabilidad por tick
        if (Random.Shared.Next(100) < 10)
            return new Bala(PosicionX, PosicionY + 1, direccionY: 1, simbolo: '!');
        return null;
    }
}
