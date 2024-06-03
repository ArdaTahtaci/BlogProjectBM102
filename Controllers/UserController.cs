using BlogProject.Entities;
using BlogProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase{
        private readonly IUserService _userService;

        public UserController(IUserService userService){
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers(){
            try{
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(string id){
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null){
                    return NotFound($"User with id {id} not found");
                }
                return Ok(user);
            }
            catch (Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user){
            try{
                var createdUser = _userService.CreateUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser._id }, createdUser);
            }
            catch (Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
