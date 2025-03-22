using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<IEnumerable<VillaModel>> GetAllAsync();
        Task<T> GetAsync<T>(int id);
        Task<T> CreatedAsync<T>(VillaCreateDTO dto);
        Task<T> UpdatedAsync<T>(VillaUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);

    }
}
