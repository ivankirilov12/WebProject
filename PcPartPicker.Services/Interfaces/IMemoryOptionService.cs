using PcPartPicker.Models.Models;
using System.Collections.Generic;

namespace PcPartPicker.Services.Interfaces
{
    public interface IMemoryOptionService
    {
        IEnumerable<string> GetMemoryOptionModels();

        Ram GetMemoryOptionByModel(string model);

        IEnumerable<Ram> GetAllMemoryOptions();

        Ram GetMemoryOptionById(int? id);

        void InsertMemoryOption(Ram memoryOption);

        void Update(Ram memoryOption);

        void Delete(int? id);
    }
}
