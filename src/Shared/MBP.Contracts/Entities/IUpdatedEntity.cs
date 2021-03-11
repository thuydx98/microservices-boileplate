using System;

namespace MBP.Contracts.Entities
{
	public interface IUpdatedEntity
	{
		DateTime? UpdatedAt { get; set; }
		string UpdatedBy { get; set; }
	}
}
