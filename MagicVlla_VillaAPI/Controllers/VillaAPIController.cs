 using AutoMapper;
using MagicVlla_VillaAPI.Data;

using MagicVlla_VillaAPI.Models;
using MagicVlla_VillaAPI.Models.Dto;
using MagicVlla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVlla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        //private readonly ApplicationDBContext _db;
        protected APIResponse _response;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;



        public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
        {
            _dbVilla = dbVilla;
            _mapper = mapper;
            this._response = new();


        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {



                //creating end point
                IEnumerable<Villa> villalist = await _dbVilla.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaDTO>>(villalist);
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
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status208AlreadyReported)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillabyId(int id)
        {
            try
            {
                if (id == 0)
                {

                    return BadRequest();
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    return NoContent();
                }
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;


                return Ok(_response);
                //return Ok(_mapper.Map<VillaDTO>(villa));
            }
            catch (Exception ex)
            {
                //_response.IsSuccess = false;
                _response.ErrorMassages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }

        //creating post request
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status208AlreadyReported)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO villaDto)
        {
            try
            {
                if (villaDto == null)
                {
                    return BadRequest();
                }
                //if id is greater than zero then it is not a create request. as Id will generated autometically so id needs not to be inserted
                Villa model = _mapper.Map<Villa>(villaDto);
                //Villa model = new()
                //{
                //    Amenity = villaDto.Amenity,
                //    Description = villaDto.Description,
                //    Name = villaDto.Name,
                //    Occupancy = villaDto.Occupancy,
                //    Rate = villaDto.Rate,
                //    sqft = villaDto.sqft,

                //    ImageUrl = villaDto.ImageUrl,

                //};
                await _dbVilla.CreateAsync(model);

                //return Ok(villaDto);
                _response.Result = _mapper.Map<VillaDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;



                return CreatedAtRoute("GetVilla", new { id = model.Id }, _response);
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
        // creating a delete request

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    return NoContent();
                }
                await _dbVilla.RemoveAsync(villa);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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


        //creating a put request
        [HttpPut("{id:int}", Name = "UpdateWithPut")]
        public async Task<ActionResult<APIResponse>> UpdateWithPut(int id, [FromBody] VillaUpdateDTO villaDto)
        {
            try
            {
                if (villaDto == null || id != villaDto.Id)
                {
                    return BadRequest();
                }
                Villa model = _mapper.Map<Villa>(villaDto);
             
                await _dbVilla.UpdateAsync(model);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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

                            //creating a patch request
                            //patch is used to update only the specficiq property
                            [HttpPatch("{id:int}", Name = "UpdateWithPatch")]
                            [ProducesResponseType(StatusCodes.Status204NoContent)]
                            [ProducesResponseType(StatusCodes.Status400BadRequest)]

                            public async Task<IActionResult> UpdateWithPatch(int id, JsonPatchDocument<VillaUpdateDTO> patchDto)
                            {
                                if (patchDto == null || id == 0)
                                {
                                    return BadRequest();
                                }
                                var villa = await _dbVilla.GetAsync(u => u.Id == id, tracked: false);
                                VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(patchDto);


                                //VillaUpdateDTO villaDTO = new()
                                //{
                                //    Amenity = villa.Amenity,
                                //    Description = villa.Description,
                                //    Name = villa.Name,
                                //    Occupancy = villa.Occupancy,
                                //    Rate = villa.Rate,
                                //    sqft = villa.sqft,
                                //    Id = villa.Id,
                                //    ImageUrl = villa.ImageUrl,

                                //};
                                if (villa == null) { return BadRequest(); }
                                patchDto.ApplyTo(villaDTO, ModelState);
                                Villa model = _mapper.Map<Villa>(villaDTO);
                                //Villa model = new Villa()
                                //{
                                //    Amenity = villa.Amenity,
                                //    Description = villa.Description,
                                //    Name = villa.Name,
                                //    Occupancy = villa.Occupancy,
                                //    Rate = villa.Rate,
                                //    sqft = villa.sqft,
                                //    Id = villa.Id,
                                //    ImageUrl = villa.ImageUrl,

                                //};
                                await _dbVilla.UpdateAsync(model);
                                if (!ModelState.IsValid)
                                {
                                    return BadRequest();
                                }
                                return NoContent();
                            }





                        }
                    }
