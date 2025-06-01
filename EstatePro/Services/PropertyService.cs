using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;

namespace EstatePro.Services
{
    public class PropertyService : IProperty
    {
        private readonly ApplicationDbContext db;
        public PropertyService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddProperty(Property p)
        {
            db.Properties.Add(p);
            db.SaveChanges();
        }
        public List<Property> prp()
        {
          var data=  db.Properties.ToList();
            return data;
        }
        public void Delete(int id)
        {
           var lab= db.Properties.Find(id);
            if(lab != null)
            {
                db.Properties.Remove(lab);
                db.SaveChanges();
            }
        }
        public Property Edit(int id)
        {
           return db.Properties.Find(id);
        }
        public void Edit(Property p)
        {
            db.Properties.Add(p);   
            db.SaveChanges();   
        }

    }
}
