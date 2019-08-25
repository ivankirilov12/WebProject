using PcPartPicker.Models.Models;
using System.Collections.Generic;

namespace PcPartPicker.Services.Interfaces
{
    public interface ISystemBuildService
    {
        IEnumerable<SystemBuild> GetAllSystemBuilds(int? skip = null, int? take = null);

        SystemBuild GetSystemBuildById(int? id);

        void InsertSystemBuild(string cpuModel, string caseModel, string gpuModel, string memoryOptionModel, 
            string motherboardModel, string storageOptionModel, decimal price, string name, string description, string imgUrl);

        void Update(string cpuModel, string caseModel, string gpuModel, string memoryOptionModel,
            string motherboardModel, string storageOptionModel, string name, string description, int id, string imgUrl);

        void Delete(int? id);
    }
}
