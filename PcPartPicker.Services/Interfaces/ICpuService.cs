using PcPartPicker.Models.Models;
using System.Collections.Generic;

namespace PcPartPicker.Services.Interfaces
{
    public interface ICpuService
    {
        IEnumerable<string> GetCpuModels();

        Cpu GetCpuByModel(string model);

        IEnumerable<Cpu> GetAllCpus(int? skip = null, int? take = null);

        Cpu GetCpuById(int? id);

        void InsertCpu(Cpu cpu);

        void Update(Cpu cpu);

        void Delete(int? id);
    }
}
