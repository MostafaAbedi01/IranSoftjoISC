using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using Upload_File_To_ASPNET_Web_API.Infrastructure.Util;

namespace Upload_File_To_ASPNET_Web_API.Infrastructure
{
	public class WithExtensionMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
	{
	    private string _nameGuid;
	    public string NameId { get; set; }
	    public string FileName { get; set; }

	    public WithExtensionMultipartFormDataStreamProvider(string rootPath,string nameGuid)
			: base(rootPath)
        {
            _nameGuid = nameGuid;
        }

		public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
		{
		    NameId = headers.ContentDisposition.Name;
            FileName = headers.ContentDisposition.FileName;
            string extension = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? Path.GetExtension(OSUtil.GetValidFileName(headers.ContentDisposition.FileName)) : "";
			return _nameGuid  + extension ;
		}
	}
}