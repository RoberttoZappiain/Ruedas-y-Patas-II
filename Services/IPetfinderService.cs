using System.Collections.Generic;
using System.Threading.Tasks;
using RuedaYPatas.DTOs;

namespace RuedaYPatas.Services;

public interface IPetfinderService
{
    Task<IEnumerable<PetfinderBreed>> GetBreedsAsync(string animalType);
}