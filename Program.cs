
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using PersonRESTApi.MiddleWare;
using Serilog;
using StudentBloggAPI.Data;
using StudentBloggAPI.Data.MigratonsNy;
using StudentBloggAPI.Extensions;
using StudentBloggAPI.Mappers;
using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Middleware;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			
			// Extension methods
			builder.RegisterMappers();
			builder.AddSwaggerWithBasicAuthentication();

			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IPostService, PostService>();
			builder.Services.AddScoped<ICommentService,CommentService >();
			//builder.Services.AddScoped(typeof(IMapper<User, UserDTO>), typeof(UserMapper));
			builder.Services.AddScoped<IMapper<User, UserDTO>, UserMapper>();
			builder.Services.AddScoped<IMapper<User, UserRegistrationDTO>, UserRegMapper>();

			builder.Services.AddScoped<StudentBloggBasicAuthentication>();
			builder.Services.AddTransient<GlobalExceptionMiddleware>();

			builder.Services.AddValidatorsFromAssemblyContaining<Program>();
			builder.Services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = false);

			//builder.Services.AddSingleton<IUserRepository, UserRepositoryInMemory>();
			builder.Services.AddScoped<IUserRepository, UserRepositoryDbContext>();
			builder.Services.AddScoped<IPostRepository, PostRepository>();
			builder.Services.AddScoped<ICommentRepository, CommentRepository>();
			

			builder.Services.AddDbContext<StudentBloggDbContextNy>(options =>
				options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
				new MySqlServerVersion(new Version(8, 0))));
			//registreringen av sriloggen som ble skrevet inn i config filen skjer her 
			builder.Host.UseSerilog((context, configuration) =>
			{
				//her sier vi at vi vil bruke serilog og hente konfigurasjonen fra config filen
				//husk også	legge det til i DI som ligger i personDBHandler
				//da er vi i mål
				configuration.ReadFrom.Configuration(context.Configuration);
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseSerilogRequestLogging();
			app.UseHttpsRedirection();
			app.UseMiddleware<StudentBloggBasicAuthentication>();
			app.UseMiddleware<GlobalExceptionMiddleware>();
			app.UseAuthorization();

			//leter etter alle controller klasser som implemeterer controllerbase klassen. controllers blir registerer automatisk
			//vi trenger kun lage controllere som impelmeterer controllerbase klassen
			app.MapControllers();

			app.Run();
		}
	}
}