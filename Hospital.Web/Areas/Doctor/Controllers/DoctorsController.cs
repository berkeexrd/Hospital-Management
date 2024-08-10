using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Hospital.Web.Areas.Doctor.Controllers
{
    [Area("Doctor")]
   
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;


        public DoctorsController(IDoctorService doctorService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _appointmentService = appointmentService;
        }

        public IActionResult OPDStatus(int pageNumber = 1, int pageSize = 10)
        {
            return View(_doctorService.GetAll(pageNumber, pageSize));
        }


        [HttpGet]
        public IActionResult AddOPDTiming()
        {
            Timing timing = new Timing();
            List<SelectListItem> morningShiftStart = new List<SelectListItem>();
            List<SelectListItem> morningShiftEnd = new List<SelectListItem>();
            List<SelectListItem> AfternoonShiftStart = new List<SelectListItem>();
            List<SelectListItem> AfternoonShiftEnd = new List<SelectListItem>();

            for (int i = 1; i <= 11; i++)
            {
                morningShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            }
            for (int i = 1; i <= 13; i++)
            {
                morningShiftEnd.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            }
            for (int i = 13; i <= 16; i++)
            {
                AfternoonShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            }
            for (int i = 13; i <= 18; i++)
            {
                AfternoonShiftEnd.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            }

            ViewBag.morningStart = new SelectList(morningShiftStart, "Value", "Text");
            ViewBag.morningEnd = new SelectList(morningShiftEnd, "Value", "Text");
            ViewBag.evenStart = new SelectList(AfternoonShiftStart, "Value", "Text");
            ViewBag.evenEnd = new SelectList(AfternoonShiftEnd, "Value", "Text");
            TimingViewModel vm =  new TimingViewModel();
            vm.ScheduleDate =DateTime.Now.ToString("dd/MM/yyyy");
            vm.ScheduleDate = DateOnly.FromDateTime(Convert.ToDateTime(vm.ScheduleDate).AddDays(1)).ToString("dd/MM/yyyy");
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddOPDTiming(TimingViewModel vm)
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (Claims != null)
            {

                vm.DoctorId = Claims.Value;
                if (_doctorService.GetTimingById(Claims.Value))
                {
                    _doctorService.UpdateTiming(Claims.Value, vm);
                }
                else
                {
                    _doctorService.AddTiming(vm);
                }
                
            }

            return RedirectToAction("OPDStatus","Doctors");
        }
       
        public IActionResult Delete(int id)
        {
            _doctorService.DeleteTiming(id);
            return RedirectToAction("Index");
        }




        [HttpGet]
        //Doctor Take Action on Patient's Appointment
        public IActionResult AllAppointments()
        {
            
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var vm = new PatientsViewModel();
            vm = new PatientsViewModel();
            vm.DoctorId = Claims.Value;
           
                return View(vm);
        }
        [HttpPost]
        public IActionResult AllAppointments(PatientsViewModel vm)
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            vm.DoctorId = Claims.Value;

            vm.Appointments = _appointmentService.GetAllAppoints(Claims.Value, vm.SelectedDate.Date).ToList();

            return View(vm);

        }


    }
}
