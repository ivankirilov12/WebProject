using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PcPartPicker.Data;
using PcPartPicker.Data.Implementations;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Interfaces;

namespace PcPartPicker.Services.Implementations
{
    public class CaseService : ICaseService
    {
        private UnitOfWork unitOfWork;

        public CaseService(ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void Delete(int? id)
        {
            unitOfWork.CasesRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<Case> GetAllCases()
        {
            return unitOfWork.CasesRepository.Get(a => true);
        }

        public Case GetCaseById(int? id)
        {
            return unitOfWork.CasesRepository.GetByID(id);
        }

        public Case GetCaseByModel(string model)
        {
            return unitOfWork.CasesRepository.Get(a => a.Model == model).ToList().FirstOrDefault();
        }

        public IEnumerable<string> GetCaseModels()
        {
            var models = unitOfWork.CasesRepository.Get(a => true).ToList().Select(s => s.Model);
            return models;
        }

        public void InsertCase(Case @case)
        {
            unitOfWork.CasesRepository.Insert(@case);
            unitOfWork.Save();
        }

        public void Update(Case @case)
        {
            unitOfWork.CasesRepository.Update(@case);
            unitOfWork.Save();
        }
    }
}
