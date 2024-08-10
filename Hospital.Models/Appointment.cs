using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Appointment
    {
      
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string AppointmentTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public ApplicationUser Doctor { get; set; }
        public ApplicationUser Patient { get; set; }              
    }

}