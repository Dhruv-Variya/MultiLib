﻿using _NET.Data;
using _NET.Dtos.Movie;
using _NET.Dtos.User;
using _NET.models;
using _NET.Services.UserService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using models;

namespace _NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private static List<User> users = new List<User> {
            new User{ Username = "test", Password = "test"},
            new User{ Username = "test1", Password = "test1"}
         };
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]

        //public async Task<ActionResult<ServiceResponse<AuthUserDto>>> Authenticate(string username, string password)
        //http://localhost:5149/api/User/authenticate?username=string&password=string
        public async Task<ActionResult<ServiceResponse<AuthUserDto>>> Authenticate(AuthUserDto userObj)
        {

            return Ok(await _userService.Authenticate(userObj.Username, userObj.Password));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<List<RegisterUserDto>>>> RegisterUser(RegisterUserDto newUser)
        {

            return Ok(await _userService.RegisterUser(newUser));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }





    }
}
