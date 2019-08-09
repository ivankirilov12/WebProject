using System.Collections.Generic;
using System.Linq;
using PcPartPicker.Data;
using PcPartPicker.Data.Implementations;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Services.Implementations
{
    public class StorageOptionService : IStorageOptionService
    {
        private UnitOfWork unitOfWork;

        public StorageOptionService(PcPartPickerDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void Delete(int? id)
        {
            unitOfWork.StorageOptionsRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<StorageOption> GetAllStorageOptions(int? skip = null, int? take = null)
        {
            return unitOfWork.StorageOptionsRepository.Get(a => true, null, "", skip, take);
        }

        public StorageOption GetStorageOptionById(int? id)
        {
            return unitOfWork.StorageOptionsRepository.GetByID(id);
        }

        public StorageOption GetStorageOptionByModel(string model)
        {
            return unitOfWork.StorageOptionsRepository.Get(a => a.Model == model).FirstOrDefault();
        }

        public IEnumerable<string> GetStorageOptionModels()
        {
            return unitOfWork.StorageOptionsRepository.Get(a => true).Select(s => s.Model);
        }

        public void InsertStorageOption(StorageOption storageOption)
        {
            unitOfWork.StorageOptionsRepository.Insert(storageOption);
            unitOfWork.Save();
        }

        public void Update(StorageOption storageOption)
        {
            unitOfWork.StorageOptionsRepository.Update(storageOption);
            unitOfWork.Save();
        }
    }
}
