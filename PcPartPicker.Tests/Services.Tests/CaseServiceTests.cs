using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PcPartPicker.Data;
using PcPartPicker.Data.Implementations;
using PcPartPicker.Data.Interfaces;
using PcPartPicker.Models.Models;
using PcPartPicker.Services.Implementations;
using PcPartPicker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PcPartPicker.Services.Tests.Services.Tests
{
    public class CaseServiceTests
    {
        private PcPartPickerDbContext _context;
        private ICaseService _caseService;
        private List<Case> _testCases;

        private void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PcPartPickerDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<ICaseService, CaseService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Case>, Repository<Case>>();
            IServiceProvider provider = services.BuildServiceProvider();
            _context = provider.GetService<PcPartPickerDbContext>();
            _caseService = provider.GetService<ICaseService>();

            _testCases = GetCases();
        }

        [Fact]
        public void GetAllCases_WithListOfCases_ShouldReturnAllCases()
        {
            SetUp();

            SeedData();
            var allCases = _caseService.GetAllCases();

            Assert.Equal(_testCases, allCases.ToList());
        }

        [Fact]
        public void GetAllCases_WithEmptyListOfCases_ShouldReturnEmptyList()
        {
            SetUp();

            var allCases = _caseService.GetAllCases();
            var expectedResult = new List<Case>();

            Assert.Equal(expectedResult, allCases);
        }

        [Fact]
        public void GetCaseModels_WithListOfCases_ShouldReturnAllModels()
        {
            SetUp();

            SeedData();
            var caseModels = _caseService.GetCaseModels().ToList();
            List<string> expectedResult = new List<string>() { "model1", "model2" };

            Assert.Equal(expectedResult, caseModels);
        }

        [Fact]
        public void GetCaseModels_WithEmptyListOfCases_ShouldReturnNoModels()
        {
            SetUp();
            List<string> expectedResult = new List<string>();

            var caseModels = _caseService.GetCaseModels().ToList();

            Assert.Equal(expectedResult, caseModels);
        }


        [Fact]
        public void GetCaseByModel_WithModelMatchingCase_ShouldReturnCorrectCase()
        {
            SetUp();

            SeedData();
            var @case = _caseService.GetCaseByModel("model1");

            Assert.True(@case != null);
        }

        [Fact]
        public void GetCaseByModel_WithNoMatchingModel_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var @case = _caseService.GetCaseByModel("model");

            Assert.True(@case == null);
        }

        [Fact]
        public void GetCaseById_WithIdMatchingCase_ShouldReturnCorrectCase()
        {
            SetUp();

            SeedData();
            var @case = _caseService.GetCaseById(1);

            Assert.True(@case != null);
        }

        [Fact]
        public void GetCaseById_WithNoMatches_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var @case = _caseService.GetCaseById(10);

            Assert.True(@case == null);
        }

        [Fact]
        public void InsertCase_WithValidModel_ShouldBeInserted()
        {
            SetUp();

            var @case = new Case();
            _caseService.InsertCase(@case);

            Assert.Equal(@case, _context.Cases.First());
        }

        [Fact]
        public void Update_WithExistingCase_ShouldUpdateCase()
        {
            SetUp();

            SeedData();
            var @case = _context.Cases.First();
            @case.Model = "123";
            _caseService.Update(@case);

            Assert.Equal(@case, _context.Cases.First());
        }  

        [Fact]
        public void Update_WithNonExistingCase_ShouldThrowError()
        {
            SetUp();

            var @case = new Case();

            Assert.ThrowsAny<Exception>(() => _caseService.Update(@case));
        }

        [Fact]
        public void Delete_WithExistingModel_ShouldRemoveCase()
        {
            SetUp();

            SeedData();
            var @case = _context.Cases.First();
            _caseService.Delete(@case.CaseId);

            Assert.DoesNotContain(@case, _context.Cases);
        }

        [Fact]
        public void Delete_WithNonExistingModel_ShouldThrowError()
        {
            SetUp();

            var @case = new Case();

            Assert.ThrowsAny<Exception>(() => _caseService.Delete(@case.CaseId));
        }

        private void SeedData()
        {
            _context.AddRange(_testCases);
            _context.SaveChanges();
        }

        private List<Case> GetCases()
        {
            Case testCaseOne = new Case() { CaseId = 1, Model = "model1" };
            Case testCaseTwo = new Case() { CaseId = 2, Model = "model2" };
            return new List<Case> { testCaseOne, testCaseTwo };
        }
    }
}
