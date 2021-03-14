using MBP.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MBP.Identity.Data
{
	public class IdentityContext : IdentityDbContext<
		MbpUser,
		MbpRole,
		Guid,
		IdentityUserClaim<Guid>,
		IdentityUserRole<Guid>,
		IdentityUserLogin<Guid>,
		IdentityRoleClaim<Guid>,
		IdentityUserToken<Guid>>
	{
		public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				entity.SetTableName(entity.GetTableName().Replace("AspNet", string.Empty));
			}

			modelBuilder.Entity<MbpUser>(users =>
			{
				users.Property(x => x.CreatedAt)
					.HasDefaultValueSql("now() at time zone 'utc'");

				users.HasMany(x => x.Claims)
					.WithOne()
					.HasForeignKey(x => x.UserId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}
