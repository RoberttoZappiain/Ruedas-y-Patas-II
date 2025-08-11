using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RuedaYPatas.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RuedaYPatas.Services
{
    public class LocalImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalImageService(IWebHostEnvironment webHostEnvironment) { _webHostEnvironment = webHostEnvironment; }

        public async Task<string> GuardarImagenAsync(IFormFile imagenFile)
        {
            return await GuardarImagenAsync(imagenFile, "mascotas");
        }

        public async Task<string> GuardarImagenAsync(IFormFile imagenFile, string subfolder)
        {
            if (imagenFile == null || imagenFile.Length == 0) return null;

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", subfolder);
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imagenFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                // La corrección está aquí: Abrimos el stream del archivo antes de copiarlo.
                await imagenFile.OpenReadStream().CopyToAsync(fileStream);
            }

            return $"/uploads/{subfolder}/{uniqueFileName}";
        }
    }
}