using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Models;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository nationalParkRepo, IMapper mapper)
        {
            _nationalParkRepo = nationalParkRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks() 
        {
            var parks = _nationalParkRepo.GetNationalParks();

            var dtos = new List<NationalParkDto>();
            foreach (var item in parks)
            {
                dtos.Add(_mapper.Map<NationalParkDto>(item));
            }

            return Ok(dtos);
        }

        /// <summary>
        /// Get national park by id
        /// </summary>
        /// <param name="nationalParkId"></param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int nationalParkId) 
        {
            var park = _nationalParkRepo.GetNationalPark(nationalParkId);

            if (park == null) return NotFound();

            var dto = _mapper.Map<NationalParkDto>(park);

            return Ok(dto);
        }

        /// <summary>
        /// Create national park
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto dto) 
        {
            if (dto == null) 
            {
                return BadRequest(ModelState);
            }

            if (_nationalParkRepo.IsNationalParkExists(dto.Name)) 
            {
                ModelState.AddModelError("", "National park exists!");
                return StatusCode(404, ModelState);
            }

            //if (!ModelState.IsValid) 
            //{
            //    return BadRequest(ModelState);
            //}

            var park = _mapper.Map<NationalPark>(dto);

            if (!_nationalParkRepo.CreateNationlPark(park)) 
            {
                ModelState.AddModelError("", $"Something went wrong when creating record with name {park.Name}!");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = park.Id }, park);
        }

        /// <summary>
        /// Update national park
        /// </summary>
        /// <param name="nationalParkId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto dto) 
        {
            if (dto == null || nationalParkId != dto.Id)
            {
                return BadRequest(ModelState);
            }

            var park = _mapper.Map<NationalPark>(dto);

            if (!_nationalParkRepo.UpdateNationalPark(park))
            {
                ModelState.AddModelError("", $"Something went wrong when updating record with id {park.Id}!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete national park
        /// </summary>
        /// <param name="nationalParkId">national park id</param>
        /// <returns></returns>
        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalParkRepo.IsNationalParkExists(nationalParkId))
                return NotFound();

            var park = _nationalParkRepo.GetNationalPark(nationalParkId);

            if (!_nationalParkRepo.DeleteNationalPark(park))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting record with id {park.Id}!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
