using Knjizara.Models;

namespace Knjizara.Repository.Interfaces
{
    public interface IZanrRepository
    {
        public List<ZanrModel> GetAll();
        public ZanrModel GetOne(int idZanra);
        public void Delete(int idZanra);
        public void Update(ZanrModel zanr);
        public void Create(ZanrModel zanr);
    }
}
