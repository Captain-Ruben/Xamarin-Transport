using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transport.Models
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
        public string Werktijd { get { return (Vertrek - Aankomst).ToString(); } }
        public string Rijtijd { get { return GetRijTijd(this); } }
        public int Paletten { get; set; }
        public DateTime AanmaakDatumRit { get; set; }


        private string GetRijTijd(Rit rit)
        {
            //Database Querry RitId - 1


            return "";
        }
    }

   
}
