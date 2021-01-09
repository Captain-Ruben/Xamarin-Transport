using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTransport.Models
{
    class Rit 
    {
        public Rit()
        {
            AanmaakDatumRit = DateTime.Now;
        }

        [PrimaryKey, AutoIncrement]
        public int RitId { get; set; }
        public string KlantNaam { get; set; }
        public string Plaats { get; set; }
        public int Kilometers { get; set; }
        public DateTime Aankomst { get; set; }
        public DateTime Vertrek { get; set; }
        public string Werktijd { get; set; }
        public string Rijtijd { get; set; }
        public int Paletten { get; set; }
        public DateTime AanmaakDatumRit { get; set; }

    }
}
