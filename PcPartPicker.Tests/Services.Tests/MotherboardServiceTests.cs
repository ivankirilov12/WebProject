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
    public class MotherboardServiceTests
    {
        private ApplicationDbContext _context;
        private IMotherboardService _motherboardService;
        private List<Motherboard> _testMotherboards;

        private void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IMotherboardService, MotherboardService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Motherboard>, Repository<Motherboard>>();
            IServiceProvider provider = services.BuildServiceProvider();
            _context = provider.GetService<ApplicationDbContext>();
            _motherboardService = provider.GetService<IMotherboardService>();

            _testMotherboards = GetMotherboards();
        }

        [Fact]
        public void GetAllMotherboards_ListOfMotherboards_ShouldReturnAllMotherboards()
        {
            SetUp();

            SeedData();
            var allMotherboards = _motherboardService.GetAllMbs();

            Assert.Equal(_testMotherboards, allMotherboards.ToList());
        }

        [Fact]
        public void GetAllMotherboards_EmptyListOfMotherboards_ShouldReturnEmptyList()
        {
            SetUp();

            var allMotherboards = _motherboardService.GetAllMbs();
            var expectedResult = new List<Motherboard>();

            Assert.Equal(expectedResult, allMotherboards);
        }

        [Fact]
        public void GetMotherboardModels_ListOfMotherboards_ShouldReturnAllModels()
        {
            SetUp();

            SeedData();
            var motherboardModels = _motherboardService.GetMbModels().ToList();
            List<string> expectedResult = new List<string>() { "model1", "model2" };

            Assert.Equal(expectedResult, motherboardModels);
        }

        [Fact]
        public void GetMotherboardModels_EmptyListOfMotherboards_ShouldReturnNoModels()
        {
            SetUp();
            List<string> expectedResult = new List<string>();

            var motherboardModels = _motherboardService.GetMbModels().ToList();

            Assert.Equal(expectedResult, motherboardModels);
        }


        [Fact]
        public void GetMotherboardByModel_ModelMatchesMotherboard_ShouldReturnCorrectMotherboard()
        {
            SetUp();

            SeedData();
            var motherboard = _motherboardService.GetMbByModel("model1");

            Assert.True(motherboard != null);
        }

        [Fact]
        public void GetMotherboardByModel_NoMatchingModel_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var motherboard = _motherboardService.GetMbByModel("model");

            Assert.True(motherboard == null);
        }

        [Fact]
        public void GetMotherboardById_IdMatchesMotherboard_ShouldReturnCorrectMotherboard()
        {
            SetUp();

            SeedData();
            var motherboard = _motherboardService.GetMbById(1);

            Assert.True(motherboard != null);
        }

        [Fact]
        public void GetMotherboardById_NoMatches_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var motherboard = _motherboardService.GetMbById(10);

            Assert.True(motherboard == null);
        }

        [Fact]
        public void InsertMotherboard_InsertModel_ShouldBeInserted()
        {
            SetUp();

            var motherboard = new Motherboard();
            _motherboardService.InsertMb(motherboard);

            Assert.Equal(motherboard, _context.Motherboards.First());
        }

        [Fact]
        public void Update_UpdateExistingMotherboard_ShouldUpdateMotherboard()
        {
            SetUp();

            SeedData();
            var motherboard = _context.Motherboards.First();
            motherboard.Model = "123";
            _motherboardService.Update(motherboard);

            Assert.Equal(motherboard, _context.Motherboards.First());
        }

        [Fact]
        public void Delete_ExistingModel_ShouldRemoveMotherboard()
        {
            SetUp();

            SeedData();
            var motherboard = _context.Motherboards.First();
            _motherboardService.Delete(motherboard.MotherboardId);

            Assert.DoesNotContain(motherboard, _context.Motherboards);
        }

        private void SeedData()
        {
            _context.AddRange(_testMotherboards);
            _context.SaveChanges();
        }

        private List<Motherboard> GetMotherboards()
        {
            Motherboard testMotherboardOne = new Motherboard() { MotherboardId = 1, Model = "model1" };
            Motherboard testMotherboardTwo = new Motherboard() { MotherboardId = 2, Model = "model2" };
            return new List<Motherboard> { testMotherboardOne, testMotherboardTwo };
        }
    }
}
