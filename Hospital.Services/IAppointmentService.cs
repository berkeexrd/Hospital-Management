using Hospital.ViewModels;
using hospitals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IAppointmentService
    {
        PagedResult<AppointmentViewModel> GetAll(int pageNumber, int pageSize);
        IEnumerable<AppointmentViewModel> GetAllAppoints(string id,DateTime selectedDate);
        bool GetPatientById(string PatientId);
        AppointmentViewModel GetAppointById(int AppointId);
        Task UpdateAppointment(string PatientId, AppointmentViewModel appointment);
        void InsertAppointment(AppointmentViewModel appointment);
        void DeleteAppointment(int id);
        List<string> GetTimeList(string docId, DateTime scheduledDate);
    }
}
