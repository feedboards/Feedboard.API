using AutoMapper;
using Feedboard.Contracts.DTOs;
using Feedboard.DAL.Models;

namespace Feedboard.Core;

public class MapperConfig
{
	public static MapperConfiguration RegisterMaps()
	{
		return new MapperConfiguration(config =>
		{
			config.CreateMap<AzureAccountDto, AzureAccount>().ReverseMap();
			config.CreateMap<GitHubAccountDto, GitHubAccount>().ReverseMap();
		});
	}
}
