using PcPartPicker.Models.Models;
using System.Collections.Generic;

namespace PcPartPicker.Services.Interfaces
{
    public interface IStorageOptionService
    {
        IEnumerable<string> GetStorageOptionModels();

        StorageOption GetStorageOptionByModel(string model);

        IEnumerable<StorageOption> GetAllStorageOptions(int? skip = null, int? take = null);

        StorageOption GetStorageOptionById(int? id);

        void InsertStorageOption(StorageOption storageOption);

        void Update(StorageOption storageOption);

        void Delete(int? id);
    }
}
