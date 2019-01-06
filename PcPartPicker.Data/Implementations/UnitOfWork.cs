using PcPartPicker.Data.Interfaces;
using PcPartPicker.Models.Models;
using System;

namespace PcPartPicker.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private PcPartPickerDbContext _context;
        private Repository<Case> casesRepository;
        private Repository<Cpu> cpusRepository;
        private Repository<Gpu> gpusRepository;
        private Repository<Motherboard> motherboardsRepository;
        private Repository<StorageOption> storageRepository;
        private Repository<MemoryOption> memoryRepository;
        private Repository<SystemBuild> systembuildsRepository;

        public UnitOfWork(PcPartPickerDbContext context)
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

        public Repository<StorageOption> StorageOptionsRepository
        {
            get
            {
                if (storageRepository == null)
                {
                    storageRepository = new Repository<StorageOption>(_context);
                }
                return storageRepository;
            }
        }

        public Repository<MemoryOption> MemoryOptionsRepository
        {
            get
            {
                if (memoryRepository == null)
                {
                    memoryRepository = new Repository<MemoryOption>(_context);
                }
                return memoryRepository;
            }
        }

        public Repository<SystemBuild> SystemBuildRepository
        {
            get
            {
                if (systembuildsRepository == null)
                {
                    systembuildsRepository = new Repository<SystemBuild>(_context);
                }
                return systembuildsRepository;
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
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
