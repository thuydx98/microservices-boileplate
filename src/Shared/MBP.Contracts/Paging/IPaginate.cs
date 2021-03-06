using System.Collections.Generic;

namespace MBP.Contracts.Paging
{
	public interface IPaginate<TResult>
	{
		int PageSize { get; }
		int PageIndex { get; }
		int Total { get; }
		IList<TResult> Items { get; }
	}
}
