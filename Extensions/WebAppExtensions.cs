using Microsoft.OpenApi.Models;
using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Mappers;


namespace StudentBloggAPI.Extensions
{
	public static class WebAppExtensions
	{
		public static void RegisterMappers(this WebApplicationBuilder builder)
		{
			var assembly = typeof(UserMapper).Assembly; // eller en hvilken som helst klasse som ligger i samme assembly som mapperne dine

			var mapperTypes = assembly.GetTypes()
				.Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
				.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)))
				.ToList();

			foreach (var mapperType in mapperTypes)
			{
				var interfaceType = mapperType.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IMapper<,>));
				builder.Services.AddScoped(interfaceType, mapperType);
			}
		}

		public static void AddSwaggerWithBasicAuthentication(this WebApplicationBuilder builder)
		{
			builder.Services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "basic",
					In = ParameterLocation.Header,
					Description = "Basic Authorization header using the Bearer scheme."
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "basic"
						}
					},
					new string[] {}
				}
			});
			});
		}
	}

}
