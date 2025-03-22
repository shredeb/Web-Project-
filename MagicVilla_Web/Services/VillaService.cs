using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using static MagicVilla_Web.Models.newApiResponse;

namespace MagicVilla_Web.Services
{
    public class VillaService : IVillaService
    {
        //public readonly  IHttpClientFactory _clientFactory;
        private string villaUrl;

        public VillaService(IConfiguration configuration) 
        {
            //_clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
            
        }

        public Task<T> CreatedAsync<T>(VillaCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VillaModel>> GetAllAsync()
        {
            using (var httpClient = new HttpClient())
            {
                //using (var response = await httpClient.GetAsync($"{villaUrl}//api/villaAPI"))
                using (var response = await httpClient.GetAsync("http://localhost:5038/api/VillaAPI"))
                {
                    string newApiResponse = await response.Content.ReadAsStringAsync();
                    var apiResult = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<VillaModel>>>(newApiResponse);
                    return apiResult.Result;
                }
            }
        }

        public Task<T> GetAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdatedAsync<T>(VillaUpdateDTO dto)
        {
            throw new NotImplementedException();
        }

        //public Task<T> CreatedAsync<T>(VillaCreateDTO dto)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.POST,
        //        Data = dto,
        //        Url = villaUrl + "/api/villaAPI"

        //    });


        //}

        //public Task<T> DeleteAsync<T>(int id)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.DELETE,
        //       // Data = dto,
        //        Url = villaUrl + "/api/villaAPI/"+id

        //    });
        //}

        //public Task<T> GetAllAsync<T>()
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.GET,
        //       // Data = dto,
        //        Url = villaUrl + "/api/villaAPI"

        //    });
        //}

        //public Task<T> GetAsync<T>(int id)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.GET,
        //       // Data = dto,
        //        Url = villaUrl + "/api/villaAPI"+id

        //    });
        //}

        //public Task<T> UpdatedAsync<T>(VillaUpdateDTO dto)
        //{
        //    return SendAsync<T>(new APIRequest() {
        //        ApiType = SD.ApiType.PUT,
        //        Data=dto,
        //        Url = villaUrl + "/api/villaAPI" + dto.Id
        //    });
        //}
    }
}
