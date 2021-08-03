﻿using System;
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
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository nationalParkRepo, IMapper mapper)
        {
            _nationalParkRepo = nationalParkRepo;
            _mapper = mapper;
        }

        [HttpGet]
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

        [HttpGet("{nationalParkId:int}")]
        public IActionResult GetNationalPark(int nationalParkId) 
        {
            var park = _nationalParkRepo.GetNationalPark(nationalParkId);

            if (park == null) return NotFound();

            var dto = _mapper.Map<NationalParkDto>(park);

            return Ok(dto);
        }

        [HttpPost]
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

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var park = _mapper.Map<NationalPark>(dto);

            if (!_nationalParkRepo.CreateNationlPark(park)) 
            {
                ModelState.AddModelError("", $"Something went wrong when saving record with name {park.Name}!");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
