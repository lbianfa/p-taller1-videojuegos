using Videojuegos;

Console.CursorVisible = false;
Console.Title = "Galaga C#";

const int ANCHO = 60;
const int ALTO = 25;

Console.SetWindowSize(ANCHO + 2, ALTO + 3);
Console.SetBufferSize(ANCHO + 2, ALTO + 3);

bool jugarDeNuevo = true;

while (jugarDeNuevo)
{
    var pantalla = new Pantalla(ANCHO, ALTO);
    ConsoleKey? tecla = null;

    while (!pantalla.JuegoTerminado())
    {
        // Leer input sin bloquear
        if (Console.KeyAvailable)
        {
            tecla = Console.ReadKey(intercept: true).Key;
            if (tecla == ConsoleKey.Escape) goto FinJuego;
        }
        else
        {
            tecla = null;
        }

        pantalla.Actualizar(tecla);
        pantalla.Dibujar();

        Thread.Sleep(80); // ~12 fps
    }

    FinJuego:
    Console.Clear();
    Console.SetCursorPosition(ANCHO / 2 - 8, ALTO / 2 - 1);

    if (pantalla.JugadorGano())
        Console.Write("*** GANASTE! ***");
    else
        Console.Write("*** GAME OVER ***");

    Console.SetCursorPosition(ANCHO / 2 - 8, ALTO / 2);
    Console.Write($"Puntuacion: {pantalla.Puntuacion}");
    Console.SetCursorPosition(ANCHO / 2 - 12, ALTO / 2 + 2);
    Console.Write("Jugar de nuevo? (S/N): ");

    var respuesta = Console.ReadKey(intercept: true).Key;
    jugarDeNuevo = respuesta == ConsoleKey.S;
}

Console.CursorVisible = true;
Console.Clear();
Console.WriteLine("Gracias por jugar!");
