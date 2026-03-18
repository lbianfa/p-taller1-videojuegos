namespace Videojuegos;

// Enemigo lento, más resistente, dispara más seguido
public class Tanque : Enemigos
{
    public override char AparienciaEnemigo => 'W';
    public override int Puntos => 20;

    private int _ticksParaMover = 0;

    public Tanque(int x, int y) : base(x, y) { }

    public override void MoverAutomatico(int direccion, int anchoPantalla)
    {
        // Se mueve cada 2 ticks
        _ticksParaMover++;
        if (_ticksParaMover >= 2)
        {
            PosicionX += direccion;
            _ticksParaMover = 0;
        }
    }

    public override Bala? Disparar()
    {
        // Dispara con 15% de probabilidad por tick
        if (Random.Shared.Next(100) < 15)
            return new Bala(PosicionX, PosicionY + 1, direccionY: 1, simbolo: '*');
        return null;
    }
}
