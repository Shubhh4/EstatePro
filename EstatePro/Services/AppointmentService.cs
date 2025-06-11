using EstatePro.Models;
using EstatePro.Repositories;

namespace EstatePro.Services
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _repository;

        public AppointmentService(AppointmentRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Appointment>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Appointment?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task AddAsync(Appointment appointment) => _repository.AddAsync(appointment);
        public Task UpdateAsync(Appointment appointment) => _repository.UpdateAsync(appointment);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}