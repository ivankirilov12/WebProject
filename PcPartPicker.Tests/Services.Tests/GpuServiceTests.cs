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
    public class GpuServiceTests
    {
        private ApplicationDbContext _context;
        private IGpuService _gpuService;
        private List<Gpu> _testGpus;

        private void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IGpuService, GpuService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Gpu>, Repository<Gpu>>();
            IServiceProvider provider = services.BuildServiceProvider();
            _context = provider.GetService<ApplicationDbContext>();
            _gpuService = provider.GetService<IGpuService>();

            _testGpus = GetGpus();
        }

        [Fact]
        public void GetAllGpus_ListOfGpus_ShouldReturnAllGpus()
        {
            SetUp();

            SeedData();
            var allGpus = _gpuService.GetAllGpus();

            Assert.Equal(_testGpus, allGpus.ToList());
        }

        [Fact]
        public void GetAllGpus_EmptyListOfGpus_ShouldReturnEmptyList()
        {
            SetUp();

            var allGpus = _gpuService.GetAllGpus();
            var expectedResult = new List<Gpu>();

            Assert.Equal(expectedResult, allGpus);
        }

        [Fact]
        public void GetGpuModels_ListOfGpus_ShouldReturnAllModels()
        {
            SetUp();

            SeedData();
            var gpuModels = _gpuService.GetGpuModels().ToList();
            List<string> expectedResult = new List<string>() { "model1", "model2" };

            Assert.Equal(expectedResult, gpuModels);
        }

        [Fact]
        public void GetGpuModels_EmptyListOfGpus_ShouldReturnNoModels()
        {
            SetUp();
            List<string> expectedResult = new List<string>();

            var gpuModels = _gpuService.GetGpuModels().ToList();

            Assert.Equal(expectedResult, gpuModels);
        }


        [Fact]
        public void GetGpuByModel_ModelMatchesGpu_ShouldReturnCorrectGpu()
        {
            SetUp();

            SeedData();
            var gpu = _gpuService.GetGpuByModel("model1");

            Assert.True(gpu != null);
        }

        [Fact]
        public void GetGpuByModel_NoMatchingModel_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var gpu = _gpuService.GetGpuByModel("model");

            Assert.True(gpu == null);
        }

        [Fact]
        public void GetGpuById_IdMatchesGpu_ShouldReturnCorrectGpu()
        {
            SetUp();

            SeedData();
            var gpu = _gpuService.GetGpuById(1);

            Assert.True(gpu != null);
        }

        [Fact]
        public void GetGpuById_NoMatches_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var gpu = _gpuService.GetGpuById(10);

            Assert.True(gpu == null);
        }

        [Fact]
        public void InsertGpu_InsertModel_ShouldBeInserted()
        {
            SetUp();

            var gpu = new Gpu();
            _gpuService.InsertGpu(gpu);

            Assert.Equal(gpu, _context.Gpus.First());
        }
                
        [Fact]
        public void Update_UpdateExistingGpu_ShouldUpdateGpu()
        {
            SetUp();

            SeedData();
            var gpu = _context.Gpus.First();
            gpu.Model = "123";
            _gpuService.Update(gpu);

            Assert.Equal(gpu, _context.Gpus.First());
        }

        [Fact]
        public void Delete_ExistingModel_ShouldRemoveGpu()
        {
            SetUp();

            SeedData();
            var gpu = _context.Gpus.First();
            _gpuService.Delete(gpu.GpuId);

            Assert.DoesNotContain(gpu, _context.Gpus);
        }

        private void SeedData()
        {
            _context.AddRange(_testGpus);
            _context.SaveChanges();
        }

        private List<Gpu> GetGpus()
        {
            Gpu testGpuOne = new Gpu() { GpuId = 1, Model = "model1" };
            Gpu testGpuTwo = new Gpu() { GpuId = 2, Model = "model2" };
            return new List<Gpu> { testGpuOne, testGpuTwo };
        }
    }
}
