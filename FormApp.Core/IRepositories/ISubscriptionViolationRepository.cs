using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface ISubscriptionViolationRepository
{
    Task<SubscriptionViolation?> GetByIdAsync(Guid id);
    Task<SubscriptionViolation> AddAsync(SubscriptionViolation violation);
    Task<SubscriptionViolation> UpdateAsync(SubscriptionViolation violation);
}
