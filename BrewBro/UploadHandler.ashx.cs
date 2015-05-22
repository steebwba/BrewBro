using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace BrewBro.Views
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
               
                string sFolder = DateTime.Now.ToString("ddMMyyyy");
                switch (context.Request.Form["type"])
                {
                    case "ProfilePic":
                        var uploadPath = string.Format("{0}\\Uploads\\Profiles\\{1}", ConfigurationManager.AppSettings["UploadPath"], sFolder);
                        if(!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        HttpPostedFile file = files[0];
                        var userId = context.Request.Form["user"];
                        string fileName = string.Format("{0}.png", userId);


                        string fname = string.Format("{0}\\{1}", uploadPath, fileName);
                        file.SaveAs(fname);

                        new BrewBro.Users.Business.Users().SaveProfileImage(new Guid(userId), Path.Combine(sFolder, fileName));
                        break;
                    default:
                        break;
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write("File/s uploaded successfully!");
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