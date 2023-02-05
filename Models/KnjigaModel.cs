namespace Knjizara.Models
{
    public class KnjigaModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public double Cena { get; set; }
        public ZanrModel Zanr { get; set; }
        public bool Izbrisana { get; set; } = false;

        public KnjigaModel() {}
        public KnjigaModel(int id, string naziv, double cena, ZanrModel zanr, bool izbrisana)
        {
            Id = id;
            Naziv = naziv;
            Cena = cena;
            Zanr = zanr;
            Izbrisana = izbrisana;
        }
    }
}
