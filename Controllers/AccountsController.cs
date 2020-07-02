using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZipPay.Data;
using ZipPay.Dtos;
using ZipPay.Models;

namespace ZipPay.Controllers
{

    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase 
    {
        private readonly IZipPayRepo _repository;
        private readonly IMapper _mapper;

        public AccountsController(IZipPayRepo repository, IMapper mapper)
        {            
            _repository = repository;
            _mapper = mapper;
        }        

        [HttpGet]
        public ActionResult <IEnumerable<AccountReadDto>> GetAccounts()
        {
            var accounts = _repository.GetAccounts();
            return Ok(_mapper.Map<IEnumerable<AccountReadDto>>(accounts));
        }

        [HttpGet("{id}", Name="GetAccount")]
        public ActionResult <AccountReadDto> GetAccount(int id)
        {
            var account = _repository.GetAccount(id);
            if (account != null)
            {                        
                return Ok(_mapper.Map<AccountReadDto>(account));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult <AccountReadDto> CreateAccount(AccountCreateDto accountCreateDto)
        {
            var accountModel = _mapper.Map<Account>(accountCreateDto);            
            try {
                _repository.CreateAccount(accountModel);
            }
            catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
            
            _repository.SaveChanges();

            var accountReadDto = _mapper.Map<AccountReadDto>(accountModel);

            return CreatedAtRoute(nameof(GetAccount), new {Id = accountReadDto.Id}, accountReadDto);
            
        }

    }
    
}