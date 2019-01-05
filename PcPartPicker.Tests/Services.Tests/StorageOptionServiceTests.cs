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
        private ApplicationDbContext _context;
        private IStorageOptionService _sorageOptionService;
        private List<StorageOption> _testStorageOptions;

        private void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IStorageOptionService, StorageOptionService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<StorageOption>, Repository<StorageOption>>();
            IServiceProvider provider = services.BuildServiceProvider();
            _context = provider.GetService<ApplicationDbContext>();
            _sorageOptionService = provider.GetService<IStorageOptionService>();

            _testStorageOptions = GetStorageOptions();
        }

        [Fact]
        public void GetAllStorageOptions_ListOfStorageOptions_ShouldReturnAllStorageOptions()
        {
            SetUp();

            SeedData();
            var allStorageOptions = _sorageOptionService.GetAllStorageOptions();

            Assert.Equal(_testStorageOptions, allStorageOptions.ToList());
        }

        [Fact]
        public void GetAllStorageOptions_EmptyListOfStorageOptions_ShouldReturnEmptyList()
        {
            SetUp();

            var allStorageOptions = _sorageOptionService.GetAllStorageOptions();
            var expectedResult = new List<StorageOption>();

            Assert.Equal(expectedResult, allStorageOptions);
        }

        [Fact]
        public void GetStorageOptionModels_ListOfStorageOptions_ShouldReturnAllModels()
        {
            SetUp();

            SeedData();
            var sorageOptionModels = _sorageOptionService.GetStorageOptionModels().ToList();
            List<string> expectedResult = new List<string>() { "model1", "model2" };

            Assert.Equal(expectedResult, sorageOptionModels);
        }

        [Fact]
        public void GetStorageOptionModels_EmptyListOfStorageOptions_ShouldReturnNoModels()
        {
            SetUp();
            List<string> expectedResult = new List<string>();

            var sorageOptionModels = _sorageOptionService.GetStorageOptionModels().ToList();

            Assert.Equal(expectedResult, sorageOptionModels);
        }


        [Fact]
        public void GetStorageOptionByModel_ModelMatchesStorageOption_ShouldReturnCorrectStorageOption()
        {
            SetUp();

            SeedData();
            var sorageOption = _sorageOptionService.GetStorageOptionByModel("model1");

            Assert.True(sorageOption != null);
        }

        [Fact]
        public void GetStorageOptionByModel_NoMatchingModel_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var sorageOption = _sorageOptionService.GetStorageOptionByModel("model");

            Assert.True(sorageOption == null);
        }

        [Fact]
        public void GetStorageOptionById_IdMatchesStorageOption_ShouldReturnCorrectStorageOption()
        {
            SetUp();

            SeedData();
            var sorageOption = _sorageOptionService.GetStorageOptionById(1);

            Assert.True(sorageOption != null);
        }

        [Fact]
        public void GetStorageOptionById_NoMatches_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var sorageOption = _sorageOptionService.GetStorageOptionById(10);

            Assert.True(sorageOption == null);
        }

        [Fact]
        public void InsertStorageOption_InsertModel_ShouldBeInserted()
        {
            SetUp();

            var sorageOption = new StorageOption();
            _sorageOptionService.InsertStorageOption(sorageOption);

            Assert.Equal(sorageOption, _context.StorageOptions.First());
        }

        [Fact]
        public void Update_UpdateExistingStorageOption_ShouldUpdateStorageOption()
        {
            SetUp();

            SeedData();
            var sorageOption = _context.StorageOptions.First();
            sorageOption.Model = "123";
            _sorageOptionService.Update(sorageOption);

            Assert.Equal(sorageOption, _context.StorageOptions.First());
        }

        [Fact]
        public void Delete_ExistingModel_ShouldRemoveStorageOption()
        {
            SetUp();

            SeedData();
            var sorageOption = _context.StorageOptions.First();
            _sorageOptionService.Delete(sorageOption.StorageOptionId);

            Assert.DoesNotContain(sorageOption, _context.StorageOptions);
        }

        private void SeedData()
        {
            _context.AddRange(_testStorageOptions);
            _context.SaveChanges();
        }

        private List<StorageOption> GetStorageOptions()
        {
            StorageOption testStorageOptionOne = new StorageOption() { StorageOptionId = 1, Model = "model1" };
            StorageOption testStorageOptionTwo = new StorageOption() { StorageOptionId = 2, Model = "model2" };
            return new List<StorageOption> { testStorageOptionOne, testStorageOptionTwo };
        }
    }
}
