using PcPartPicker.Models.Models;
using System.Collections.Generic;

namespace PcPartPicker.Services.Interfaces
{
    public interface IStorageOptionService
    {
        IEnumerable<string> GetStorageOptionModels();

        Storage GetStorageOptionByModel(string model);

        IEnumerable<Storage> GetAllStorageOptions();

        Storage GetStorageOptionById(int? id);

        void InsertStorageOption(Storage storageOption);

        void Update(Storage storageOption);

        void Delete(int? id);
    }
}
