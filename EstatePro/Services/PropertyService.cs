using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.EntityFrameworkCore;

namespace EstatePro.Services
{
    public class PropertyService : IPropertyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PropertyService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IEnumerable<Property> GetAll()
        {
            return _context.Properties.Include(p => p.Images).ToList();
        }

        public Property GetById(int id)
        {
            return _context.Properties.Include(p => p.Images).FirstOrDefault(p => p.PropertyId == id);
        }

        public void Add(Property property, List<IFormFile> images)
        {
            _context.Properties.Add(property);
            _context.SaveChanges();
            SaveImages(property.PropertyId, images);
        }

        public void Update(Property property, List<IFormFile> newImages)
        {
            _context.Properties.Update(property);
            _context.SaveChanges();
            SaveImages(property.PropertyId, newImages);
        }

        public void Delete(int id)
        {
            var property = GetById(id);
            if (property != null)
            {
                Console.WriteLine($"Deleting property: {property.PropertyId} - {property.Title}");

                _context.Properties.Remove(property);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Property with ID {id} not found.");
            }
        }


        private void SaveImages(int propertyId, List<IFormFile> images)
        {
            foreach (var file in images)
            {
                if (file.Length > 0)
                {
                    string path = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string fullPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    _context.PropertyImages.Add(new PropertyImage
                    {
                        PropertyId = propertyId,
                        ImageUrl = "/uploads/" + fileName
                    });
                }
            }
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}