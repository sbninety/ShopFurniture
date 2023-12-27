using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Helper;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        public UploadImageController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile formFile, string productId)
        {
            APIResponse response = new APIResponse();
            try
            {
                string Filepath = GetFilePath(productId);
                if (!System.IO.File.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                string imagepath = Filepath + "\\" + productId + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await formFile.CopyToAsync(stream);
                    response.ResponseCode = 200;
                    response.Result = "pass";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return Ok(response);
        }
        [Authorize]
        [HttpPost("MultiUploadImage")]
        public async Task<IActionResult> MultiUploadImage(IFormFileCollection fileColection, string productId)
        {
            APIResponse response = new APIResponse();
            int passcount = 0, errorcount = 0;
            try
            {
                string Filepath = GetFilePath(productId);
                if (!System.IO.File.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                foreach (var file in fileColection)
                {
                    string imagepath = Filepath + "\\" + file.FileName;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        passcount++;
                    }
                }
            }
            catch (Exception ex)
            {
                errorcount++;
                response.ErrorMessage = ex.Message;
            }
            response.ResponseCode = 200;
            response.Result = passcount + " File upload & " + errorcount + " file failed";
            return Ok(response);
        }
        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string productId)
        {
            string imageUrl = string.Empty;
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilePath(productId);
                string imagepath = Filepath + "\\" + productId + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    imageUrl = hostUrl + "/Upload/Product/" + productId + "/" + productId + ".png";
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {

            }
            return Ok(imageUrl);
        }
        [Authorize]
        [HttpGet("GetMultiImage")]
        public async Task<IActionResult> GetMultiImage(string productId)
        {
            List<string> imageUrl = new List<string>();
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilePath(productId);
                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string filename = fileInfo.Name;
                        string imagepath = Filepath + "\\" + filename;
                        if (System.IO.File.Exists(imagepath))
                        {
                            string _imageUrl = hostUrl + "/Upload/Product/" + productId + "/" + filename;
                            imageUrl.Add(_imageUrl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(imageUrl);
        }
        [Authorize]
        [HttpGet("RemoveImage")]
        public async Task<IActionResult> RemoveImage(string productId, string url)
        {
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            int passcount = 0;
            try
            {
                string Filepath = GetFilePath(productId);
                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string filename = fileInfo.Name;
                        string imagepath = Filepath + "\\" + filename;
                        if (System.IO.File.Exists(imagepath))
                        {
                            string _imageUrl = hostUrl + "/Upload/Product/" + productId + "/" + filename;
                            if(String.Compare(url,_imageUrl) == 0)
                            {
                                passcount++;
                                System.IO.File.Delete(imagepath);
                            }
                        }
                    }
                    if(passcount != 0)
                    {
                        return Ok("pass");
                    }
                    else
                    {
                        return NotFound("image không tồn tại");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("RemoveMultiImage")]
        public async Task<IActionResult> RemoveMultiImage(string productId)
        {
            try
            {
                string Filepath = GetFilePath(productId);
                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        fileInfo.Delete();
                    }
                    return Ok("pass");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost("UpAndGetImageUrl")]
        public async Task<IActionResult> UpAndGetImageUrl(IFormFileCollection fileColection, string productId)
        {
            List<string> imageUrl = new List<string>();
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilePath(productId);
                if (!System.IO.File.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                foreach (var file in fileColection)
                {
                    string imagepath = Filepath + "\\" + file.FileName;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    string _imageUrl = hostUrl + "/Upload/Product/" + productId + "/" + file.FileName;
                    imageUrl.Add(_imageUrl);
                }
            }
            catch (Exception ex)
            {
                
            }
            return Ok(imageUrl);
        }
        [NonAction]
        public string GetFilePath(string productId)
        {
            return Path.GetFullPath("./wwwroot") + "\\Upload\\Product\\" + productId;
        }
    }
}
