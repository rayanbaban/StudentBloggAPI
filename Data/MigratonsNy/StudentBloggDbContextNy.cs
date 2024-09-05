using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using StudentBloggAPI.Models.Entities;

#nullable disable

namespace StudentBloggAPI.Data.MigratonsNy
{
    /// <inheritdoc />
    public class StudentBloggDbContextNy : DbContext
	{
		public StudentBloggDbContextNy(DbContextOptions<StudentBloggDbContextNy> options)
		: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Post> Post { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}
