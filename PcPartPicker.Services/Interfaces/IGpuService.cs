using PcPartPicker.Models.Models;
using System.Collections.Generic;

namespace PcPartPicker.Services.Interfaces
{
    public interface IGpuService
    {
        IEnumerable<string> GetGpuModels();

        Gpu GetGpuByModel(string model);

        IEnumerable<Gpu> GetAllGpus(int? skip = null, int? take = null);

        Gpu GetGpuById(int? id);

        void InsertGpu(Gpu gpu);

        void Update(Gpu gpu);

        void Delete(int? id);
    }
}
