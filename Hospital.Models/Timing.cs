using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
   
    public class Timing
    {
        public int Id { get; set; }
      
        public string DoctorId { get; set; }
        public ApplicationUser Doctor { get; set; }
        public DateTime Date { get; set; }
        public int MorningShiftStartTime { get; set; }
        public int MorningShiftEndTime { get; set; }
        public int AfternoonShiftStartTime { get; set; }
        public int AfternoonShiftEndTime { get; set; }
        public int Duration { get; set; }
        public Status Status { get; set; }

    }
    
}

namespace Hospital.Models
{
    public enum Status
    {
        Available=0,Pending=1,Confirm=2
    }
}