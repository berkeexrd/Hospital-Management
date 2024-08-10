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
    public class SupplierService : ISupplierService
    {
        private IUnitOfWork _unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteSupplier(int id)
        {
            var model = _unitOfWork.GenericRepository<Supplier>().GetById(id);
            _unitOfWork.GenericRepository<Supplier>().Delete(model);
            _unitOfWork.Save();
        }        
        public PagedResult<SupplierViewModel> GetAll(int pageNumber, int pageSize)
        {

            var vm = new SupplierViewModel();
            int totalCount;
            List<SupplierViewModel> vmList = new List<SupplierViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;


                var modelList = _unitOfWork.GenericRepository<Supplier>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Supplier>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {

                throw;
            }
            var result = new PagedResult<SupplierViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize

            };
            return result;
        }

        public IEnumerable<SupplierViewModel> GetAll()
        {
            var SupplierList = _unitOfWork.GenericRepository<Supplier>().GetAll().ToList();
            var vmList = ConvertModelToViewModelList(SupplierList);
            return vmList;
        }        
        public SupplierViewModel GetSupplierById(int SuppplierId)
        {
            var model = _unitOfWork.GenericRepository<Supplier>().GetById(SuppplierId);
            var vm = new SupplierViewModel(model);
            return vm;
        }
         public void InsertSupplier(SupplierViewModel Supplier)
        {
            var model = new SupplierViewModel().ConvertViewModel(Supplier);
            _unitOfWork.GenericRepository<Supplier>().Add(model);
            _unitOfWork.Save();
        }
        
        public void UpdateSupplier(SupplierViewModel Supplier)
        {
            var model = new SupplierViewModel().ConvertViewModel(Supplier);
            var ModelById = _unitOfWork.GenericRepository<Supplier>().GetById(model.Id);
            ModelById.Address = model.Address;
            ModelById.Company = model.Company;
            ModelById.Email=model.Email;
            ModelById.Phone = model.Phone;

            _unitOfWork.GenericRepository<Supplier>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<SupplierViewModel> ConvertModelToViewModelList(List<Supplier> modelList)
        {
            return modelList.Select(x => new SupplierViewModel(x)).ToList();
        }

       
    }
}
