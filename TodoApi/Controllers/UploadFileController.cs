using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using TodoApi.Models.ApiResult;

namespace TodoApi.Controllers
{
    /// <summary>
    /// 上传文件
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        [HttpPost, Route("UploadFile")]
        public ActionResult<ApiResult> UploadFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                if (file.Length > (1024 * 1024) * 5) // 5MB in bytes
                {
                    return ApiResultHelper.Error("文件大小限制 5MB");
                }

                string uploadeFile = Path.Combine(@"Content\Upload");
                if (Directory.Exists(uploadeFile) == false)
                {
                    Directory.CreateDirectory(uploadeFile);
                }

                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension != ".png" && fileExtension != ".mp4")
                {
                    return ApiResultHelper.Error("只允许上传 PNG 和 MP4 文件");
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadeFile, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                    fileStream.Dispose();
                }
            }
            else
            {
                return ApiResultHelper.Error("上传文件不能为空");
            }

            return ApiResultHelper.Success("/images/upload/" + uniqueFileName + "");
        }

    }
}
