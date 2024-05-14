using Math4FunBackedn.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Math4FunBackedn.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        private readonly IConfiguration _configuration;
        public UploadController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadMedia([FromForm] MediaUploadDTO dto)
        {
            try
            {
                List<string> pathList = new();

                foreach (var item in dto.file)
                {
                    if (dto.file != null && item.Length > 0 && (item.Length / 1048576) <= int.Parse(_configuration["ImageProfile:MaxSize"]))
                    {

                        var folderName = _configuration.GetValue<string>("Storage") ;
                        var fileName = Path.GetFileName(item.FileName);
                        var folder = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        var name = $"{fileName.Split(".")[0]}_{DateTime.Now.Ticks}_ORIGIN.png";
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                        var filePath = Path.Combine(folder, name);

                        using (var fileSteam = new FileStream(filePath, FileMode.Create))
                        {
                            await item.CopyToAsync(fileSteam);
                        }
                        var path = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{_configuration.GetValue<string>("Domain")}/{name}";
                        var namepath = path.Split('/').Last();
                        pathList.Add(path);
                    }
                }
                return Ok(pathList);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }


    }
}
