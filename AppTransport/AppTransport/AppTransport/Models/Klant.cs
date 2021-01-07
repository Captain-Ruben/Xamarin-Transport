using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTransport.Models
{
    class Klant
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Plaats { get; set; }
    }
}
