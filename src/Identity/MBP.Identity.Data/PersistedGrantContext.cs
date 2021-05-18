using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace MBP.Identity.Data
{
	public class PersistedGrantContext : PersistedGrantDbContext
	{
		public PersistedGrantContext(DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions) : base(options, storeOptions)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//foreach (var entity in modelBuilder.Model.GetEntityTypes())
			//{
			//	entity.SetTableName($"PG.{entity.GetTableName()}");
			//}
		}
	}
}
