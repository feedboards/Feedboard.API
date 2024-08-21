using AutoMapper;
using Feedboard.Contracts.DTOs;
using Feedboard.Core.Interfaces;
using Feedboard.DAL.Context;
using Feedboard.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Feedboard.Core.Services;

public class AzureService : IAzureService
{
	private readonly FeedboardDbContext feedboardDbContext;
	private readonly IMapper mapper;

	public AzureService(FeedboardDbContext feedboard, IMapper mapper)
	{
		this.feedboardDbContext = feedboard;
		this.mapper = mapper;
	}

	public async Task<AzureAccountDto> UpdateOrInsertAsync(AzureAccountDto obj)
	{
		var model = mapper.Map<AzureAccount>(obj);
		model.IsActive = true;

		var existingModel = await feedboardDbContext.AzureAccounts
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Email == model.Email);

		if (existingModel == null)
		{
			feedboardDbContext.AzureAccounts.Add(model);
		}
		else
		{
			model.UpdatedAt = DateTime.UtcNow;
			feedboardDbContext.Update(model);
		}

		await feedboardDbContext.SaveChangesAsync();

		return mapper.Map<AzureAccountDto>(model);
	}
}
