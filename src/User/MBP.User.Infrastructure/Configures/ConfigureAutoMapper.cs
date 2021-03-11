using AutoMapper;
using MBP.User.Infrastructure.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace MBP.User.Infrastructure.Configures
{
	public static class ConfigureAutoMapper
	{
		public static IServiceCollection AddAutoMapper(this IServiceCollection services)
		{
			var configuration = new MapperConfiguration(config =>
			{
				config.AddProfile(new MappingProfile());
			});

			var mapper = configuration.CreateMapper();

			return services.AddSingleton(mapper);
		}
	}
}
