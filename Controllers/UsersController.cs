using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZipPay.Data;
using ZipPay.Dtos;
using ZipPay.Models;

namespace ZipPay.Controllers
{

    
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase 
    {
        private readonly IZipPayRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IZipPayRepo repository, IMapper mapper)
        {            
            _repository = repository;
            _mapper = mapper;
        }        

        [HttpGet]
        public ActionResult <IEnumerable<UserReadDto>> GetUsers()
        {
            var users = _repository.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        [HttpGet("{id}", Name="GetUser")]
        public ActionResult <UserReadDto> GetUser(int id)
        {
            var user = _repository.GetUser(id);
            if (user != null)
            {                        
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            return NotFound();

        }

        [HttpPost]
        public ActionResult <UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);            
            try {
                _repository.CreateUser(userModel);
            }
            catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
            
            
            _repository.SaveChanges();

            

            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return CreatedAtRoute(nameof(GetUser), new {Id = userReadDto.Id}, userReadDto);
            
        }

    }
    
}