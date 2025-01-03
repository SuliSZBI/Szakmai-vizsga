using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace VersenyzokKonzol
{
    internal class Versenyzo
    {
        static public List<Versenyzo> VersenyzoLista = new List<Versenyzo>();

        public string Nev { get; set; }
        public DateTime Szuletes_Datuma { get; set; }
        public string Nemzetiseg { get; set; }

        public Versenyzo(string nev, DateTime szuletes_Datuma, string nemzetiseg)
        {
            this.Nev = nev;
            this.Szuletes_Datuma = szuletes_Datuma;
            this.Nemzetiseg = nemzetiseg;
        }

        static public void ListaFeltolt()
        {
            string elsoSor = "";

            using (StreamReader sr = new StreamReader("pilotak.csv", Encoding.UTF8))
            {
                elsoSor = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    string[] tomb = sor.Split(';');

                    string nev = tomb[0];
                    int ev = Int32.Parse(tomb[1].Split('.')[0]);
                    int honap = Int32.Parse(tomb[1].Split('.')[1]);
                    int nap = Int32.Parse(tomb[1].Split('.')[2]);
                    DateTime szuletes_datum = new DateTime(ev, honap, nap);
                    string nemzetiseg = tomb[2];

                    Versenyzo versenyzo = new Versenyzo(nev, szuletes_datum, nemzetiseg);
                    VersenyzoLista.Add(versenyzo);
                }
            }
        }

        static public void Harmadik_Feladat()
        {
            int adatokSzama = VersenyzoLista.Count;

            Console.WriteLine($"3. Feladat: Versenyzők száma: {adatokSzama}");
        }

        static public void Negyedik_Feladat()
        {
            DateTime most = DateTime.Now;
            int minimum = Int32.MaxValue;
            string legfiatalabbNev = "";
            DateTime legfiatalabbSzuletesiDatum = new DateTime();

            foreach (Versenyzo item in VersenyzoLista)
            {
                TimeSpan ts = most.Subtract(item.Szuletes_Datuma);

                if (ts.Days < minimum)
                {
                    legfiatalabbNev = item.Nev;
                    legfiatalabbSzuletesiDatum = item.Szuletes_Datuma;
                    minimum = ts.Days;
                }
            }

            string negyedik = $"4. Feladat: A legfiatalabb pilóta: ";
            negyedik += $"\n\tNeve: {legfiatalabbNev}";
            negyedik += $"\n\tSzületési dátuma: {legfiatalabbSzuletesiDatum.Year}-{legfiatalabbSzuletesiDatum.Month}-{legfiatalabbSzuletesiDatum.Day}";

            Console.WriteLine(negyedik);
        }
    }
}
