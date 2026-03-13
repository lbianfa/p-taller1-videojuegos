using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videojuegos
{
    public class MarioKart
    {
        // atributos
        public string NombreJugador { get; set; }
        public string Personaje { get; set; }
        public string Vehiculo { get; set; }

        // constructor

        public MarioKart(string nombreJugador, string personaje, string vehiculo)
        {
            NombreJugador = nombreJugador;
            Personaje = personaje;
            Vehiculo = vehiculo;
        }

    
    }
}
