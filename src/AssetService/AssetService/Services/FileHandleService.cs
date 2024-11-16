using AssetService.Constants;
using AssetService.DependencyInjection.Extensions;
using AssetService.Enumerations;
using AssetService.Exceptions;
using AssetService.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using IO = System.IO;

namespace AssetService.Services
{
    [ApiController]
    public class FileHandleController : ControllerBase
    {
        private readonly string fileDirectory;
        private readonly string fileDirectoryPath;
        private readonly long maxFileSize;
        private readonly List<string> permittedExtensions = new() { ".png", ".jpg", ".jpeg", ".webp" };

        public FileHandleController(IWebHostEnvironment env, IConfiguration configuration)
        {
            fileDirectory = configuration["FileSettings:Directory"];
            fileDirectoryPath = Path.Combine(env.WebRootPath, fileDirectory);
            maxFileSize = configuration.GetValue<long>("FileSettings:MaxSize", 2 * 1024 * 1024);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        {
            try
            {
                // Check if request asset type is correct
                if (!Enum.IsDefined(typeof(AssetType), request.AssetType))
                    AppleShopException.ThrowIfValidation(MsgConst.INVALID_ASSET_TYPE.FormatMsg(nameof(request.AssetType)));

                string extension = Path.GetExtension(request.FileName).ToLowerInvariant();
                if (!permittedExtensions.Contains(extension))
                    AppleShopException.ThrowIfValidation(MsgConst.EXT_NOT_PERMITTED);

                IFormFile file = ConvertBase64ToFormFile(request.Content, request.FileName);
                string fileName = IO.File.Exists(GetFilePath((AssetType)request.AssetType, request.FileName))
                        ? request.FileName
                        : $"{((AssetType)request.AssetType).ToString().ToLower()}_{Guid.NewGuid().ToString("N").Substring(0, 6)}{extension}";

                string filePath = GetFilePath((AssetType)request.AssetType, fileName);

                await using (FileStream stream = new(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new
                {
                    Data = GetRelativePath((AssetType)request.AssetType, fileName),
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete")]
        public IActionResult DeleteFile([FromBody] DeleteFileRequest request)
        {
            try
            {
                if (!IO.File.Exists(request.Path))
                    return NotFound(new
                    {
                        Message = MsgConst.FILE_NOT_FOUND,
                        StatusCode = (int)HttpStatusCode.NotFound
                    });

                IO.File.Delete(request.Path);
                return Ok(new
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private string GetFolder(AssetType type)
        {
            return type switch
            {
                AssetType.CAT_ICON => "cat-icon",
                AssetType.PRODUCT_IMG => "prd-img",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private IFormFile ConvertBase64ToFormFile(string fileContent, string fileName)
        {
            byte[]? fileBytes = Convert.FromBase64String(fileContent);
            if (fileBytes == null || fileBytes.Length == 0) AppleShopException.ThrowIfValidation();
            if (fileBytes.Length > maxFileSize) AppleShopException.ThrowIfValidation();
            MemoryStream stream = new(fileBytes);
            return new FormFile(stream, 0, fileBytes.Length, "file", fileName);
        }

        private string GetFilePath(AssetType type, string fileName)
        {
            string typeFolder = GetFolder(type);
            string folderPath = Path.Combine(fileDirectoryPath, typeFolder);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, fileName).Replace("\\", "/");
        }

        private string GetRelativePath(AssetType type, string fileName)
        {
            string typeFolder = GetFolder(type);
            return Path.Combine(fileDirectory, typeFolder, fileName).Replace("\\", "/");
        }
    }
}
