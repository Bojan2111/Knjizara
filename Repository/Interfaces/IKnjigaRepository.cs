using Knjizara.Models;

namespace Knjizara.Repository.Interfaces
{
    public interface IKnjigaRepository
    {
        public List<KnjigaModel> GetAll();
        public List<KnjigaModel> GetAllDeleted();
        public KnjigaModel GetOne(int id);
        public void Delete(int id);
        public void Archive(int id);
        public void Update(KnjigaModel knjiga);
        public void Create(KnjigaModel knjiga);
        public bool CheckIfKnjigaExists(string nazivKnjige);
        public List<KnjigaModel> Sort(int deleted, string field, string sorting);
    }
}
