using C.O.S.E.C.Api.Properties;
using C.O.S.E.C.Domain.Entity;
using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Domain.InterfaceDrivers.Business;
using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Infrastructure.Auth.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [Route("api/files")]
    [Produces("application/json")]
    [Authorize(AuthPolicyEnum.RequireRoleOfAdminOrClient)]
    public class FileController : ControllerBase
    {
        private readonly IUserFileBLL fileBLL;
        readonly string[] imgtype = { "jpg", "jpeg", "gif", "png" };
        readonly string[] doctype = { "pdf", "txt", "doc", "xls", "ppt", "docx", "xlsx", "pptx" };
        public FileController(IUserFileBLL userFileBLL) => this.fileBLL = userFileBLL;

        /// <summary>
        /// 上传多张图片
        /// </summary>
        /// <param name="files"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        [HttpPost("UploadImgList"), Description("上传多张图片")]
        [Authorize(AuthPolicyEnum.RequireRoleOfSystemAdmin)]
        public async Task<List<string>> UploadImgList(IFormFileCollection files, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            if (files is null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            if (hostingEnvironment is null)
            {
                throw new ArgumentNullException(nameof(hostingEnvironment));
            }

            List<string> filenames = new List<string>();
            foreach (var file in files)
            {
                var extname = file.FileName.Split(".").Last();
                if (!imgtype.Contains(extname))
                {
                    throw new Infrastructure.CustomException.AppException(Resources.Error_txt_file_type);
                }
                if (file.Length > 1024 * 1024 * 2)
                {
                    throw new Infrastructure.CustomException.AppException(Resources.Error_txt_img_size);
                }

                var fileName = file.FileName + "." + extname;
                Console.WriteLine(fileName);

                fileName = $@"\UploadFile\{fileName}";
                filenames.Add(fileName);

                fileName = $@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\{fileName}";
                if (!Directory.Exists($@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\"))
                {
                    _ = Directory.CreateDirectory($@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\");//不存在就创建目录
                }
                using FileStream fs = System.IO.File.Create(fileName);
                await file.CopyToAsync(fs).ConfigureAwait(false);
                fs.Flush();
            }
            return filenames;
        }

        /// <summary>
        /// 上传单张图片
        /// </summary>
        /// <param name="file"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        [HttpPost("UploadImg"), Description("上传单张图片")]
        [Authorize(AuthPolicyEnum.RequireRoleOfAdminOrClient)]
        public async Task<string> UploadImg(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (hostingEnvironment is null)
            {
                throw new ArgumentNullException(nameof(hostingEnvironment));
            }

            var extname = file.FileName.Split(".").Last();
            if (!imgtype.Contains(extname))
            {
                throw new Infrastructure.CustomException.AppException(Resources.Error_txt_file_type);
            }
            if (file.Length > 1024 * 1024 * 2)
            {
                throw new Infrastructure.CustomException.AppException(Resources.Error_txt_img_size);
            }
            var fileName = DateTime.Now.Ticks + "." + extname;
            fileName = $@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\{fileName}";
            if (!Directory.Exists($@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\"))
            {
                _ = Directory.CreateDirectory($@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\");//不存在就创建目录
            }
            using FileStream fs = System.IO.File.Create(fileName);
            await file.CopyToAsync(fs).ConfigureAwait(false);
            fs.Flush();
            _ = fileBLL.SaveFormAsync(default, new UserFile
            {
                FileExt = extname,
                FileName = fileName,
                FilePath = $@"\UploadFile\{DateTime.Today:d}\{fileName}",
                FormerName = file.FileName
            });
            return fileName;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        [HttpPost("UploadDoc"), Description("上传文件")]
        [Authorize(AuthPolicyEnum.RequireRoleOfAdminOrClient)]
        public async Task<string> UploadDocAsync(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (hostingEnvironment is null)
            {
                throw new ArgumentNullException(nameof(hostingEnvironment));
            }

            var extname = file.FileName.Split(".").Last();
            if (!doctype.Contains(extname))
            {
                throw new Infrastructure.CustomException.AppException(Resources.Error_txt_file_type);
            }
            var fileName = DateTime.Now.Ticks + "." + extname;

            fileName = $@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\{fileName}";
            if (!Directory.Exists($@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\"))
            {
                _ = Directory.CreateDirectory($@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\");//不存在就创建目录
            }
            using FileStream fs = System.IO.File.Create(fileName);
            await file.CopyToAsync(fs).ConfigureAwait(false);
            fs.Flush();
            _ = fileBLL.SaveFormAsync(default, new UserFile
            {
                FileExt = extname,
                FileName = fileName,
                FilePath = $@"\UploadFile\{DateTime.Today:d}\{fileName}",
                FormerName = file.FileName
            });
            return fileName;
        }

        /// <summary>
        /// 上传多文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        [HttpPost("UploadDocList"), Description("上传多文件")]
        [Authorize(AuthPolicyEnum.RequireRoleOfSystemAdmin)]
        public async Task<List<string>> UploadDocList(IFormFileCollection files, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            if (files is null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            if (hostingEnvironment is null)
            {
                throw new ArgumentNullException(nameof(hostingEnvironment));
            }

            List<string> filenames = new List<string>();
            foreach (var file in files)
            {
                var extname = file.FileName.Split(".").Last();
                if (!doctype.Contains(extname))
                {
                    throw new Infrastructure.CustomException.AppException(Resources.Error_txt_file_type);
                }

                var fileName = file.FileName;
                Console.WriteLine(fileName);

                fileName = $@"\UploadFile\{fileName}";
                filenames.Add(fileName);

                fileName = $@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\{fileName}";
                if (!Directory.Exists($@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\"))
                {
                    _ = Directory.CreateDirectory($@"{hostingEnvironment.ContentRootPath}\UploadFile\{DateTime.Today:d}\");//不存在就创建目录
                }
                using FileStream fs = System.IO.File.Create(fileName);
                await file.CopyToAsync(fs).ConfigureAwait(false);
                fs.Flush();
            }
            return filenames;
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("/UploadFile/{path}/{name}"), Description("读取图片")]
        [AuthorizeFree]
        public FileContentResult LoadingPhoto(string path, string name)
        {
            path = Directory.GetCurrentDirectory() + "\\UploadFile\\" + path + "\\" + name;// + ".jpeg";
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
            {
                return null;
            }
            using FileStream fs = fi.OpenRead();
            byte[] buffer = new byte[fi.Length];
            //读取图片字节流
            //从流中读取一个字节块，并在给定的缓冲区中写入数据。
            fs.Read(buffer, 0, Convert.ToInt32(fi.Length));
            return File(buffer, "image/jpeg");
        }

        /// <summary>
        /// 获取用户文件列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpPost("GetFileListByUser"), Description("获取文件列表")]
        public async Task<PagingResult<UserFile>> GetFileListByUserAsync(Pagination pagination, string uid)
        {
            if (pagination is null)
            {
                pagination = new Pagination();
            }

            SqlSugar.RefAsync<int> totalNum = 0;
            var list = await fileBLL.GetPageListAsync(n => n.CreateUserID == uid, pagination, totalNum).ConfigureAwait(false);
            pagination.Records = totalNum.Value;
            return new PagingResult<UserFile>(pagination) { Data = list };
        }
    }
}