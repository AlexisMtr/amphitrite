﻿using Amphitrite.Dtos;
using Amphitrite.Models;
using Amphitrite.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Amphitrite.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AlarmsController : Controller
    {
        private readonly AlarmService alarmService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public AlarmsController(AlarmService alarmService, IMapper mapper, UserManager<User> userManager)
        {
            this.alarmService = alarmService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPut("{id}/ack")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AlarmDto))]
        public async Task<IActionResult> Ack(int id)
        {
            string userEmail = User.FindFirst(ClaimTypes.Email).Value;
            User user = await userManager.FindByEmailAsync(userEmail);

            Alarm alarm = alarmService.Ack(id, user);
            return Ok(mapper.Map<AlarmDto>(alarm));
        }
    }
}
