using Feedboard.Contracts.DTOs;

namespace Feedboard.Core.Interfaces;

public interface IGitHubService
{
	Task<GitHubAccountDto> UpdateOrInsertByUserIdAsync(GitHubAccountDto obj);
}
