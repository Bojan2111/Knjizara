using System.ComponentModel.DataAnnotations;

namespace Knjizara.Models
{
    public class ZanrModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Naziv zanra je obavezno polje")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Naziv zanra mora biti duzine izmedju 2 i 30 karaktera")]
        public string NazivZanra { get; set; }

        public ZanrModel() { }

        public ZanrModel(int id, string nazivZanra)
        {
            Id = id;
            NazivZanra = nazivZanra;
        }
    }

}
