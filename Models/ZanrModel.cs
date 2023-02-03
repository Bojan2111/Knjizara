namespace Knjizara.Models
{
    public class ZanrModel
    {
        public int Id { get; set; }
        public string NazivZanra { get; set; }

        public ZanrModel() { }

        public ZanrModel(int id, string nazivZanra)
        {
            Id = id;
            NazivZanra = nazivZanra;
        }
    }

}
