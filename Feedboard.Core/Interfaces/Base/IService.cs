using Feedboard.Contracts.DTOs.Response;

namespace Feedboard.Core.Interfaces.Base;

public interface IService<T>
    where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task<DeleteDto> DeleteByIdAsync(Guid id);
    Task<DeleteDto> SoftDeleteByIdAsync(Guid id);
    Task<T> UpdateOrInsertAsync(T obj);
}
