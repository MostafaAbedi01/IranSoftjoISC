using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Upload_File_To_ASPNET_Web_API.Infrastructure.Util
{
	public class OSUtil
	{
		public static string GetValidFileName(string filePath)
		{
			char[] invalids = System.IO.Path.GetInvalidFileNameChars();
			return String.Join("_", filePath.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
		}
	}
}