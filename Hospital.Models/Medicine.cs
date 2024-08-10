using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        //[NotMapped]
      //  public ICollection<MedicineReport> MedicineReport { get; set; }
        //[NotMapped]
       // public ICollection<PrescribedMedicine> PrescribedMedicine { get; set; }

    }
}
