using Knjizara.Models;

namespace Knjizara.ViewModels
{
    public class KnjigaZanrViewModel
    {
        public KnjigaModel Knjiga { get; set; }
        public List<ZanrModel> Zanrovi { get; set; }
    }
}
