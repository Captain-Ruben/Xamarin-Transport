using System;
using System.Collections.Generic;
using System.Linq;
using TransportConsole.Data;

namespace TransportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Ritverslag ritverslag = new Ritverslag();

            bool RittenLoop = true;
            int RittenCount = 0;

            Rit rit = RitverslagRit(RittenCount);
            ritverslag.Ritten.Add(rit);
            RittenCount++;
            Console.Clear();

            while (RittenLoop)
            {
                Console.WriteLine("Nog Een Rit Toevoegen Duw 1");
                Console.WriteLine("Overzicht Ritten Duw 2");
                int optie = int.Parse(Console.ReadLine());
                Console.Clear();
                if (optie == 1)
                {
                    rit = RitverslagRit(RittenCount);
                    for (int i = 0; i < ritverslag.Ritten.Count; i++)
                    {
                        Rit ritList = ritverslag.Ritten[i];
                        TimeSpan rijtijd = rit.Aankomst - ritList.Vertrek;
                        rit.Rijtijd = $"{(int)rijtijd.TotalHours}h {rijtijd:mm}mn {rijtijd:ss}sec";
                    }
                    ritverslag.Ritten.Add(rit);
                    RittenCount++;
                    Console.Clear();
                }
                else if(optie == 2)
                {
                    foreach (var x in ritverslag.Ritten)
                    {
                        Console.WriteLine($"RIT:|{x.Klant.Naam}|{x.Plaats}|{x.Kilometers}KM \n" +
                            $"-------------------------------\n" +
                            $"Aankomst  : {x.Aankomst} \n" +
                            $"Vertrek   : {x.Vertrek}  \n" +
                            $"Werktijd  : {x.Werktijd} \n" +
                            $"Rijtijd   : {x.Rijtijd}  \n" +
                            $"Paletten  : {x.Paletten} \n" +
                            $"-------------------------------");

                       
                    }
                    Console.ReadLine();
                }
                else
                {
                    RittenLoop = false;
                }
            }
        }

        public static Rit RitverslagRit(int RittenCount)
        {
            Rit rit = new Rit();
            List<Klant> klanten = DummyData.DummyKlanten();

            if (RittenCount == 0)
            {
                //Cel 1: Klant 
                Klant klant = KeuzenKlant(klanten);
                rit.Klant = klant;

                //Cel 2: Plaats
                rit.Plaats = klant.Plaats;

                //Cel 3: KM 
                Console.WriteLine("Wat is de kilometerstand?");
                rit.Kilometers = int.Parse(Console.ReadLine());
                
                //Cel 4: Aankomst
                rit.Aankomst = DateTime.MinValue;

                // Cel 5: Vertrek
                rit.Vertrek = VertrekTijd();

                //Cel 6: Werktijd
                rit.Werktijd = "0h 0mn 0sec";

                //Cel 7: Rijtijd
                rit.Rijtijd = "0h 0mn 0sec";

                //Cel 8: Paletten
                Console.WriteLine("Aantal Paletten");
                rit.Paletten = int.Parse(Console.ReadLine());
            }
            else
            {
                //Cel 1: Klant 
                Klant klant = KeuzenKlant(klanten);
                rit.Klant = klant;

                //Cel 2: Plaats
                rit.Plaats = klant.Plaats;

                //Cel 3: KM 
                Console.WriteLine("Wat is de kilometerstand?");
                rit.Kilometers = int.Parse(Console.ReadLine());

                //Cel 4: Aankomst
                rit.Aankomst = AankomstTijd();

                // Cel 5: Vertrek
                rit.Vertrek = VertrekTijd();

                //Cel 6: Werktijd
                TimeSpan werktijd = rit.Vertrek - rit.Aankomst;
                rit.Werktijd = $"{(int)werktijd.TotalHours}h {werktijd:mm}mn {werktijd:ss}sec";

                //Cel 7: Rijtijd
                rit.Rijtijd = null;

                //Cel 8: Paletten
                Console.WriteLine("Aantal Paletten");
                rit.Paletten = int.Parse(Console.ReadLine());
            }

            return rit;

        }
        public static Klant KeuzenKlant(List<Klant> _klanten)
        {
            IEnumerable<Klant> klanten = _klanten;
            Klant returnKlant = null;

            Console.WriteLine("Kies een Klant:");
            foreach (var x in klanten)
            {
                Console.WriteLine($"{x.Id} {x.Naam}");
            }
            Console.WriteLine("Kies door ID hieronder in te vullen");
            int gekozenKlantId = int.Parse(Console.ReadLine());

            var klantX = from x in klanten
                        where x.Id.Equals(gekozenKlantId)
                        select x;

            foreach (Klant k in klantX)
            {
                returnKlant = k;
            }
            return returnKlant;
            
        }
        public static DateTime AankomstTijd()
        {
            Console.WriteLine("Duw Enter Om De Aankomsttijd Te Registreren");
            Console.ReadLine();
            return DateTime.Now;
        }
        public static DateTime VertrekTijd()
        {
            Console.WriteLine("Duw Enter Om De Vertrektijd Te Registreren");
            Console.ReadLine();
            return DateTime.Now;
        }
    }
}
