using System;

namespace PcPartPicker.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
