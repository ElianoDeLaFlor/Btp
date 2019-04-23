using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Controllers
{
    public class ZipResult: ActionResult
    {
        private IEnumerable<FileResult> fileResult;
        private string fileName;

        public string FileName
        {
            get
            {
                return fileName ?? "file.zip";
            }
            set { fileName = value; }
        }

        public ZipResult(params FileResult[] files)
        {
            this.fileResult = files;
        }
        public ZipResult(IEnumerable<FileResult> files)
        {
            this.fileResult = files;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            //ZipFile.(fileResult, false, "");
            //    context.HttpContext
            //        .Response.ContentType = "application/zip";
            //    context.HttpContext
            //        .Response.AppendHeader("content-disposition", "attachment; filename=" + FileName);
            //    zf.Save(context.HttpContext.Response.OutputStream);
            
        }
    }
}