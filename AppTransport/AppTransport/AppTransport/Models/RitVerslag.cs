using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTransport.Models
{
    class RitVerslag
    {
        [PrimaryKey, AutoIncrement]
        public int RitverslagId { get; set; }
        public ObservableCollection<Rit> RitVerslagList {get; set;}
    }
}
