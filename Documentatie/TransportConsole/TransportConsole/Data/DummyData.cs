using System;
using System.Collections.Generic;
using System.Text;

namespace TransportConsole.Data
{
    internal class DummyData
    {
        public List<Klant> Klanten { get; }
        public static List<Klant> DummyKlanten()
        {
            #region KLANTEN INLADEN;
            var klanten = new List<Klant>()
            {
                new Klant
                {
                    Id = 1,
                    Naam = "Extrade",
                    Plaats = "Beringen"
                },
                new Klant
                {
                    Id = 2,
                    Naam = "Pludis",
                    Plaats = "Bree"
                },
                new Klant
                {
                    Id = 3,
                    Naam = "Buvens",
                    Plaats = "Halen"
                },
                new Klant
                {
                    Id = 4,
                    Naam = "Kippenwinkel",
                    Plaats = "Wezemaal"
                },
                new Klant
                {
                    Id = 5,
                    Naam = "Java",
                    Plaats = "Rotselaar"
                }
            };
            #endregion
            return klanten;
        }
        
    }
}
