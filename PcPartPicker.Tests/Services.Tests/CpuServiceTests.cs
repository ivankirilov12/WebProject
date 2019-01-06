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
    public class CpuServiceTests
    {
        private PcPartPickerDbContext _context;
        private ICpuService _cpuService;
        private List<Cpu> _testCpus;

        private void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PcPartPickerDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<ICpuService, CpuService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Cpu>, Repository<Cpu>>();
            IServiceProvider provider = services.BuildServiceProvider();
            _context = provider.GetService<PcPartPickerDbContext>();
            _cpuService = provider.GetService<ICpuService>();

            _testCpus = GetCpus();
        }

        //test GetAllCpus
        [Fact]
        public void GetAllCpus_WithListOfCpus_ShouldReturnAllCpus()
        {
            SetUp();

            SeedData();
            var allCpus = _cpuService.GetAllCpus();

            Assert.Equal(_testCpus, allCpus.ToList());
        }

        [Fact]
        public void GetAllCpus_WithEmptyListOfCpus_ShouldReturnEmptyList()
        {
            SetUp();

            var allCpus = _cpuService.GetAllCpus();
            var expectedResult = new List<Cpu>();

            Assert.Equal(expectedResult, allCpus);
        }

        //test GetCpuModels
        [Fact]
        public void GetCpuModels_WithListOfCpus_ShouldReturnAllModels()
        {
            SetUp();

            SeedData();
            var cpuModels = _cpuService.GetCpuModels().ToList();
            List<string> expectedResult = new List<string>() { "model1", "model2" };

            Assert.Equal(expectedResult, cpuModels);
        }

        [Fact]
        public void GetCpuModels_WithEmptyListOfCpus_ShouldReturnNoModels()
        {
            SetUp();
            List<string> expectedResult = new List<string>();

            var cpuModels = _cpuService.GetCpuModels().ToList();            

            Assert.Equal(expectedResult, cpuModels);
        }

        [Fact]
        public void GetCpuByModel_WithModelMatchingCpu_ShouldReturnCorrectCpu()
        {
            SetUp();

            SeedData();
            var cpu = _cpuService.GetCpuByModel("model1");

            Assert.True(cpu != null);
        }

        [Fact]
        public void GetCpuByModel_WithNoMatchingModel_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var cpu = _cpuService.GetCpuByModel("model");

            Assert.True(cpu == null);
        }
        
        [Fact]
        public void GetCpuById_WithIdMatchingCpu_ShouldReturnCorrectCpu()
        {
            SetUp();

            SeedData();
            var cpu = _cpuService.GetCpuById(1);

            Assert.True(cpu != null);
        }

        [Fact]
        public void GetCpuById_WithNoMatches_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var cpu = _cpuService.GetCpuById(10);

            Assert.True(cpu == null);
        }
        
        [Fact]
        public void InsertCpu_WithValidModel_ShouldInsertRecord()
        {
            SetUp();

            var cpu = new Cpu();
            _cpuService.InsertCpu(cpu);

            Assert.Equal(cpu, _context.Cpus.First());
        }


        [Fact]
        public void Update_WithExistingCpu_ShouldUpdateCpu()
        {
            SetUp();

            SeedData();
            var cpu = _context.Cpus.First();
            cpu.Model = "123";
            _cpuService.Update(cpu);

            Assert.Equal(cpu, _context.Cpus.First());
        }

        [Fact]
        public void Update_WithNonExistingCpu_ShouldThrowError()
        {
            SetUp();

            var cpu = new Cpu();

            Assert.ThrowsAny<Exception>(() => _cpuService.Update(cpu));
        }

        [Fact]
        public void Delete_WithExistingModel_ShouldRemoveCpu()
        {
            SetUp();

            SeedData();
            var cpu = _context.Cpus.First();
            _cpuService.Delete(cpu.CpuId);

            Assert.DoesNotContain(cpu, _context.Cpus);
        }

        [Fact]
        public void Delete_WithNonExistingModel_ShouldThrowError()
        {
            SetUp();
            
            Assert.ThrowsAny<Exception>(() => _cpuService.Delete(10));
        }

        private void SeedData()
        {
            _context.AddRange(_testCpus);
            _context.SaveChanges();
        }

        private List<Cpu> GetCpus()
        {
            Cpu testCpuOne = new Cpu() { Model = "model1" };
            Cpu testCpuTwo = new Cpu() { Model = "model2" };
            return new List<Cpu> { testCpuOne, testCpuTwo};
        }
    }
}
