using Domain.Models;
using Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<ActionResult<Zoo>> Add(
            [FromBody] Zoo animal)
        {
            try
            {
                await _repo.Add(animal);
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
            await _repo.Remove(animal);
            return animal;

        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<Zoo>> Patch(
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
                await _repo.Update(animal);
            }
            catch (Exception)
            {
                return NotFound();

            }
            return NoContent();
        }

        [Consumes("multipart/form-data")]
        [HttpPost("Read")]
        public async Task<ActionResult<string>> Read([FromForm] IFormFile file)
        /*    string path = @"C:\Users\Resource\Downloads\Animal.txt" */
        {
            var filePath = Path.Combine(@"C:\Users\Resource\Downloads", $"{DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")}.txt");

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            try
            {
                string message = _repo.readFile(filePath);
                System.IO.File.Delete(filePath);
                return message;

            } catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}