using MBP.Contracts.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace MBP.Identity.Data.Entities
{
	public class MbpRole : IdentityRole<Guid>, ICreatedEntity, IUpdatedEntity
	{
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }
	}
}
