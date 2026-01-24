using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface IMeterScaleRepository
{
    Task<MeterScale?> GetByIdAsync(Guid id);
    Task<MeterScale> AddAsync(MeterScale meterScale);
    Task<MeterScale> UpdateAsync(MeterScale meterScale);
}
