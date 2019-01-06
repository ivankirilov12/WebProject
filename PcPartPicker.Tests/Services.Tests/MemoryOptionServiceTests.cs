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
    public class MemoryOptionServiceTests
    {
        private PcPartPickerDbContext _context;
        private IMemoryOptionService _memoryOptionService;
        private List<MemoryOption> _testMemoryOptions;

        private void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PcPartPickerDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IMemoryOptionService, MemoryOptionService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<MemoryOption>, Repository<MemoryOption>>();
            IServiceProvider provider = services.BuildServiceProvider();
            _context = provider.GetService<PcPartPickerDbContext>();
            _memoryOptionService = provider.GetService<IMemoryOptionService>();

            _testMemoryOptions = GetMemoryOptions();
        }

        [Fact]
        public void GetAllMemoryOptions_WithListOfMemoryOptions_ShouldReturnAllMemoryOptions()
        {
            SetUp();

            SeedData();
            var allMemoryOptions = _memoryOptionService.GetAllMemoryOptions();

            Assert.Equal(_testMemoryOptions, allMemoryOptions.ToList());
        }

        [Fact]
        public void GetAllMemoryOptions_WithEmptyListOfMemoryOptions_ShouldReturnEmptyList()
        {
            SetUp();

            var allMemoryOptions = _memoryOptionService.GetAllMemoryOptions();
            var expectedResult = new List<MemoryOption>();

            Assert.Equal(expectedResult, allMemoryOptions);
        }

        [Fact]
        public void GetMemoryOptionModels_WithListOfMemoryOptions_ShouldReturnAllModels()
        {
            SetUp();

            SeedData();
            var memoryOptionModels = _memoryOptionService.GetMemoryOptionModels().ToList();
            List<string> expectedResult = new List<string>() { "model1", "model2" };

            Assert.Equal(expectedResult, memoryOptionModels);
        }

        [Fact]
        public void GetMemoryOptionModels_WithEmptyListOfMemoryOptions_ShouldReturnNoModels()
        {
            SetUp();
            List<string> expectedResult = new List<string>();

            var memoryOptionModels = _memoryOptionService.GetMemoryOptionModels().ToList();

            Assert.Equal(expectedResult, memoryOptionModels);
        }


        [Fact]
        public void GetMemoryOptionByModel_WithModelMatchingMemoryOption_ShouldReturnCorrectMemoryOption()
        {
            SetUp();

            SeedData();
            var memoryOption = _memoryOptionService.GetMemoryOptionByModel("model1");

            Assert.True(memoryOption != null);
        }

        [Fact]
        public void GetMemoryOptionByModel_WithNoMatchingModel_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var memoryOption = _memoryOptionService.GetMemoryOptionByModel("model");

            Assert.True(memoryOption == null);
        }

        [Fact]
        public void GetMemoryOptionById_WithIdMatchingMemoryOption_ShouldReturnCorrectMemoryOption()
        {
            SetUp();

            SeedData();
            var memoryOption = _memoryOptionService.GetMemoryOptionById(1);

            Assert.True(memoryOption != null);
        }

        [Fact]
        public void GetMemoryOptionById_WithNoMatches_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var memoryOption = _memoryOptionService.GetMemoryOptionById(10);

            Assert.True(memoryOption == null);
        }

        [Fact]
        public void InsertMemoryOption_WithValidModel_ShouldBeInserted()
        {
            SetUp();

            var memoryOption = new MemoryOption();
            _memoryOptionService.InsertMemoryOption(memoryOption);

            Assert.Equal(memoryOption, _context.MemoryOptions.First());
        }

        [Fact]
        public void Update_WithExistingMemoryOption_ShouldUpdateMemoryOption()
        {
            SetUp();

            SeedData();
            var memoryOption = _context.MemoryOptions.First();
            memoryOption.Model = "123";
            _memoryOptionService.Update(memoryOption);

            Assert.Equal(memoryOption, _context.MemoryOptions.First());
        }

        [Fact]
        public void Update_WithNonExistingMemoryOption_ShouldThrowError()
        {
            SetUp();

            var mo = new MemoryOption();

            Assert.ThrowsAny<Exception>(() => _memoryOptionService.Update(mo));
        }

        [Fact]
        public void Delete_WithExistingModel_ShouldRemoveMemoryOption()
        {
            SetUp();

            SeedData();
            var memoryOption = _context.MemoryOptions.First();
            _memoryOptionService.Delete(memoryOption.MemoryOptionId);

            Assert.DoesNotContain(memoryOption, _context.MemoryOptions);
        }

        [Fact]
        public void Delete_WithNonExistingModel_ShouldThrowError()
        {
            SetUp();

            Assert.ThrowsAny<Exception>(() => _memoryOptionService.Delete(1));
        }

        private void SeedData()
        {
            _context.AddRange(_testMemoryOptions);
            _context.SaveChanges();
        }

        private List<MemoryOption> GetMemoryOptions()
        {
            MemoryOption testMemoryOptionOne = new MemoryOption() { Model = "model1" };
            MemoryOption testMemoryOptionTwo = new MemoryOption() { Model = "model2" };
            return new List<MemoryOption> { testMemoryOptionOne, testMemoryOptionTwo };
        }
    }
}
