using System;
using System.Collections.Generic;
using System.Text;

namespace TransportConsole
{
    class Rit
    {
        public Klant Klant { get; set; }
        public string Plaats { get; set; }
        public int Kilometers  { get; set; }
        public DateTime Aankomst { get; set; }
        public DateTime Vertrek { get; set; }
        public string Werktijd { get; set; }
        public string Rijtijd { get; set; }
        public int Paletten { get; set; }
        public string Opmerking { get; set; }
    }
}
