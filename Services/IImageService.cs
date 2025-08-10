using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RuedaYPatas.Services
{
    public interface IImageService
    {
        Task<string> GuardarImagenAsync(IFormFile imagenFile);
    }
}