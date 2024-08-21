using AutoMapper;
using Feedboard.Contracts.DTOs;
using Feedboard.Core.Interfaces;
using Feedboard.DAL.Context;
using Feedboard.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Feedboard.Core.Services;

public class GitHubService : IGitHubService
{
	private readonly FeedboardDbContext feedboardDbContext;
	private readonly IMapper mapper;

	public GitHubService(FeedboardDbContext feedboard, IMapper mapper)
	{
		this.feedboardDbContext = feedboard;
		this.mapper = mapper;
	}

	public async Task<GitHubAccountDto> UpdateOrInsertByUserIdAsync(GitHubAccountDto obj)
	{
		var model = mapper.Map<GitHubAccount>(obj);
		model.IsActive = true;

		var existingModel = await feedboardDbContext.GitHubAccounts
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.UserId == model.UserId);

		if (existingModel == null)
		{
			model.UpdatedAt = null;
			feedboardDbContext.GitHubAccounts.Add(model);
		}
		else
		{
			model.UpdatedAt = DateTime.UtcNow;
			feedboardDbContext.GitHubAccounts.Update(model);
		}

		await feedboardDbContext.SaveChangesAsync();

		return mapper.Map<GitHubAccountDto>(model);
	}
}
