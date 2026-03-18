namespace Videojuegos;

public class Nave : ElJugador
{
    public char DiseñoNave { get; } = '^';
    public int Municion { get; private set; }

    private readonly int _limiteIzq;
    private readonly int _limiteDer;

    public Nave(int x, int y, int anchoPantalla)
        : base(x, y, vida: 3, velocidad: 1)
    {
        Municion = 100;
        _limiteIzq = 1;
        _limiteDer = anchoPantalla - 2;
    }

    public override void Mover(ConsoleKey direccion)
    {
        if (direccion == ConsoleKey.LeftArrow && PosicionX > _limiteIzq)
            PosicionX -= Velocidad;
        else if (direccion == ConsoleKey.RightArrow && PosicionX < _limiteDer)
            PosicionX += Velocidad;
    }

    public override Bala? Disparar()
    {
        if (Municion <= 0) return null;
        Municion--;
        return new Bala(PosicionX, PosicionY - 1, direccionY: -1, simbolo: '|');
    }
}
