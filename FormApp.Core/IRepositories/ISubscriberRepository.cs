using FormApp.Core.Entities;

namespace FormApp.Core.IRepositories;

public interface ISubscriberRepository
{
    Task<Subscriber?> GetByIdAsync(Guid id);
    Task<Subscriber?> GetByAccountNumberAsync(string accountNumber);
    Task<Subscriber?> GetBySubscriptionNumberAsync(string subscriptionNumber);
    Task<Subscriber> AddAsync(Subscriber subscriber);
    Task<Subscriber> UpdateAsync(Subscriber subscriber);
}
