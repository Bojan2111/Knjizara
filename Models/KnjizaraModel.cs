namespace Knjizara.Models
{
    public class KnjizaraModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<KnjigaModel> Knjige { get; set; }
        public List<ZanrModel> Zanrovi { get; set; }
        public int BrojacKnjiga { get; set; }
        public int BrojacZanrova { get; set; }
        public KnjizaraModel(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
            Knjige = new List<KnjigaModel>();
            Zanrovi = new List<ZanrModel>();
            BrojacKnjiga = 1;
            BrojacZanrova = 1;
        }
    }
}
