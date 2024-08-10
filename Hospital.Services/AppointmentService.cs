using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.ViewModels;
using hospitals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class AppointmentService : IAppointmentService
    {
        private IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Bu yapılandırıcı metod, AppointmentService sınıfının bir örneği oluşturulurken bir IUnitOfWork örneği alır ve _unitOfWork alanına atar. IUnitOfWork, genellikle veritabanı işlemlerini yöneten bir arayüzü temsil eder.

        public void DeleteAppointment(int id)
        {
            var model = _unitOfWork.GenericRepository<Appointment>().GetById(id);
            _unitOfWork.GenericRepository<Appointment>().Delete(model);
            _unitOfWork.Save();
        }

        //Bu metot, belirli bir randevuyu siler. id parametresiyle belirtilen randevu GetById metodu ile alınır, sonra Delete ve Save metotları ile veritabanından silinir.

        public PagedResult<AppointmentViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new AppointmentViewModel();
            int totalCount;
            List<AppointmentViewModel> vmList = new List<AppointmentViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;


                var modelList = _unitOfWork.GenericRepository<Appointment>().GetAll(includeProperties: "Doctor,Patient")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Appointment>().GetAll(includeProperties: "Doctor,Patient").ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }
            var result = new PagedResult<AppointmentViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize

            };
            return result;
        }

        //Bu metot, tüm randevuları sayfalama ile getirir. Veritabanındaki tüm randevular alınır, belirli bir sayfa ve sayfa boyutuna göre filtrelenir ve AppointmentViewModel'e dönüştürülerek sayfalama sonuçlarıyla birlikte geri döner.

        public IEnumerable<AppointmentViewModel> GetAllAppoints(string docId,DateTime date)
        {
            var modelList = _unitOfWork.GenericRepository<Appointment>().GetAll(x=>x.DoctorId==docId && x.CreatedDate==date,includeProperties:"Patient").ToList();
            var vmlist = ConvertModelToViewModelList(modelList);
            return vmlist;
        }

        public AppointmentViewModel GetAppointById(int AppointId)
        {
           var model = _unitOfWork.GenericRepository<Appointment>().GetById(AppointId);
           var vm = new AppointmentViewModel(model);
            return vm;
        }

        public List<string> GetTimeList(string docId, DateTime scheduledDate)
        {
            var model = _unitOfWork.GenericRepository<Appointment>().GetAll(x=>x.DoctorId==docId && x.CreatedDate==scheduledDate).
                Select((x => x.AppointmentTime)).ToList();
            return model;
        }

        //Belirli bir doktorun ve tarihin randevularını getirir. docId ve date parametreleri ile filtreleme yapılır ve sonuçlar AppointmentViewModel'e dönüştürülerek geri döner.
        public void InsertAppointment(AppointmentViewModel appointment)
        {
            var model = new AppointmentViewModel().ConvertViewModel(appointment);
              _unitOfWork.GenericRepository<Appointment>().Add(model);
           
             _unitOfWork.Save();

            var modelUpdate = _unitOfWork.GenericRepository<Appointment>().GetById(model.Id);
            modelUpdate.Number = model.Id.ToString();
            _unitOfWork.GenericRepository<Appointment>().Update(modelUpdate);
            _unitOfWork.Save();

        }

        public async Task UpdateAppointment(string id, AppointmentViewModel appointment)
        {
            var model = new AppointmentViewModel().ConvertViewModel(appointment);
            var ModelById = _unitOfWork.GenericRepository<Appointment>().GetByIdAsync(x=>x.Patient.Id==id);
            ModelById.AppointmentTime = model.AppointmentTime;
            ModelById.Number=model.Number;
            ModelById.Patient = model.Patient;
            ModelById.Doctor = model.Doctor;
            ModelById.Status = model.Status;
            ModelById.CreatedDate=model.CreatedDate;
            ModelById.Description = model.Description;
          
        _unitOfWork.GenericRepository<Appointment>().Update(ModelById);
        _unitOfWork.Save();
        }
        private List<AppointmentViewModel> ConvertModelToViewModelList(List<Appointment> modelList)
        {
            return modelList.Select(x => new AppointmentViewModel(x)).ToList();
        }

        public bool GetPatientById(string PatientId)
        {
            var result = _unitOfWork.GenericRepository<Appointment>().GetByIdAsync(x => x.Patient.Id == PatientId);

            if (result != null)
            {
                return true;
            }
            return false;

        }
    }
}
