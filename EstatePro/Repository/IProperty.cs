using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface IProperty
    {
        void AddProperty(Property p);
        List<Property> prp();

        void Delete(int id);

        Property Edit(int id);
        void Edit(Property p);

    }
}
