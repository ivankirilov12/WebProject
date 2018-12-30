using PcPartPicker.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PcPartPicker.Services.Interfaces
{
    public interface ICaseService
    {
        IEnumerable<string> GetCaseModels();

        Case GetCaseByModel(string model);

        IEnumerable<Case> GetAllCases();

        Case GetCaseById(int? id);

        void Update(Case @case);

        void Delete(int? id);

        void InsertCase(Case @case);
    }
}
