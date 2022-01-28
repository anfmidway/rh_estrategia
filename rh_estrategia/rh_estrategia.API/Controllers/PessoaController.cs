using Domain.Entities;
using Domain.Interfaces.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace rh_estrategia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        IRepository _repository;

        public PessoaController(IRepository repository)
        {
            this._repository = repository;
        }
        // GET: api/<PessoaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pessoas = _repository.GetAllPessoasAsync();

            return Ok(pessoas);
        }

        // GET api/<PessoaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByPessoaId(int id)
        {
            var result = _repository.GetPessoaById(id);
            if (result == null) return BadRequest("O pessoa não foi encontrado");
            return Ok(result);
        }

        // POST api/<PessoaController>
        [HttpPost]
        public IActionResult Post(Pessoa pessoa)
        {
            _repository.Add(pessoa);
            if (_repository.SaveChanges())
            {
                return Ok(pessoa);
            }
            return BadRequest("Pessoa Não encontrada");

        }

        // PUT api/<PessoaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Pessoa pessoa)
        {
            _repository.Update(pessoa);
            if (_repository.SaveChanges())
            {
                return Ok(pessoa);
            }
            return BadRequest("Pessoa Não Atualizado");

        }

        // DELETE api/<PessoaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pessoa = _repository.GetPessoaById(id);
            _repository.Delete(pessoa);
            if (_repository.SaveChanges())
            {
                return Ok(pessoa);
            }
            return BadRequest("Pessoa Não excluida");

        }
    }
}
