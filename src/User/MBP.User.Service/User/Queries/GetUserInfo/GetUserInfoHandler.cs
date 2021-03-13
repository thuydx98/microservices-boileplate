using MBP.Common.ApiResponse;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MBP.User.Service.User.Queries.GetUserInfo
{
	public class GetUserInfoHandler : IRequestHandler<GetUserInfoQuery, ApiResult>
	{
		public Task<ApiResult> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
