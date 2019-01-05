using System.Collections.Generic;
using System.Linq;
using PcPartPicker.Data;
using PcPartPicker.Data.Implementations;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Services.Implementations
{
    public class GpuService : IGpuService
    {
        private UnitOfWork unitOfWork;

        public GpuService(ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void Delete(int? id)
        {
            unitOfWork.GpusRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<Gpu> GetAllGpus()
        {
            return unitOfWork.GpusRepository.Get(a => true);
        }

        public Gpu GetGpuById(int? id)
        {
            return unitOfWork.GpusRepository.GetByID(id);
        }

        public Gpu GetGpuByModel(string model)
        {
            return unitOfWork.GpusRepository.Get(a => a.Model == model).FirstOrDefault();
        }

        public IEnumerable<string> GetGpuModels()
        {
            return unitOfWork.GpusRepository.Get(a => true).Select(s => s.Model);
        }

        public void InsertGpu(Gpu gpu)
        {
            unitOfWork.GpusRepository.Insert(gpu);
            unitOfWork.Save();
        }

        public void Update(Gpu gpu)
        {
            unitOfWork.GpusRepository.Update(gpu);
            unitOfWork.Save();
        }
    }
}
