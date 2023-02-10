using System.ComponentModel.DataAnnotations;

namespace Knjizara.Models
{
    public class KnjigaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Naziv knjige je obavezno polje")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naziv knjige mora biti duzine od 2 do 100 karaktera")]
        public string Naziv { get; set; }
        // RegEx sa proverom formata i opsegom bi bio "^\d{1,8}(\.\d{1,2})?$"
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Cena moze sadrzati maksimum 2 decimale")]
        [Range(0, 99999999.99, ErrorMessage = "Cena mora biti izmedju 0 i 99999999.99")]
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
