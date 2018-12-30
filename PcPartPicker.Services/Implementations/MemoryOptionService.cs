﻿using System.Collections.Generic;
using System.Linq;
using PcPartPicker.Data;
using PcPartPicker.Data.Implementations;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Services.Implementations
{
    public class MemoryOptionService : IMemoryOptionService
    {
        private UnitOfWork unitOfWork;

        public MemoryOptionService(ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void Delete(int? id)
        {
            unitOfWork.MemoryOptionsRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<Ram> GetAllMemoryOptions()
        {
            return unitOfWork.MemoryOptionsRepository.Get(a => true);
        }

        public Ram GetMemoryOptionById(int? id)
        {
            return unitOfWork.MemoryOptionsRepository.GetByID(id);
        }

        public Ram GetMemoryOptionByModel(string model)
        {
            return unitOfWork.MemoryOptionsRepository.Get(a => a.Model == model).First();
        }

        public IEnumerable<string> GetMemoryOptionModels()
        {
            return unitOfWork.MemoryOptionsRepository.Get(a => true).Select(s => s.Model);
        }

        public void InsertMemoryOption(Ram memoryOption)
        {
            unitOfWork.MemoryOptionsRepository.Insert(memoryOption);
            unitOfWork.Save();
        }

        public void Update(Ram memoryOption)
        {
            unitOfWork.MemoryOptionsRepository.Update(memoryOption);
            unitOfWork.Save();
        }
    }
}