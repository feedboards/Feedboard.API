using Feedboard.Contracts.DTOs;

namespace Feedboard.Core.Interfaces;

public interface IAzureService
{
	Task<AzureAccountDto> UpdateOrInsertAsync(AzureAccountDto obj);
}
