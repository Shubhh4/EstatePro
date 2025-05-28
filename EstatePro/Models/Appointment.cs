using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePro.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    }


}
