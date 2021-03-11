using MBP.Contracts.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MBP.Identity.Data.Entities
{
	public partial class MbpUser : IdentityUser<Guid>, ICreatedEntity, IUpdatedEntity
	{
		public MbpUser()
		{
			Claims = new HashSet<IdentityUserClaim<Guid>>();
		}

		public string FullName { get; set; }
		public string AvatarUrl { get; set; }
		public string Status { get; set; }

		public DateTime? UpdatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }

		public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
	}
}
