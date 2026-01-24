using FormApp.Core.Entities;
using FormApp.Core.IRepositories;
using FormApp.Infrastructure.Data;

namespace FormApp.Infrastructure.Repositories;

public class TransformerRepository : ITransformerRepository
{
    private readonly ApplicationDbContext _context;

    public TransformerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Transformer?> GetByIdAsync(Guid id)
    {
        return await _context.Transformers.FindAsync(id);
    }

    public async Task<Transformer> AddAsync(Transformer transformer)
    {
        await _context.Transformers.AddAsync(transformer);
        await _context.SaveChangesAsync();
        return transformer;
    }

    public async Task<Transformer> UpdateAsync(Transformer transformer)
    {
        _context.Transformers.Update(transformer);
        await _context.SaveChangesAsync();
        return transformer;
    }
}
