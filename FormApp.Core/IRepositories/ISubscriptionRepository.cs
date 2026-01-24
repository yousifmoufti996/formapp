using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface ISubscriptionRepository
{
    Task<Core.Entities.Subscription?> GetByIdAsync(Guid id);
    Task<Core.Entities.Subscription> AddAsync(Core.Entities.Subscription subscription);
    Task<Core.Entities.Subscription> UpdateAsync(Core.Entities.Subscription subscription);
}
