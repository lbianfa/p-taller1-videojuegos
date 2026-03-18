# Galaga C# — Videojuego de consola

Juego estilo Galaga desarrollado en C# con .NET 8, usando consola como motor gráfico. Proyecto académico para la materia de Paradigma Orientado a Objetos.

---

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Windows (la consola usa APIs específicas de Windows)

---

## Cómo ejecutar

1. Clona o descarga el repositorio
2. Abre una terminal y navega a la carpeta `Spaceship/`
```bash
cd Spaceship
```
3. Ejecuta el juego
```bash
dotnet run
```

---

## Controles

| Tecla | Acción |
|-------|--------|
| `←` `→` | Mover la nave |
| `Espacio` | Disparar |
| `Escape` | Salir |
| `S` / `N` | Jugar de nuevo al terminar |

---

## Estructura del proyecto

```
Spaceship/
├── Program.cs        → Loop principal del juego
├── Pantalla.cs       → Motor del juego: render, colisiones, estado
├── ElJugador.cs      → Clase abstracta base del jugador
├── Nave.cs           → Nave del jugador (hereda ElJugador)
├── Enemigos.cs       → Clase abstracta base de enemigos
├── Caza.cs           → Enemigo rápido, 10 puntos (símbolo: V)
├── Tanque.cs         → Enemigo lento, 20 puntos (símbolo: W)
└── Bala.cs           → Proyectil disparado por jugador o enemigos
```

---

## Diagrama de clases (UML)

```
ElJugador (abstract)
├── +vida: int
├── +posicionX, posicionY: int
├── +velocidad: int
├── +Mover(ConsoleKey): abstract
├── +Disparar(): abstract Bala?
└── +RecibirDaño()
        △
        |
      Nave
      ├── +diseñoNave: char ('^')
      └── +municion: int

Enemigos (abstract)
├── +posicionX, posicionY: int
├── +aparienciaEnemigo: char (abstract)
├── +MoverAutomatico(): abstract
├── +Disparar(): abstract Bala?
└── +Morir()
        △
       / \
    Caza  Tanque
    (V)    (W)

Pantalla
├── +ancho, alto: int
├── -jugador: Nave
├── -listaEnemigos: List<Enemigos>
├── -balas: List<Bala>
├── +Actualizar(ConsoleKey?)
├── +Dibujar()
└── +VerificarColisiones()

Bala
├── +x, y: int
├── +direccionY: int  (-1 jugador / +1 enemigo)
├── +activa: bool
├── +simbolo: char
└── +Mover()
```

---

## Conceptos de POO aplicados

| Concepto | Aplicación |
|----------|-----------|
| Herencia | `Nave : ElJugador`, `Caza : Enemigos`, `Tanque : Enemigos` |
| Abstracción | `abstract class ElJugador`, `abstract class Enemigos` |
| Polimorfismo | `e.Disparar()` y `e.AparienciaEnemigo` en el loop de `Pantalla` |
| Encapsulamiento | `protected set` en propiedades, acceso controlado con métodos |
| Composición | `Pantalla` contiene `Nave`, `List<Enemigos>` y `List<Bala>` |
| Propiedades | `EstaVivo`, `Activa`, `AparienciaEnemigo` |

---

## Mecánicas del juego

- 8 enemigos tipo **Caza** (fila superior) — rápidos, disparan con 10% de probabilidad por tick
- 6 enemigos tipo **Tanque** (fila inferior) — lentos, disparan con 15% de probabilidad por tick
- Los enemigos se mueven en bloque y bajan una fila al tocar el borde
- El jugador tiene 3 vidas y 100 de munición
- Ganar: eliminar todos los enemigos
- Perder: quedarse sin vidas o que un enemigo llegue a tu fila
