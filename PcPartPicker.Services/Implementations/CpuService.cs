using System.Collections.Generic;
using System.Linq;
using PcPartPicker.Data;
using PcPartPicker.Data.Implementations;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Services.Implementations
{
    public class CpuService : ICpuService
    {
        private UnitOfWork unitOfWork;

        public CpuService(ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void Delete(int? id)
        {
            unitOfWork.CpusRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<Cpu> GetAllCpus()
        {
            return unitOfWork.CpusRepository.Get(a => true);
        }

        public Cpu GetCpuById(int? id)
        {
            return unitOfWork.CpusRepository.GetByID(id);
        }

        public Cpu GetCpuByModel(string model)
        {
            return unitOfWork.CpusRepository.Get(a => a.Model == model).ToList().FirstOrDefault();
        }

        public IEnumerable<string> GetCpuModels()
        {
            return unitOfWork.CpusRepository.Get(a => true).ToList().Select(s => s.Model);
        }

        public void InsertCpu(Cpu cpu)
        {
            unitOfWork.CpusRepository.Insert(cpu);
            unitOfWork.Save();
        }

        public void Update(Cpu cpu)
        {
            unitOfWork.CpusRepository.Update(cpu);
            unitOfWork.Save();
        }
    }
}
