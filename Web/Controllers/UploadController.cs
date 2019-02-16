using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Core.Helpers;

namespace Web.Controllers
{
    /// <summary>
    /// Controller for uploading file
    /// </summary>
    [Route("[controller]")]
    [Authorize(Roles ="Admin")]
    public class UploadController : Controller
    {
        private IHostingEnvironment _env;
        private IConfiguration _configuration;
        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public UploadController(IHostingEnvironment env,IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }
        /// <summary>
        /// Save uploaded file to configured path
        /// Demo is in Views/Home/Index
        /// </summary>
        /// <returns>Json object with status and/or filename</returns>
        [RequestFormLimits(MultipartBodyLengthLimit =209715200)]
        [HttpPost]
        [Route("Up")]
        public async Task<IActionResult> Upload()
        {
            var files = HttpContext.Request.Form.Files;
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            string filePath = $"{_env.WebRootPath}\\{_configuration["UploadFilePath"]}\\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }


            //foreach (var file in files)
            //{
            //    if (file.Length > 0)
            //    {
            //        string[] temp = file.FileName.Split("\\");
            //        filePath += temp[temp.Length - 1];
            //        using (var stream = new FileStream(filePath , FileMode.Create))
            //        {
            //            await file.CopyToAsync(stream);
            //        }
            //    }
            //}

            var file = files[0];
            string[] temp = file.FileName.Split("\\");
            string fileName = temp[temp.Length - 1];
            filePath += fileName;

            if (System.IO.File.Exists(filePath))
            {
                return Ok(new { status = "error" });
            }
            else
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new {status="success", fileName });
            }


        }
        /// <summary>
        /// Delete a existing file from configured path
        /// </summary>
        [HttpPost]
        [Route("Down")]
        public void Remove()
        {
            JObject jsonObj = Request.Body.GetJObject();
            string fileName = jsonObj["fileName"].ToString();
            string filePath = $"{_env.WebRootPath}\\{_configuration["UploadFilePath"]}\\{fileName}";
            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

    }
}