namespace Knjizara.Models
{
    public class KnjigaModel
    {
        public int Id { get; set; }
        public string? Naziv { get; set; }
        public double Cena { get; set; }
        public string? Zanr { get; set; }
        public bool Izbrisana { get; set; } = false;
    }
}
