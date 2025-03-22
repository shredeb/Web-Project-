using AutoMapper;
using MagicVlla_VillaAPI.Models;
using MagicVlla_VillaAPI.Models.Dto;
using MagicVlla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace MagicVlla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber,IMapper mapper)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper=mapper;
            _response = new();
            
        }

        //creating the http get all 
        [HttpGet]
        
        public async Task<ActionResult<APIResponse>> GetVillNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberlist = await _dbVillaNumber.GetAllAsync(includeProperties:"Villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberlist);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
               // _response.IsSuccess = false;
                _response.ErrorMassages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }

        }

        // get resource by ID
        [HttpGet("{id:int}", Name = "GetVillaNumberByID")]
        public async Task<ActionResult<APIResponse>> GetVillaNumberByID(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();

                }
                var villa = await _dbVillaNumber.GetAsync(u => u.villaNo == id);
                if (villa == null)
                {
                    return NoContent();

                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
               // _response.IsSuccess = false;
                _response.ErrorMassages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }

        }

        //now delete operation 
        [HttpDelete("{id:int}",Name = "DeleteVillaNumber")]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villanumber = await _dbVillaNumber.GetAsync(u => u.villaNo == id);
                if (villanumber == null)
                {
                    return NoContent();
                }
                await _dbVillaNumber.RemoveAsync(villanumber);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);

            }
            catch (Exception ex) { 
               // _response.IsSuccess=false;
                _response.ErrorMassages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;

            }
        }


        // now post request 
        [HttpPost("CreateVillaNumber")]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberDTO villadto)
        {
            try
            {
                if (villadto == null)
                {
                    return BadRequest();
                }
                var villanumModel = _mapper.Map<VillaNumber>(villadto);
                await _dbVillaNumber.CreateAsync(villanumModel);
                _response.Result = _mapper.Map<VillaNumberDTO>(villanumModel);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVillaNumberByID", new { id = villanumModel.villaNo }, _response);

            }
            catch (Exception ex)
            {
               // _response.IsSuccess = false;
                _response.ErrorMassages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;

            }
        }

        // now thw put request  to update any response
        [HttpPut("{id:int}", Name = "UpdateResponse")]


        public async Task<ActionResult<APIResponse>> UpdateResponse(int id, [FromBody] VillaNumberDTO villanumberdto)
        {
            try
            {
                if (villanumberdto == null || id != villanumberdto.villaNo)
                {
                    return BadRequest();
                }
                VillaNumber model = _mapper.Map<VillaNumber>(villanumberdto);
                await _dbVillaNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(0);


            }
        
             catch (Exception ex){
              //  _response.IsSuccess = false;
                _response.ErrorMassages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;

            }
            
        }



    }
}
