using System;
using System.Collections.Generic;
using System.Text;

namespace Transport.Models
{
    class Rit
    {        
        public string KlantNaam { get; set; }
        public string Plaats { get; set; }
        public int Kilometers { get; set; }
        public DateTime Aankomst { get; set; }
        public DateTime Vertrek { get; set; }
        public string Werktijd { get; set; }
        public string Rijtijd { get; set; }
        public int Paletten { get; set; }
    }
}
