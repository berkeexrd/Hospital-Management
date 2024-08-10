using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Hospital.Web.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IApplicationUserService _userService;
        private IDoctorService _dctorService;
        private IAppointmentService _appointmentService;


        public HomeController(ILogger<HomeController> logger, IApplicationUserService userService, IDoctorService dctorService, IAppointmentService appointmentService)
        {
            _logger = logger;
            _userService = userService;
            _dctorService = dctorService;
            _appointmentService = appointmentService;
        }

        public IActionResult AllDoctors(int PageNumber = 1, int PageSize = 10)
        {
            return View(_userService.GetAllDoctor(PageNumber, PageSize));
        }

        //[HttpGet]
        //public IActionResult DoctorDetails(string doctorId)
        //{
           

        //    //get Doctor Details  using ID
        //    var doctorDetail = _userService.GetDoctorById(doctorId);
        //    // Get Patient Id using context
        //    var ClaimsIdentity = (ClaimsIdentity)User.Identity;
        //    var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


        //    var docTime = _dctorService.GetTiming(doctorId);
           
        //    AppointmentViewModel vm = new AppointmentViewModel()
        //    {
                
        //        CreatedDate = docTime.ScheduleDate,
        //        DoctorId = doctorDetail.Id,
        //        PatientId = Claims.Value,
        //        Doctor =  doctorDetail,
        //        DoctorStatus = docTime.Status.ToString()

        //    };

        //    List<SelectListItem> AppointmentTime = new List<SelectListItem>();
        //    int year = Convert.ToDateTime(docTime.ScheduleDate).Year;
        //    int month = Convert.ToDateTime(docTime.ScheduleDate).Month;
        //    int day = Convert.ToDateTime(docTime.ScheduleDate).Day;


        //    DateTime MorningStartDateTime = new DateTime(year, month, day, docTime.MorningShiftStartTime, 0, 0);
        //    DateTime MorningEndDateTime = new DateTime(year, month, day, docTime.MorningShiftEndTime, 0, 0);
        //    DateTime EveningStartDateTime = new DateTime(year, month, day, docTime.AfternoonShiftStartTime, 0, 0);
        //    DateTime EveningEndDateTime = new DateTime(year, month, day, docTime.AfternoonShiftEndTime, 0, 0);
        //    //var booking = _appointmentService.GetAllAppoints(doctorId).Select(x=>x.AppointmentTime);
            

        //    while (MorningStartDateTime < MorningEndDateTime)
        //    {
              
        //        AppointmentTime.Add(new SelectListItem
        //        {
        //            Text = MorningStartDateTime.TimeOfDay.ToString(),
        //            Value = MorningStartDateTime.TimeOfDay.ToString(),
        //            //Disabled =(booking.ToList()).Contains(MorningStartDateTime.TimeOfDay.ToString())
        //        });
        //        MorningStartDateTime = MorningStartDateTime.AddMinutes(docTime.Duration);
        //    }
        //    while (EveningStartDateTime < EveningEndDateTime)
        //    {
        //        AppointmentTime.Add(new SelectListItem
        //        {
        //            Text = EveningStartDateTime.TimeOfDay.ToString(),
        //            Value = EveningStartDateTime.TimeOfDay.ToString(),
        //        });
        //        EveningStartDateTime = EveningStartDateTime.AddMinutes(docTime.Duration);
        //    }
        //    //Get Time List Using DoctorId and date from appointment table list - selectedTimeList in string 
        //    List<String> timeFromDataBase = new List<String>();
        //    var timefromDatabase = _appointmentService.GetTimeList(doctorId, Convert.ToDateTime(docTime.ScheduleDate));
        //    List<SelectListItem> newcreatedList = new List<SelectListItem>();
           
        //    foreach (var item in AppointmentTime)
        //    {
        //    newcreatedList.Add(new SelectListItem()
        //        {
        //            Text = item.Text,
        //            Value = item.Value,
        //            Disabled = timefromDatabase.Contains(item.Value),
        //        });
        //    }
        //    ViewBag.appointmentTime = newcreatedList;
        //    // Get Time from booking table 
            
        //    return View(vm);
        //}
        //[HttpPost]
        //public IActionResult DoctorDetails(AppointmentViewModel viewModel)
        //{
           
        //        var ClaimsIdentity = (ClaimsIdentity)User.Identity;
        //        var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //         viewModel.PatientId = Claims.Value;
        //        viewModel.AppointsStatus = 1;            
        //              viewModel.Number = "";
            
        //          _appointmentService.InsertAppointment(viewModel);
           
                
        //       return RedirectToAction("AllDoctors");
        //}



        
        //[HttpGet]      
        ////Doctor Take Action on Patient's Appointment
        //public IActionResult AllAppointment()
        //{
        //    var ClaimsIdentity = (ClaimsIdentity)User.Identity;
        //    var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //    var vm = new PatientsViewModel();
        //    vm = new PatientsViewModel();
        //    vm.DoctorId = Claims.Value;
           
        //    return View(vm);
          
        //}
        //[HttpPost]
        //public IActionResult AllAppointment(PatientsViewModel vm)
        //{
        //    var ClaimsIdentity = (ClaimsIdentity)User.Identity;
        //    var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
          
          
        //    vm.DoctorId = Claims.Value;
            
        //    vm.Appointments = _appointmentService.GetAllAppoints(Claims.Value, vm.SelectedDate.Date).ToList();
          
        //    return View(vm);
          
        //}





        //[HttpGet]
        //public IActionResult EditAppointment(int Id)
        //{
        //    var viewModel = _appointmentService.GetAppointById(Id);
        //    return View(viewModel);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}