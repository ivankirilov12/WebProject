﻿using System.Collections.Generic;
using System.Linq;
using PcPartPicker.Data;
using PcPartPicker.Data.Implementations;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Services.Implementations
{
    public class MotherboardService : IMotherboardService
    {
        private UnitOfWork unitOfWork;

        public MotherboardService(PcPartPickerDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void Delete(int? id)
        {
            unitOfWork.MotherboardsRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<Motherboard> GetAllMbs(int? skip = null, int? take = null)
        {
            return unitOfWork.MotherboardsRepository.Get(a => true, null, "", skip, take);
        }

        public Motherboard GetMbById(int? id)
        {
            return unitOfWork.MotherboardsRepository.GetByID(id);
        }

        public Motherboard GetMbByModel(string model)
        {
            return unitOfWork.MotherboardsRepository.Get(a => a.Model == model).FirstOrDefault();
        }

        public IEnumerable<string> GetMbModels()
        {
            return unitOfWork.MotherboardsRepository.Get(a => true).Select(s => s.Model);
        }

        public void InsertMb(Motherboard mb)
        {
            unitOfWork.MotherboardsRepository.Insert(mb);
            unitOfWork.Save();
        }

        public void Update(Motherboard mb)
        {
            unitOfWork.MotherboardsRepository.Update(mb);
            unitOfWork.Save();
        }
    }
}
