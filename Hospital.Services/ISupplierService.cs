using Hospital.ViewModels;
using hospitals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface ISupplierService
    {
        PagedResult<SupplierViewModel> GetAll(int pageNumber, int pageSize);
        IEnumerable<SupplierViewModel> GetAll();
        SupplierViewModel GetSupplierById(int SupplierId);
        void UpdateSupplier(SupplierViewModel Supplier);
        void InsertSupplier(SupplierViewModel Supplier);
        void DeleteSupplier(int id);
    }
}
