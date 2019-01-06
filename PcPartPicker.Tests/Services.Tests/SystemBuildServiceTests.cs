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
    public class SystemBuildServiceTests
    {
        private PcPartPickerDbContext _context;
        private ISystemBuildService _systemBuildService;
        private List<SystemBuild> _testSystemBuilds;
        private string cpuModel;
        private string gpuModel;
        private string caseModel;
        private string mbModel;
        private string storageOptionModel;
        private string memoryOptionModel;

        private void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PcPartPickerDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<ISystemBuildService, SystemBuildService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<SystemBuild>, Repository<SystemBuild>>();
            IServiceProvider provider = services.BuildServiceProvider();
            _context = provider.GetService<PcPartPickerDbContext>();
            _systemBuildService = provider.GetService<ISystemBuildService>();

            _testSystemBuilds = GetSystemBuilds();
        }

        [Fact]
        public void GetAllSystemBuilds_WithRecordsInContext_ShouldReturnAllBuilds()
        {
            SetUp();
            SeedData();

            var builds = _systemBuildService.GetAllSystemBuilds();

            Assert.Equal(builds, _context.SystemBuilds);
        }

        [Fact]
        public void GetAllSystemBuilds_WithNoRecordsInContext_ShouldReturnNoBuilds()
        {
            SetUp();

            var builds = _systemBuildService.GetAllSystemBuilds();

            Assert.Equal(builds, _context.SystemBuilds);
        }

        [Fact]
        public void GetSystemBuildById_WithMatchingRecord_ShouldReturnCorrectBuild()
        {
            SetUp();
            SeedData();

            var build = _systemBuildService.GetSystemBuildById(_context.SystemBuilds.First(a => a.SystemBuildId == 1).SystemBuildId);

            Assert.Equal(build, _context.SystemBuilds.First(a => a.SystemBuildId == 1));
        }

        [Fact]
        public void GetSystemBuildById_WithNoMatchingRecord_ShouldNotReturnCorrectBuild()
        {
            SetUp();
            SeedData();

            var build = _systemBuildService.GetSystemBuildById(_context.SystemBuilds.First(a => a.SystemBuildId == 1).SystemBuildId);

            Assert.False(build == _context.SystemBuilds.First(a => a.SystemBuildId == 2));
        }

        [Fact]
        public void InsertSystemBuild_WithValidData_ShouldInsertRecord()
        {
            SetUp();
            SetUpComponents();
            _systemBuildService.InsertSystemBuild(cpuModel, caseModel, gpuModel, memoryOptionModel, mbModel, storageOptionModel, 123m, "name1", "desc1");

            Assert.Contains(_context.SystemBuilds.First(a => a.SystemBuildId == 1), _context.SystemBuilds);
        }

        [Fact]
        public void InsertSystemBuild_WithInvalidData_ShouldThrowError()
        {
            SetUp();
            SetUpComponents();
            
            Assert.ThrowsAny<Exception>(() => _systemBuildService.InsertSystemBuild("", "", "", "", "", "", 123m, "name1", "desc1"));
        }

        [Fact]
        public void UpdateSystemBuild_WithValidData_ShouldUpdateRecord()
        {
            SetUp();
            SeedData();
            SetUpComponents();
            _systemBuildService.Update(cpuModel, caseModel, gpuModel, memoryOptionModel, mbModel, storageOptionModel, "name2", "desc2", 1);

            Assert.Equal("name2", _context.SystemBuilds.First(a => a.SystemBuildId == 1).Name);
        }

        [Fact]
        public void UpdateSystemBuild_WithInvalidData_ShouldThrowError()
        {
            SetUp();
            SeedData();
            SetUpComponents();

            Assert.ThrowsAny<Exception>(() => _systemBuildService.Update("", "", "", "", "", "", "", "", 10));
        }

        [Fact]
        public void Delete_WithExistingSystemBuild_ShouldDeleteRecord()
        {
            SetUp();
            SeedData();

            var build = _context.SystemBuilds.First();
            _systemBuildService.Delete(build.SystemBuildId);

            Assert.DoesNotContain(build, _context.SystemBuilds);
        }

        [Fact]
        public void Delete_WithNonExistingModel_ShouldThrowError()
        {
            SetUp();
            SeedData();

            Assert.ThrowsAny<Exception>(() => _systemBuildService.Delete(10));
        }

        private void SetUpComponents()
        {
            _context.Cpus.Add(new Cpu() { Model = "cpuModel"});
            _context.Gpus.Add(new Gpu() { Model = "gpuModel"});
            _context.Cases.Add(new Case() { Model = "caseModel"});
            _context.Motherboards.Add(new Motherboard() { Model = "mbModel"});
            _context.MemoryOptions.Add(new MemoryOption() { Model = "memoryOptionModel"});
            _context.StorageOptions.Add(new StorageOption() { Model = "storageOptionModel"});
            _context.SaveChanges();

            cpuModel = "cpuModel";
            gpuModel = "gpuModel";
            caseModel = "caseModel";
            mbModel = "mbModel";
            memoryOptionModel = "memoryOptionModel";
            storageOptionModel = "storageOptionModel";
        }

        private void SeedData()
        {
            _context.SystemBuilds.AddRange(_testSystemBuilds);
            _context.SaveChanges();
        }

        private List<SystemBuild> GetSystemBuilds()
        {
            var testBuildOne = new SystemBuild() { Name = "name1"};
            var testBuildTwo = new SystemBuild() { Name = "name2" };
            return new List<SystemBuild>() { testBuildOne, testBuildTwo };
        }
    }
}
