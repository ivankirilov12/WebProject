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
    public class StorageOptionServiceTests
    {
        private PcPartPickerDbContext _context;
        private IStorageOptionService _storageOptionService;
        private List<StorageOption> _testStorageOptions;

        private void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PcPartPickerDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IStorageOptionService, StorageOptionService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<StorageOption>, Repository<StorageOption>>();
            IServiceProvider provider = services.BuildServiceProvider();
            _context = provider.GetService<PcPartPickerDbContext>();
            _storageOptionService = provider.GetService<IStorageOptionService>();

            _testStorageOptions = GetStorageOptions();
        }

        [Fact]
        public void GetAllStorageOptions_WithListOfStorageOptions_ShouldReturnAllStorageOptions()
        {
            SetUp();

            SeedData();
            var allStorageOptions = _storageOptionService.GetAllStorageOptions();

            Assert.Equal(_testStorageOptions, allStorageOptions.ToList());
        }

        [Fact]
        public void GetAllStorageOptions_WithEmptyListOfStorageOptions_ShouldReturnEmptyList()
        {
            SetUp();

            var allStorageOptions = _storageOptionService.GetAllStorageOptions();
            var expectedResult = new List<StorageOption>();

            Assert.Equal(expectedResult, allStorageOptions);
        }

        [Fact]
        public void GetStorageOptionModels_WithListOfStorageOptions_ShouldReturnAllModels()
        {
            SetUp();

            SeedData();
            var sorageOptionModels = _storageOptionService.GetStorageOptionModels().ToList();
            List<string> expectedResult = new List<string>() { "model1", "model2" };

            Assert.Equal(expectedResult, sorageOptionModels);
        }

        [Fact]
        public void GetStorageOptionModels_WithEmptyListOfStorageOptions_ShouldReturnNoModels()
        {
            SetUp();
            List<string> expectedResult = new List<string>();

            var sorageOptionModels = _storageOptionService.GetStorageOptionModels().ToList();

            Assert.Equal(expectedResult, sorageOptionModels);
        }


        [Fact]
        public void GetStorageOptionByModel_WithModelMatchingStorageOption_ShouldReturnCorrectStorageOption()
        {
            SetUp();

            SeedData();
            var sorageOption = _storageOptionService.GetStorageOptionByModel("model1");

            Assert.True(sorageOption != null);
        }

        [Fact]
        public void GetStorageOptionByModel_WithNoMatchingModel_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var sorageOption = _storageOptionService.GetStorageOptionByModel("model");

            Assert.True(sorageOption == null);
        }

        [Fact]
        public void GetStorageOptionById_WithIdMatchingStorageOption_ShouldReturnCorrectStorageOption()
        {
            SetUp();

            SeedData();
            var sorageOption = _storageOptionService.GetStorageOptionById(1);

            Assert.True(sorageOption != null);
        }

        [Fact]
        public void GetStorageOptionById_WithNoMatches_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var sorageOption = _storageOptionService.GetStorageOptionById(10);

            Assert.True(sorageOption == null);
        }

        [Fact]
        public void InsertStorageOption_WithValidModel_ShouldBeInserted()
        {
            SetUp();

            var sorageOption = new StorageOption();
            _storageOptionService.InsertStorageOption(sorageOption);

            Assert.Equal(sorageOption, _context.StorageOptions.First());
        }

        [Fact]
        public void Update_WithExistingStorageOption_ShouldUpdateStorageOption()
        {
            SetUp();

            SeedData();
            var sorageOption = _context.StorageOptions.First();
            sorageOption.Model = "123";
            _storageOptionService.Update(sorageOption);

            Assert.Equal(sorageOption, _context.StorageOptions.First());
        }

        [Fact]
        public void Update_WithNonExistingModel_ShouldThrowError()
        {
            SetUp();

            var storage = new StorageOption();

            Assert.ThrowsAny<Exception>(() => _storageOptionService.Update(storage));
        }

        [Fact]
        public void Delete_WithExistingModel_ShouldRemoveStorageOption()
        {
            SetUp();

            SeedData();
            var sorageOption = _context.StorageOptions.First();
            _storageOptionService.Delete(sorageOption.StorageOptionId);

            Assert.DoesNotContain(sorageOption, _context.StorageOptions);
        }

        [Fact]
        public void Delete_WithNonExistingModel_ShouldThrowError()
        {
            SetUp();

            Assert.ThrowsAny<Exception>(() => _storageOptionService.Delete(1));
        }

        private void SeedData()
        {
            _context.AddRange(_testStorageOptions);
            _context.SaveChanges();
        }

        private List<StorageOption> GetStorageOptions()
        {
            StorageOption testStorageOptionOne = new StorageOption() { Model = "model1" };
            StorageOption testStorageOptionTwo = new StorageOption() { Model = "model2" };
            return new List<StorageOption> { testStorageOptionOne, testStorageOptionTwo };
        }
    }
}
