namespace Videojuegos;

public class Bala
{
    public int X { get; set; }
    public int Y { get; set; }
    public int DireccionY { get; }   // -1 jugador, +1 enemigo
    public bool Activa { get; set; }
    public char Simbolo { get; }

    public Bala(int x, int y, int direccionY, char simbolo = '|')
    {
        X = x;
        Y = y;
        DireccionY = direccionY;
        Activa = true;
        Simbolo = simbolo;
    }

    public void Mover() => Y += DireccionY;
}
