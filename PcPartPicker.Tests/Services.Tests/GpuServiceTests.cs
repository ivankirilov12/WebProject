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
        private PcPartPickerDbContext _context;
        private IGpuService _gpuService;
        private List<Gpu> _testGpus;

        private void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PcPartPickerDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IGpuService, GpuService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Gpu>, Repository<Gpu>>();
            IServiceProvider provider = services.BuildServiceProvider();
            _context = provider.GetService<PcPartPickerDbContext>();
            _gpuService = provider.GetService<IGpuService>();

            _testGpus = GetGpus();
        }

        [Fact]
        public void GetAllGpus_WithListOfGpus_ShouldReturnAllGpus()
        {
            SetUp();

            SeedData();
            var allGpus = _gpuService.GetAllGpus();

            Assert.Equal(_testGpus, allGpus.ToList());
        }

        [Fact]
        public void GetAllGpus_WithEmptyListOfGpus_ShouldReturnEmptyList()
        {
            SetUp();

            var allGpus = _gpuService.GetAllGpus();
            var expectedResult = new List<Gpu>();

            Assert.Equal(expectedResult, allGpus);
        }

        [Fact]
        public void GetGpuModels_WithListOfGpus_ShouldReturnAllModels()
        {
            SetUp();

            SeedData();
            var gpuModels = _gpuService.GetGpuModels().ToList();
            List<string> expectedResult = new List<string>() { "model1", "model2" };

            Assert.Equal(expectedResult, gpuModels);
        }

        [Fact]
        public void GetGpuModels_WithEmptyListOfGpus_ShouldReturnNoModels()
        {
            SetUp();
            List<string> expectedResult = new List<string>();

            var gpuModels = _gpuService.GetGpuModels().ToList();

            Assert.Equal(expectedResult, gpuModels);
        }


        [Fact]
        public void GetGpuByModel_WithModelMatchingGpu_ShouldReturnCorrectGpu()
        {
            SetUp();

            SeedData();
            var gpu = _gpuService.GetGpuByModel("model1");

            Assert.True(gpu != null);
        }

        [Fact]
        public void GetGpuByModel_WithNoMatchingModel_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var gpu = _gpuService.GetGpuByModel("model");

            Assert.True(gpu == null);
        }

        [Fact]
        public void GetGpuById_WithIdMatchingGpu_ShouldReturnCorrectGpu()
        {
            SetUp();

            SeedData();
            var gpu = _gpuService.GetGpuById(1);

            Assert.True(gpu != null);
        }

        [Fact]
        public void GetGpuById_WithNoMatches_ShouldReturnNull()
        {
            SetUp();

            SeedData();
            var gpu = _gpuService.GetGpuById(10);

            Assert.True(gpu == null);
        }

        [Fact]
        public void InsertGpu_WithValidModel_ShouldBeInserted()
        {
            SetUp();

            var gpu = new Gpu();
            _gpuService.InsertGpu(gpu);

            Assert.Equal(gpu, _context.Gpus.First());
        }
                
        [Fact]
        public void Update_WithExistingGpu_ShouldUpdateGpu()
        {
            SetUp();

            SeedData();
            var gpu = _context.Gpus.First();
            gpu.Model = "123";
            _gpuService.Update(gpu);

            Assert.Equal(gpu, _context.Gpus.First());
        }

        [Fact]
        public void Update_WithNonExistingGpu_ShouldThrowError()
        {
            SetUp();

            var gpu = new Gpu();

            Assert.ThrowsAny<Exception>(() => _gpuService.Update(gpu));
        }

        [Fact]
        public void Delete_WithExistingModel_ShouldRemoveGpu()
        {
            SetUp();

            SeedData();
            var gpu = _context.Gpus.First();
            _gpuService.Delete(gpu.GpuId);

            Assert.DoesNotContain(gpu, _context.Gpus);
        }

        [Fact]
        public void Delete_WithNonExistingModel_ShouldThrowError()
        {
            SetUp();

            Assert.ThrowsAny<Exception>(() => _gpuService.Delete(1));
        }

        private void SeedData()
        {
            _context.AddRange(_testGpus);
            _context.SaveChanges();
        }

        private List<Gpu> GetGpus()
        {
            Gpu testGpuOne = new Gpu() { Model = "model1" };
            Gpu testGpuTwo = new Gpu() { Model = "model2" };
            return new List<Gpu> { testGpuOne, testGpuTwo };
        }
    }
}
