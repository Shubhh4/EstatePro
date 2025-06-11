using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface IPropertyRepository
    {
        IEnumerable<Property> GetAll();
        Property GetById(int id);
        void Add(Property property, List<IFormFile> images);
        void Update(Property property, List<IFormFile> newImages);
        void Delete(int id);
        void Save();
    }
}
