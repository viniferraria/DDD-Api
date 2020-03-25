using Domain.Models;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("zoo")]
    [ApiController]
    public class ZooController : ControllerBase
    {
        private readonly ZooRepository _repo = new ZooRepository();

        // GET: api/File
        [HttpGet("")]
        public IEnumerable<Zoo> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Zoo>> GetAnimal(int id)
        {
            var animal = await _repo.GetById(id);
            if (animal != null)
            {
                return animal;
            }
            return NotFound();
        }


        [HttpPost("Add/")]
        public ActionResult<Zoo> Add(
            [FromBody] Zoo animal)
        {
            try
            {
                _repo.Add(animal);
                return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BadRequestResult();
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Zoo>> Delete(int id)
        {
            Zoo animal = await _repo.GetById(id);
            if (animal == null)
            {
                return BadRequest();
            }
            _repo.Remove(animal);
            return animal;

        }

        [HttpPatch("Update/{id}")]
        public ActionResult<Zoo> Patch(
            int id,
            [FromBody] Zoo animal)
        {
            // If model is not valid, return the problem
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != animal.Id)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(animal);
            }
            catch (Exception)
            {
                return NotFound();

            }
            return NoContent();
        }

        [HttpGet("Read")]
        public string Read(string path= @"C:\Users\Resource\Downloads\Animal.txt")
        {
            return _repo.readFile(path);
        }
    }
}
