using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Hippra.Data;
using Hippra.Code;
using AutoMapper;
using Hippra.Models.POCO;

namespace Hippra.API
{

    [ApiController]
    [Route("api/[controller]")]
    public class AutoMapTestController : ControllerBase //Controller
    {
        private readonly IMapper _mapper;

        public AutoMapTestController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var autoMapTest = new AutoMapTest
            {
                ID = 1,
                Name = "Test",
                Value = "Data"
            };

            AutoMapTestVM testViewModel = _mapper.Map<AutoMapTestVM>(autoMapTest);

            return new JsonResult(testViewModel);
        }
    }
}
