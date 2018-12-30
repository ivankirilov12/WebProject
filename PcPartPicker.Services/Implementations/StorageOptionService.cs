﻿using System.Collections.Generic;
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

        public StorageOptionService(ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void Delete(int? id)
        {
            unitOfWork.StorageOptionsRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<Storage> GetAllStorageOptions()
        {
            return unitOfWork.StorageOptionsRepository.Get(a => true);
        }

        public Storage GetStorageOptionById(int? id)
        {
            return unitOfWork.StorageOptionsRepository.GetByID(id);
        }

        public Storage GetStorageOptionByModel(string model)
        {
            return unitOfWork.StorageOptionsRepository.Get(a => a.Model == model).First();
        }

        public IEnumerable<string> GetStorageOptionModels()
        {
            return unitOfWork.StorageOptionsRepository.Get(a => true).Select(s => s.Model);
        }

        public void InsertStorageOption(Storage storageOption)
        {
            unitOfWork.StorageOptionsRepository.Insert(storageOption);
            unitOfWork.Save();
        }

        public void Update(Storage storageOption)
        {
            unitOfWork.StorageOptionsRepository.Update(storageOption);
            unitOfWork.Save();
        }
    }
}