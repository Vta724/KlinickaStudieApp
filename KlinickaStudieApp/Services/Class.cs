using KlinickaStudieApp.Models;

namespace KlinickaStudieApp.Services
{
    public class DataService
    {
        private readonly IWebHostEnvironment _env;
        private List<Models.StudieZaznam>? _data;
        public DataService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public  List<StudieZaznam> Nacistdata()
        {
            if (_data != null)
            {
                return _data;
            }

            var vysledek = new List<StudieZaznam>();
            string cesta = Path.Combine(_env.WebRootPath, "data", "klinicka_studie_ucinne_latky.csv");
            string[] radky = File.ReadAllLines(cesta, System.Text.Encoding.UTF8);

            for (int i = 1; i < radky.Length; i++)
            {
                string radek = radky[i];
                if (string.IsNullOrWhiteSpace(radek))
                {
                    continue;
                })

                string[] sloupce = radek.Split(';');
                var zaznam = new StudieZaznam
                {
                    skupina = sloupce[0],
                    ucinnalatka = sloupce[1],
                    snizenisymptomu = ParsujDesetinneCislo(sloupce[2]),
                    pocetpacientu = int.Parse(sloupce[3]),
                    vyskytvedlejsichucinku = ParsujDesetinneCislo(sloupce[4])

                };
            vysledek.Add(zaznam);
        }
        _data = vysledek;
            return vysledek;
        }
    private double ParsujDesetinneCislo(string text)
        {
            string upraveny = text.Replace(',', '.');
            return double.Parse(upraveny, System.Globalization.CultureInfo.InvariantCulture);
        }

        public double Prumer(List<StudieZaznam> data)
        {
            if (data.Count == 0)
            {
                return 0;
            }
            double soucet = 0;
            foreach (var zaznam in data)
            {
                soucet = soucet + zaznam.snizenisymptomu;
            }
            return soucet / data.Count;
        }
        public string Nejcastejsiucinnalatka(List<StudieZaznam> data)
        {
            var pocty = new Dictionary<string, int>();
            foreach (var zaznam in data)
            {
                if (pocty.ContainsKey(zaznam.ucinnalatka))
                {
                    pocty[zaznam.ucinnalatka] = pocty[zaznam.ucinnalatka] + 1;

                }
                else
                {
                    pocty[zaznam.ucinnalatka] = 1;
                }
            }
            string nejcastejsi = "";
            int nejvyssipocet = 0;

            foreach (var polozka in pocty)
            {
                if (polozka.Value > nejvyssipocet)
                {
                    nejvyssipocet = polozka.Value;
                    nejcastejsi = polozka.Key;
                }
            }
            return nejcastejsi
        }
    }
}
