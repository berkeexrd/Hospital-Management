using Hospital.ViewModels;
using hospitals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IDoctorService
    {

        PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize);
        IEnumerable<TimingViewModel> GetAll();
      
        bool GetTimingById(string DoctorId);
        TimingViewModel GetTiming(string DocId);
        Task UpdateTiming(string doctorId, TimingViewModel timing);
        void AddTiming(TimingViewModel timing);
        void DeleteTiming(int TimingId);

    }
}
