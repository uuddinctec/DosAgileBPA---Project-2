using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R2MD
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                
                HttpFileCollection files = context.Request.Files;
                HttpPostedFile file = files[0];
                int size = Convert.ToInt32(file.ContentLength);
                string name = file.FileName;
                byte[] fileData = new byte[size];
                file.InputStream.Read(fileData, 0, size);
                SaveData SD = new SaveData();
                string ID = SD.saveUploadFileData(name, file.ContentType, size, fileData);
                context.Response.Write(ID);
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}