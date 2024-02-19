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
            //alapok(etelek);
            //komplex1(etelek);
            komplex2(etelek);
            Console.ReadKey();
        }

        private static void komplex2(List<Etel> etelek)
        {
            // legtöbb energiát tartalmazó ételt.
            Etel maxEnergiaEtel= etelek.OrderByDescending(x => x.energia).First();
            Console.WriteLine($"{maxEnergiaEtel.neve} {maxEnergiaEtel.energia}");
            Console.WriteLine();

            // legdrágább ételt az összes élelmiszer között.
            Etel maxArasEtel = etelek.OrderByDescending(x => x.ara).First();
            Console.WriteLine($"{maxArasEtel.neve} {maxArasEtel.ara}");
            Console.WriteLine();

            // minden kategória legdrágább ára.
            etelek.GroupBy(x => x.kategoria)
                .ToList().ForEach(x => Console.WriteLine($"{x.Key} {x.Max(y => y.ara)}"));
            Console.WriteLine();

            // listázza az alacsony energia tartalmú ételeket.
            // Melyek energia értéke kevesebb, mint 300.
            etelek.Where(x => x.energia < 300).ToList()
                .ForEach(x => Console.WriteLine($"{x.neve} {x.energia}"));
            Console.WriteLine();

            // Határozza meg, hogy melyik kategóriában van a legtöbb étel,
            // és hogy ez hány darab ételt jelent!
            // Rendezze a kategoriákat a benne lévő ételek darabszáma
            // szerint növekvő sorrendbe.
            etelek.GroupBy(x => x.kategoria)
                .Select(x => new { kategoria = x.Key, darabszam = x.Count() })
                .OrderBy(x=>x.darabszam)
                .ToList()
                .ForEach(x => Console.WriteLine($"{x.kategoria} {x.darabszam}"));
            Console.WriteLine();

            // melyik az a kategória amelyikben a legtöbb étel van.
            // Írassa ki a kategória nevét, és hogy hány darab
            var legtobbEtel = etelek.GroupBy(x => x.kategoria)
                .Select(x => new { kategoria = x.Key, darabszam = x.Count() })
                .OrderByDescending(x => x.darabszam)
                .First();
            Console.WriteLine($"{legtobbEtel.kategoria} {legtobbEtel.darabszam}");
            Console.WriteLine();

            // Készítsen egy listát az összes desszertről, amelyek kategóriája "D",
            // majd árak szerint csökkenő sorrendben rendezve írassa ki!
            etelek.Where(x => x.kategoria == "D")
                .OrderByDescending(x => x.ara)
                .ToList()
                .ForEach(x => Console.WriteLine($"{x.neve} {x.ara}"));
            Console.WriteLine();

            // Írassa ki azokat az ételeket, amelyeknek a
            // szénhidrát tartalma nem haladja meg az energia tartalmát!
            // (kisebb egyenlő)
            etelek.Where(x => x.szenh <= x.energia)
                .ToList().ForEach(x => Console.WriteLine($"{x.neve} {x.szenh}<={x.energia}"));

            // Írassa ki egy-egy adott kategóriához tartozó ételek
            // átlagos árát 2 tizedesre kerekítve!
            etelek.GroupBy(x => x.kategoria)
                .ToList()
                .ForEach(x => Console.WriteLine($"{x.Key} {x.Average(y=>y.ara):0.00}"));
            Console.WriteLine();

            etelek.GroupBy(x => x.kategoria)
                .ToList()
                .ForEach(x => Console.WriteLine($"{x.Key} {Math.Round(x.Average(y => y.ara), 2)}"));
            Console.WriteLine();

            // Határozza meg, hogy az "L" kategóriában VAN-e legalább egy étel,
            // amelynek a szénhidrát tartalma meghaladja az 50g-ot!
            bool vanE= etelek.Where(x => x.kategoria == "L")
                .Any(x => x.szenh > 50);
            Console.WriteLine(vanE ? "van" : "nincs");
            Console.WriteLine();

            // VAN-e olyan kategoria amelyikben van legalább 10 étel
            bool vanE2= etelek.GroupBy(x => x.kategoria).Any(x => x.Count() >= 10);
            Console.WriteLine(vanE2 ? "van" : "nincs");
            Console.WriteLine();

            // Határozza meg azokat az ételeket, amelyeknek a neve
            // tartalmazza a "leves" szóösszetételt!
            etelek.Where(x => x.neve.ToLower().Contains("leves".ToLower()))
                .ToList().ForEach(x => Console.WriteLine(x.neve));
            Console.WriteLine();

            // Határozza meg azokat az ételeket, amelyeknek a neve
            // tartalmazza a "leves" ÉS "csirke" szóösszetételt!
            etelek.Where(x => x.neve.Contains("leves") && x.neve.Contains("csirke"))
                .ToList().ForEach(x => Console.WriteLine(x.neve));
            Console.WriteLine();

            // Határozza meg azokat az ételeket, amelyeknek a neve tartalmazza
            // a "leves" VAGY "csirke" szóösszetételt!
            etelek.Where(x => x.neve.Contains("leves") || x.neve.Contains("csirke"))
                .ToList().ForEach(x => Console.WriteLine(x.neve));
            Console.WriteLine();

            // Írja ki az összes olyan ételt, amelynek az ára 500
            // forint alatt van, de energia tartalma legalább 200 kcal!
            etelek.Where(x => x.ara < 500 && x.energia >= 200)
                .ToList().ForEach(x => Console.WriteLine($"{x.neve} {x.ara} {x.energia}"));
            Console.WriteLine();

            // !DISTINCT! mely kategóriákban vannak jelen a
            // csike szóösszetételt tartalmazó ételek
            etelek.Where(x => x.neve.ToLower().Contains("csirke"))
                .Select(x => new { kategoria = x.kategoria })
                .Distinct()
                .ToList()
                .ForEach(x => Console.WriteLine(x.kategoria));
            Console.WriteLine();

            // a leves "L" kategóriákba tartozó ételek árait rendezze
            // csökkenő sorrendben egy ár egyszer szerepeljen
            etelek.Where(x => x.kategoria == "L")
                .OrderByDescending(x => x.ara)
                .Select(x=>new {ara=x.ara })
                .Distinct()
                .ToList()
                .ForEach(x => Console.WriteLine(x.ara));
            Console.WriteLine();

            // Határozza meg azokat az ételeket, amelyek
            // energia tartalma a szénhidrát tartalmának legalább kétszerese!
            etelek.Where(x => x.energia >= x.szenh * 2)
                .ToList().ForEach(x => Console.WriteLine($"{x.neve} {x.energia}>={x.szenh}*2"));
            Console.WriteLine();

            // Írja ki azokat az ételeket, amelyek energia
            // tartalma meghaladja az átlagos energiatartalmat.
            etelek.Where(x => x.energia > etelek.Average(y => y.energia))
                .ToList()
                .ForEach(x => Console.WriteLine($"{x.energia} {etelek.Average(y => y.energia):0.00}"));
            Console.WriteLine();

            // Határozza meg, hogy melyik étel kategóriában van a legtöbb 200 alatti
            // energia tartalmú étel, és ez hány darab ételt jelent!
            etelek.Where(x => x.energia < 200).GroupBy(x => x.kategoria)
                .ToList().ForEach(x => Console.WriteLine($"{x.Key} {x.Count()}"));
        }



        private static void komplex1(List<Etel> etelek)
        {
            // írassuk ki az összes Desszert árát, egy ár egyszer szerepeljen
            etelek.Where(x => x.kategoria == "D")
                .Select(x=>new { ara=x.ara})
                .Distinct()
                .ToList()
                .ForEach(x => Console.WriteLine(x.ara));
            Console.WriteLine();

            // Mennyibe kerülne, ha az összes desszertből szeretnénk
            // elfogyasztani egy adagot?
            int ara = etelek.Where(x => x.kategoria == "D")
                .Sum(x => x.ara);
            Console.WriteLine(ara);
            Console.WriteLine();

            //Jelenítse meg a képernyőn az egyes ételkategóriák(K, D, F, L)
            //átlagos energia-és szénhidrát tartalmát!2 tizedes
            etelek.GroupBy(x => x.kategoria)
                .ToList()
                .ForEach(x => Console.WriteLine($"nev: {x.Key}, en: {x.Average(y => y.energia):0.00}, szh: {x.Average(y => y.szenh):0.00}, összeg: {x.Sum(y => y.ara)}"));
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
            int energia = etelek.Max(x => x.energia);
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
        }

        private static void alapok(List<Etel> etelek)
        {
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
            // lekódolva
            bool vanE2 = false;
            for (int i=0, vanE3=0; vanE3==0 && i<etelek.Count();i++) 
                if (etelek[i].kategoria == "F") vanE3 = 1;
            Console.WriteLine(vanE2 ? "van" : "nincs");
        }
    }
}
