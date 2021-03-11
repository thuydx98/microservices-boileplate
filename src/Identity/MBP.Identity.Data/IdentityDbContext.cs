using MBP.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MBP.Identity.Data
{
	public class IdentityDbContext : IdentityDbContext<
		MbpUser,
		MbpRole,
		Guid,
		IdentityUserClaim<Guid>,
		IdentityUserRole<Guid>,
		IdentityUserLogin<Guid>,
		IdentityRoleClaim<Guid>,
		IdentityUserToken<Guid>>
	{
		public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

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

			modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("MbpUserClaims");
			modelBuilder.Entity<IdentityUserToken<string>>().ToTable("MbpUserTokens");
			modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("MbpRoleClaims");
		}
	}
}
