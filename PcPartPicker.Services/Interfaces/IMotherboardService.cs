using PcPartPicker.Models.Models;
using System.Collections.Generic;

namespace PcPartPicker.Services.Interfaces
{
    public interface IMotherboardService
    {
        IEnumerable<string> GetMbModels();

        Motherboard GetMbByModel(string model);

        IEnumerable<Motherboard> GetAllMbs();

        Motherboard GetMbById(int? id);

        void InsertMb(Motherboard mb);

        void Update(Motherboard mb);

        void Delete(int? id);
    }
}
