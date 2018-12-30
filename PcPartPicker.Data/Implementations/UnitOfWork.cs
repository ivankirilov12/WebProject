using PcPartPicker.Data.Interfaces;
using PcPartPicker.Models.Models;
using System;

namespace PcPartPicker.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;// = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>());
        private Repository<Case> casesRepository;
        private Repository<Cpu> cpusRepository;
        private Repository<Gpu> gpusRepository;
        private Repository<Motherboard> motherboardsRepository;
        private Repository<Storage> storageRepository;
        private Repository<Ram> memoryRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        #region repositoryGetters
        public Repository<Case> CasesRepository
        {
            get
            {
                if (casesRepository == null)
                {
                    casesRepository = new Repository<Case>(_context);
                }
                return casesRepository;
            }
        }

        public Repository<Cpu> CpusRepository
        {
            get
            {
                if (cpusRepository == null)
                {
                    cpusRepository = new Repository<Cpu>(_context);
                }
                return cpusRepository;
            }
        }

        public Repository<Gpu> GpusRepository
        {
            get
            {
                if (gpusRepository == null)
                {
                    gpusRepository = new Repository<Gpu>(_context);
                }
                return gpusRepository;
            }
        }

        public Repository<Motherboard> MotherboardsRepository
        {
            get
            {
                if (motherboardsRepository == null)
                {
                    motherboardsRepository = new Repository<Motherboard>(_context);
                }
                return motherboardsRepository;
            }
        }

        public Repository<Storage> StorageOptionsRepository
        {
            get
            {
                if (storageRepository == null)
                {
                    storageRepository = new Repository<Storage>(_context);
                }
                return storageRepository;
            }
        }

        public Repository<Ram> MemoryOptionsRepository
        {
            get
            {
                if (memoryRepository == null)
                {
                    memoryRepository = new Repository<Ram>(_context);
                }
                return memoryRepository;
            }
        }
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
