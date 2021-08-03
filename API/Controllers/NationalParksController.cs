using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
