using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MBP.Common.Extensions
{
	public static class File
	{
		public static async Task<string> ReadFileContentAsync(IList<string> paths)
		{
			paths.Insert(0, "wwwroot");
			paths.Insert(0, Directory.GetCurrentDirectory());

			var file = Path.Combine(paths.Select(path => path).ToArray());
			FileStream fileStream = new FileStream(file, FileMode.Open);

			using (StreamReader reader = new StreamReader(fileStream))
			{
				return await reader.ReadToEndAsync();
			}
		}
	}
}
