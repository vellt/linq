using NetworkHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etterem_gyak
{
    class Etel
    {
        public int id { get; set; }
        public string neve { get; set; }
        public int energia { get; set; }
        public double szenh { get; set; }
        public int ara { get; set; }
        private string kategoriaMezo;
        public string kategoria 
        {
            get => (kategoriaMezo == "") ? "D" : kategoriaMezo;
            set => kategoriaMezo = value;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:3000/etelek";
            List<Etel> etelek = Backend.GET(url).Send().ToList<Etel>();
            // alapok();
            // írassuk ki az összes Desszert árát
            etelek.Where(x => x.kategoria == "D")
                .ToList()
                .ForEach(x => Console.WriteLine(x.ara));
            Console.WriteLine();

            // Mennyibe kerülne, ha az összes desszertből szeretnénk elfogyasztani egy adagot?
            int ara= etelek.Where(x => x.kategoria == "D")
                .Sum(x => x.ara);
            Console.WriteLine(ara);
            Console.WriteLine();

            //Jelenítse meg a képernyőn az egyes ételkategóriák(K, D, F, L)
            //átlagos energia-és szénhidrát tartalmát!2 tizedes
            etelek.GroupBy(x => x.kategoria)
                .ToList()
                .ForEach(x => Console.WriteLine($"nev: {x.Key}, en: {x.Average(y=>y.energia):0.00}, szh: {x.Average(y=>y.szenh):0.00}, összeg: {x.Sum(y=>y.ara)}"));
            Console.WriteLine();

            // Hány féle ételből választhatnak a vendégek az egyes ételkategóriákban? A
            // listát rendezze az ételek száma szerint növekvő sorrendbe!
            etelek.GroupBy(x => x.kategoria)
                .OrderBy(x => x.Count())
                .ToList()
                .ForEach(x => Console.WriteLine($"kat: {x.Key}: {x.Count()}db"));
            Console.WriteLine();

            // Melyek azok az ételkategóriák, melyek átlagos szénhidrát tartalma nem haladja meg a 10g-ot?
            // Jelenítse meg a képernyőn a kategória nevét és az átlagos szénhidrát tartalmat! 2 tizedes
            etelek.GroupBy(x => x.kategoria)
                .Where(x => x.Average(y => y.szenh) <= 10)
                .ToList()
                .ForEach(x => Console.WriteLine($"kat: {x.Key} {x.Average(y => y.szenh):0.00}"));

            // Határozza meg, hogy az ételek közül melyek azok, amelyek energia tartalma a legmagasabb és a legalacsonyabb!
            // max
            int energia= etelek.Max(x => x.energia);
            Console.WriteLine(energia);
            Console.WriteLine();
            // min
            int energia2 = etelek.Min(x => x.energia);
            Console.WriteLine(energia2);
            Console.WriteLine();

            // ugyan ez csak le tudjuk hivatkozni az étel nevét is mondjuk
            // min
            Etel minEtel = etelek.OrderBy(x => x.energia).First();
            Console.WriteLine($"{minEtel.neve}: {minEtel.energia}");
            Console.WriteLine();
            // max
            Etel maxEtel = etelek.OrderByDescending(x => x.energia).First();
            Console.WriteLine($"{maxEtel.neve}: {maxEtel.energia}");
            Console.WriteLine();

            // Írja ki azokat az ételeket, amelyeknek az ára meghaladja a 1000 dollárt!
            etelek.Where(x => x.ara > 1000).ToList().ForEach(x => Console.WriteLine($"{x.neve}: {x.ara}"));

            Console.ReadKey();
        }

        private static void alapok()
        {
            /*
            // forEach
            etelek.ForEach(x => Console.WriteLine($"{x.neve}: ${x.ara}"));
            // leszűkíteni a paramétereket és egy listát képezn
            var lista = etelek.Select(x => new { nev = x.neve, ar = x.ara }).ToList();
            // sorba rendezés ár szerint
            var lista2 = lista.OrderBy(x => x.ar).ToList();
            // csökkenő sorrend
            var lista3 = lista.OrderByDescending(x => x.ar).ToList();
            // megszámolni a lista darabszámát:
            int db = lista3.Count();
            // első elem
            Etel elsoEtel = etelek.First();
            // van-e F kategória
            bool vanE = etelek.Any(x => x.kategoria == "F");
            */
        }
    }
}
