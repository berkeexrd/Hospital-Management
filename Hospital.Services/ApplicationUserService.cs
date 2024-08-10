using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.ViewModels;
using hospitals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PagedResult<ApplicationUserViewModel> GetAll(int PageNumber, int PageSize)
        {
            var vm = new ApplicationUserViewModel();
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
            try
            {
                int ExcludeRecords = (PageSize * PageNumber) - PageSize;


                var modelList = _unitOfWork.GenericRepository<ApplicationUser>().GetAll()
                    .Skip(ExcludeRecords).Take(PageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {

                throw;
            }
            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = PageNumber,
                PageSize = PageSize

            };
            return result;
        }

        public PagedResult<ApplicationUserViewModel> GetAllDoctor(int PageNumber, int PageSize)
        {
            var vm = new ApplicationUserViewModel();
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
            try
            {
                int ExcludeRecords = (PageSize * PageNumber) - PageSize;


                var modelList = _unitOfWork.GenericRepository<ApplicationUser>().GetAll(x=>x.IsDoctor==true)
                    .Skip(ExcludeRecords).Take(PageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<ApplicationUser>().GetAll(x=>x.IsDoctor==true).ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {

                throw;
            }
            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = PageNumber,
                PageSize = PageSize

            };
            return result;
        }

        public PagedResult<ApplicationUserViewModel> GetAllPatient(int PageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public ApplicationUserViewModel GetDoctorById(string? DoctorId)
        {
          var model = _unitOfWork.GenericRepository<ApplicationUser>().GetById(DoctorId);
          var vm = new ApplicationUserViewModel(model);
            return vm;
        }

        public PagedResult<ApplicationUserViewModel> SearchDoctor(int PageNumber, int PageSize, string Spicility = null, string City = null)
        {

            var vm = new ApplicationUserViewModel();
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
            try
            {
                int ExcludeRecords = (PageSize * PageNumber) - PageSize;


                var modelList = _unitOfWork.GenericRepository<ApplicationUser>().GetAll(x => x.IsDoctor == true && x.Specialist.Contains(Spicility))
                    .Skip(ExcludeRecords).Take(PageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<ApplicationUser>().GetAll(x => x.IsDoctor == true && x.Specialist.Contains(Spicility)).ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {

                throw;
            }
            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,    
                PageNumber = PageNumber,
                PageSize = PageSize

            };
            return result;
        }

        //Bu metot, ApplicationUser modelinden ApplicationUserViewModel'e dönüşümü sağlar. Bir liste alır ve her öğeyi dönüştürerek yeni bir liste oluşturur.
        private List<ApplicationUserViewModel> ConvertModelToViewModelList(List<ApplicationUser> modelList)
        {
            return modelList.Select(x => new ApplicationUserViewModel(x)).ToList();
        }
    }
}
