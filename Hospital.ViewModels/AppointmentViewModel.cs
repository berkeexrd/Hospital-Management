using Hospital.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string AppointmentTime { get; set; }
        public string CreatedDate { get; set; }
        public string Description { get; set; }
        public int AppointsStatus { get; set; }
        public string DoctorStatus { get; set; }
        public string DoctorId { get; set; }       
        public ApplicationUserViewModel Doctor { get; set; }
        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }




        public AppointmentViewModel()
        {

        }
        public AppointmentViewModel(Appointment model)
        {
            Id = model.Id;
            Number = model.Number;
            Type = model.Type;
            AppointmentTime = model.AppointmentTime;
            CreatedDate = model.CreatedDate.ToString("dd/MM/yyyy");
            Description = model.Description;
            AppointsStatus = Convert.ToInt32(model.Status);
            PatientId = model.PatientId;
            DoctorId=model.DoctorId;
            Patient = model.Patient;
        
        }
        public Appointment ConvertViewModel(AppointmentViewModel model)
        {
            return new Appointment
            {
                Id = model.Id,
                Number = model.Number,
                Type = model.Type,
                AppointmentTime = model.AppointmentTime,
                CreatedDate = Convert.ToDateTime(model.CreatedDate),
                Description = model.Description,
                DoctorId = model.DoctorId,
                PatientId = model.PatientId,
                Patient = model.Patient,
            
                
            };

        }
    }
    
}
