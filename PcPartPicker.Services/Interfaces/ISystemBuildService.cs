using PcPartPicker.Models.Models;
using System.Collections.Generic;

namespace PcPartPicker.Services.Interfaces
{
    public interface ISystemBuildService
    {
        IEnumerable<SystemBuild> GetAllSystemBuilds();

        SystemBuild GetSystemBuildById(int? id);

        void InsertSystemBuild(string cpuModel, string caseModel, string gpuModel, string memoryOptionModel, 
            string motherboardModel, string storageOptionModel, decimal price, string name, string description);

        void Update(string cpuModel, string caseModel, string gpuModel, string memoryOptionModel,
            string motherboardModel, string storageOptionModel, string name, string description, int id);

        void Delete(int? id);
    }
}
