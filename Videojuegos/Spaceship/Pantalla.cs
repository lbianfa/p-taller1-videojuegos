namespace Videojuegos;

public class Pantalla
{
    public int Ancho { get; }
    public int Alto { get; }

    private List<Enemigos> _listaEnemigos;
    private List<Bala> _balas;
    private Nave _jugador;
    private int _puntuacion;
    private int _direccionEnemigos = 1;
    private int _ticksBajas = 0;

    public Pantalla(int ancho, int alto)
    {
        Ancho = ancho;
        Alto = alto;
        _listaEnemigos = new List<Enemigos>();
        _balas = new List<Bala>();
        _jugador = new Nave(ancho / 2, alto - 2, ancho);
        _puntuacion = 0;
        InicializarEnemigos();
    }

    private void InicializarEnemigos()
    {
        // Fila de Cazas
        for (int col = 0; col < 8; col++)
            _listaEnemigos.Add(new Caza(4 + col * 5, 2));

        // Fila de Tanques
        for (int col = 0; col < 6; col++)
            _listaEnemigos.Add(new Tanque(6 + col * 6, 4));
    }

    public void Actualizar(ConsoleKey? tecla)
    {
        // Mover jugador
        if (tecla == ConsoleKey.LeftArrow || tecla == ConsoleKey.RightArrow)
            _jugador.Mover(tecla.Value);

        // Disparar jugador
        if (tecla == ConsoleKey.Spacebar)
        {
            var bala = _jugador.Disparar();
            if (bala != null) _balas.Add(bala);
        }

        // Mover enemigos y verificar límites
        bool tocoBorde = _listaEnemigos.Any(e => e.Vivo &&
            (e.PosicionX + _direccionEnemigos <= 1 || e.PosicionX + _direccionEnemigos >= Ancho - 2));

        if (tocoBorde)
        {
            _direccionEnemigos *= -1;
            foreach (var e in _listaEnemigos.Where(e => e.Vivo))
                e.PosicionY++;
        }
        else
        {
            foreach (var e in _listaEnemigos.Where(e => e.Vivo))
                e.MoverAutomatico(_direccionEnemigos, Ancho);
        }

        // Disparos enemigos
        _ticksBajas++;
        if (_ticksBajas >= 3)
        {
            foreach (var e in _listaEnemigos.Where(e => e.Vivo))
            {
                var bala = e.Disparar();
                if (bala != null) _balas.Add(bala);
            }
            _ticksBajas = 0;
        }

        // Mover balas
        foreach (var b in _balas) b.Mover();

        // Desactivar balas fuera de pantalla
        foreach (var b in _balas)
            if (b.Y < 1 || b.Y >= Alto - 1) b.Activa = false;

        VerificarColisiones();

        // Limpiar balas inactivas
        _balas.RemoveAll(b => !b.Activa);
    }

    public void VerificarColisiones()
    {
        // Balas del jugador vs enemigos
        foreach (var bala in _balas.Where(b => b.Activa && b.DireccionY == -1))
        {
            foreach (var enemigo in _listaEnemigos.Where(e => e.Vivo))
            {
                if (bala.X == enemigo.PosicionX && bala.Y == enemigo.PosicionY)
                {
                    enemigo.Morir();
                    bala.Activa = false;
                    _puntuacion += enemigo.Puntos;
                }
            }
        }

        // Balas enemigas vs jugador
        foreach (var bala in _balas.Where(b => b.Activa && b.DireccionY == 1))
        {
            if (bala.X == _jugador.PosicionX && bala.Y == _jugador.PosicionY)
            {
                _jugador.RecibirDaño();
                bala.Activa = false;
            }
        }

        // Enemigos llegan al jugador
        foreach (var enemigo in _listaEnemigos.Where(e => e.Vivo))
        {
            if (enemigo.PosicionY >= _jugador.PosicionY)
            {
                _jugador.RecibirDaño(3); // daño fatal
                enemigo.Morir();
            }
        }
    }

    public void Dibujar()
    {
        Console.Clear();

        // Borde superior
        Console.SetCursorPosition(0, 0);
        Console.Write(new string('-', Ancho));

        // Enemigos
        foreach (var e in _listaEnemigos.Where(e => e.Vivo))
        {
            if (e.PosicionY > 0 && e.PosicionY < Alto - 1)
            {
                Console.SetCursorPosition(e.PosicionX, e.PosicionY);
                Console.Write(e.AparienciaEnemigo);
            }
        }

        // Balas
        foreach (var b in _balas.Where(b => b.Activa))
        {
            if (b.Y > 0 && b.Y < Alto - 1)
            {
                Console.SetCursorPosition(b.X, b.Y);
                Console.Write(b.Simbolo);
            }
        }

        // Jugador
        Console.SetCursorPosition(_jugador.PosicionX, _jugador.PosicionY);
        Console.Write(_jugador.DiseñoNave);

        // HUD
        Console.SetCursorPosition(0, Alto - 1);
        Console.Write(new string('-', Ancho));
        Console.SetCursorPosition(0, Alto);
        Console.Write($" Vidas: {_jugador.Vida}  Puntos: {_puntuacion}  Municion: {_jugador.Municion}");
    }

    public bool JuegoTerminado()
    {
        if (!_jugador.EstaVivo) return true;
        if (!_listaEnemigos.Any(e => e.Vivo)) return true;
        return false;
    }

    public bool JugadorGano() => _jugador.EstaVivo && !_listaEnemigos.Any(e => e.Vivo);

    public int Puntuacion => _puntuacion;
}
