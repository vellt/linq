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
            // forEach
            etelek.ForEach(x => Console.WriteLine($"{x.neve}: ${x.ara}"));
            // leszűkíteni a paramétereket és egy listát képezn
            var lista= etelek.Select(x => new { nev = x.neve, ar= x.ara }).ToList();
            // sorba rendezés ár szerint
            var lista2= lista.OrderBy(x => x.ar).ToList();
            // csökkenő sorrend
            var lista3= lista.OrderByDescending(x => x.ar).ToList();
            // megszámolni a lista darabszámát:
            int db = lista3.Count();
            // 
            ;
            Console.ReadKey();
        }
    }
}
