using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.ViewModels;
using hospitals.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class DoctorService :IDoctorService
    {
        private IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddTiming(TimingViewModel timing)
        {
            var model = new TimingViewModel().ConvertViewModel(timing);
            _unitOfWork.GenericRepository<Timing>().Add(model);
            _unitOfWork.Save();
        }

        public void DeleteTiming(int TimingId)
        {
            var model = _unitOfWork.GenericRepository<Timing>().GetById(TimingId);
            _unitOfWork.GenericRepository<Timing>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new TimingViewModel();
            int totalCount;
            List<TimingViewModel> vmList = new List<TimingViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;


                var modelList = _unitOfWork.GenericRepository<Timing>().GetAll(includeProperties: "Doctor")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Timing>().GetAll(includeProperties: "Doctor").ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }
            var result = new PagedResult<TimingViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize

            };
            return result;
        }

        public IEnumerable<TimingViewModel> GetAll()
        {
            var TimingList = _unitOfWork.GenericRepository<Timing>().GetAll().ToList();
            var vmList = ConvertModelToViewModelList(TimingList);
            return vmList;
        }


        public TimingViewModel GetTiming(string DocId)
        {
            var result = _unitOfWork.GenericRepository<Timing>().GetByIdAsync(x => x.DoctorId == DocId,includeProperties:"Doctor");
            var vm = new TimingViewModel(result);
            return vm;
        }

        public bool GetTimingById(string DoctorId)
        {
            var result = _unitOfWork.GenericRepository<Timing>().GetByIdAsync(x => x.DoctorId == DoctorId);
           
            if (result!=null)
            {
                return true;
            }
            return false;
           
        }

        

        public async Task UpdateTiming(string id, TimingViewModel timing)
        {
            var model = new TimingViewModel().ConvertViewModel(timing);
            var ModelById = _unitOfWork.GenericRepository<Timing>().GetByIdAsync(x => x.DoctorId == id);
           
            ModelById.Status = timing.Status;
            ModelById.Duration = timing.Duration;
            ModelById.MorningShiftStartTime = timing.MorningShiftStartTime;
            ModelById.MorningShiftEndTime = timing.MorningShiftEndTime;
            ModelById.AfternoonShiftStartTime = timing.AfternoonShiftStartTime;
            ModelById.AfternoonShiftEndTime = timing.AfternoonShiftEndTime;
            ModelById.Date = Convert.ToDateTime(timing.ScheduleDate);
            _unitOfWork.GenericRepository<Timing>().Update(ModelById);
            _unitOfWork.Save();
           
        }


        private List<TimingViewModel> ConvertModelToViewModelList(List<Timing> modelList)
        {
            return modelList.Select(x => new TimingViewModel(x)).ToList();
        }
    }
}
