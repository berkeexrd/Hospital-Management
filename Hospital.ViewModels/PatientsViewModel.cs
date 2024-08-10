using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class PatientsViewModel
    {
        public DateTime SelectedDate { get; set; }
        public string DoctorId { get; set; }
        public List<AppointmentViewModel> Appointments { get; set; }=new List<AppointmentViewModel>();


        public PatientsViewModel()
        {

        }
        public PatientsViewModel( Appointment model)
        {
            SelectedDate = model.CreatedDate;
            DoctorId = model.DoctorId;
        }
        public Appointment ConvertViewModel(PatientsViewModel model)
        {
            return new Appointment
            {
                CreatedDate = model.SelectedDate,
                DoctorId = model.DoctorId,

            };
        }










    }


    
}
