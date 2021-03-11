using Microsoft.EntityFrameworkCore;

namespace MBP.User.Data.Contexts
{
	public class WriteDbContext : ApplicationDbContext
	{
		public WriteDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
	}
}
