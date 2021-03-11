using Microsoft.EntityFrameworkCore;

namespace MBP.User.Data.Contexts
{
	public class ReadDbContext : ApplicationDbContext
	{
		public ReadDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
	}
}
