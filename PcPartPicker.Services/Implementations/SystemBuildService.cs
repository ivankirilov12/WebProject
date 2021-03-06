﻿using System.Collections.Generic;
using System.Linq;
using PcPartPicker.Data;
using PcPartPicker.Data.Implementations;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Services.Implementations
{
    public class SystemBuildService : ISystemBuildService
    {
        private UnitOfWork unitOfWork;

        public SystemBuildService(PcPartPickerDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void Delete(int? id)
        {
            unitOfWork.SystemBuildRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<SystemBuild> GetAllSystemBuilds(int? skip = null, int? take = null)
        {
            return unitOfWork.SystemBuildRepository.Get(a => true, null, "Cpu,Case,Gpu,Motherboard,MemoryOption,StorageOption", skip, take);
        }

        public SystemBuild GetSystemBuildById(int? id)
        {
            return unitOfWork.SystemBuildRepository.Get(a => a.SystemBuildId == id, null, "Cpu,Case,Gpu,Motherboard,MemoryOption,StorageOption").FirstOrDefault();
        }

        public void InsertSystemBuild(string cpuModel, string caseModel, string gpuModel, string memoryOptionModel, 
            string motherboardModel, string storageOptionModel, decimal price, string name, string description, string imgUrl)
        {
            SystemBuild systemBuild = new SystemBuild();
            systemBuild.Cpu = unitOfWork.CpusRepository.Get(a => a.Model == cpuModel).First();
            systemBuild.Case = unitOfWork.CasesRepository.Get(a => a.Model == caseModel).First();
            systemBuild.Gpu = unitOfWork.GpusRepository.Get(a => a.Model == gpuModel).First();
            systemBuild.MemoryOption = unitOfWork.MemoryOptionsRepository.Get(a => a.Model == memoryOptionModel).First();
            systemBuild.Motherboard = unitOfWork.MotherboardsRepository.Get(a => a.Model == motherboardModel).First();
            systemBuild.StorageOption = unitOfWork.StorageOptionsRepository.Get(a => a.Model == storageOptionModel).First();
            systemBuild.Price = price;
            systemBuild.Name = name;
            systemBuild.ImgUrl = imgUrl;
            systemBuild.Description = description;

            unitOfWork.SystemBuildRepository.Insert(systemBuild);
            unitOfWork.Save();
        }

        public void Update(string cpuModel, string caseModel, string gpuModel, string memoryOptionModel, string motherboardModel, 
            string storageOptionModel, string name, string description, int id, string imgUrl)
        {
            SystemBuild systemBuild = unitOfWork.SystemBuildRepository.GetByID(id);
            systemBuild.Cpu = unitOfWork.CpusRepository.Get(a => a.Model == cpuModel).First();
            systemBuild.Case = unitOfWork.CasesRepository.Get(a => a.Model == caseModel).First();
            systemBuild.Gpu = unitOfWork.GpusRepository.Get(a => a.Model == gpuModel).First();
            systemBuild.MemoryOption = unitOfWork.MemoryOptionsRepository.Get(a => a.Model == memoryOptionModel).First();
            systemBuild.Motherboard = unitOfWork.MotherboardsRepository.Get(a => a.Model == motherboardModel).First();
            systemBuild.StorageOption = unitOfWork.StorageOptionsRepository.Get(a => a.Model == storageOptionModel).First();
            systemBuild.Price = systemBuild.Cpu.Price + systemBuild.Case.Price + systemBuild.Gpu.Price + systemBuild.MemoryOption.Price + 
                systemBuild.Motherboard.Price + systemBuild.StorageOption.Price;
            systemBuild.Name = name;
            systemBuild.Description = description;
            systemBuild.ImgUrl = imgUrl;
            unitOfWork.SystemBuildRepository.Update(systemBuild);
            unitOfWork.Save();
        }
    }
}
