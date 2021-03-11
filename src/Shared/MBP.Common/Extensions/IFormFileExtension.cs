using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace MBP.Common.Extensions
{
	public static class IFormFileExtension
	{
		public static async Task<byte[]> GetBytes(this IFormFile formFile)
		{
			using (var memoryStream = new MemoryStream())
			{
				await formFile.CopyToAsync(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}
}
