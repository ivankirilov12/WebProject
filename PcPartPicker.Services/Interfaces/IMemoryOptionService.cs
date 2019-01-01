using PcPartPicker.Models.Models;
using System.Collections.Generic;

namespace PcPartPicker.Services.Interfaces
{
    public interface IMemoryOptionService
    {
        IEnumerable<string> GetMemoryOptionModels();

        MemoryOption GetMemoryOptionByModel(string model);

        IEnumerable<MemoryOption> GetAllMemoryOptions();

        MemoryOption GetMemoryOptionById(int? id);

        void InsertMemoryOption(MemoryOption memoryOption);

        void Update(MemoryOption memoryOption);

        void Delete(int? id);
    }
}
