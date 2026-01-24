using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface ITransformerRepository
{
    Task<Transformer?> GetByIdAsync(Guid id);
    Task<Transformer> AddAsync(Transformer transformer);
    Task<Transformer> UpdateAsync(Transformer transformer);
}
