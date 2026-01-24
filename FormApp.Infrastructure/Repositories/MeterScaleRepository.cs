using FormApp.Core.Entities;
using FormApp.Core.IRepositories;
using FormApp.Infrastructure.Data;

namespace FormApp.Infrastructure.Repositories;

public class MeterScaleRepository : IMeterScaleRepository
{
    private readonly ApplicationDbContext _context;

    public MeterScaleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MeterScale?> GetByIdAsync(Guid id)
    {
        return await _context.MeterScales.FindAsync(id);
    }

    public async Task<MeterScale> AddAsync(MeterScale meterScale)
    {
        await _context.MeterScales.AddAsync(meterScale);
        await _context.SaveChangesAsync();
        return meterScale;
    }

    public async Task<MeterScale> UpdateAsync(MeterScale meterScale)
    {
        _context.MeterScales.Update(meterScale);
        await _context.SaveChangesAsync();
        return meterScale;
    }
}
