using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentsController : Controller
    {
        private IAppointmentService _appointmentService;
        private IDoctorService _doctorService;
        private IApplicationUserService _userService;
        public AppointmentsController(IAppointmentService appointmentService, IDoctorService dctorService, IApplicationUserService userService)
        {
            _appointmentService = appointmentService;
            _doctorService = dctorService;
            _userService = userService;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_appointmentService.GetAll(pageNumber, pageSize));
        }

       

        [HttpGet]
        //DoctorAppointment
        public IActionResult CreateAppointment()
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var docTime = _doctorService.GetTiming(Claims.Value);
              
            Appointment appointment = new Appointment();
            List<SelectListItem> AppointmentTime = new List<SelectListItem>();
            int year = Convert.ToDateTime(docTime.ScheduleDate).Year;
            int month = Convert.ToDateTime(docTime.ScheduleDate).Month;
            int day = Convert.ToDateTime(docTime.ScheduleDate).Day;
          

            DateTime MorningStartDateTime = new DateTime(year,month,day,docTime.MorningShiftStartTime,0,0);
            DateTime MorningEndDateTime = new DateTime(year, month, day, docTime.MorningShiftEndTime,0,0);
            DateTime EveningStartDateTime = new DateTime(year, month, day, docTime.AfternoonShiftStartTime,0,0);
            DateTime EveningEndDateTime = new DateTime(year, month, day, docTime.AfternoonShiftEndTime,0,0);
            /*
             * booking =  _context.booking.where(x=>x.doctid).getalll();
             * 
             */

            while (MorningStartDateTime < MorningEndDateTime) { 
                AppointmentTime.Add(new SelectListItem
                {
                    Text = MorningStartDateTime.TimeOfDay + " - " + MorningStartDateTime.AddMinutes(docTime.Duration).TimeOfDay,
                    Value = MorningStartDateTime.TimeOfDay + " - " + MorningStartDateTime.AddMinutes(docTime.Duration).TimeOfDay,
                    //disabled =  booking.contains(MorningStartDateTime.TimeOfDay + " - " + MorningStartDateTime.AddMinutes(docTime.Duration).TimeOfDay) && booking.contains(doctime.date)

                }

                );
                MorningStartDateTime = MorningStartDateTime.AddMinutes(docTime.Duration);

                }
            while (EveningStartDateTime < EveningEndDateTime)
            {
                AppointmentTime.Add(new SelectListItem
                {
                    Text = EveningStartDateTime.TimeOfDay + " - " + EveningStartDateTime.AddMinutes(docTime.Duration).TimeOfDay,
                    Value = EveningStartDateTime.TimeOfDay + " - " + EveningStartDateTime.AddMinutes(docTime.Duration).TimeOfDay,
                    //disabled =  booking.contains(MorningStartDateTime.TimeOfDay + " - " + MorningStartDateTime.AddMinutes(docTime.Duration).TimeOfDay) && booking.contains(doctime.date)

                }

                );
                EveningStartDateTime = EveningStartDateTime.AddMinutes(docTime.Duration);

            }
            ViewBag.Booking = new SelectList(AppointmentTime,"Value","Text");
            return View();
        }

        [HttpPost]
        public IActionResult CreateAppointment(AppointmentViewModel vm)
        {
          
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _appointmentService.DeleteAppointment(id);
            return RedirectToAction("Index");
        }
       
    }
}
