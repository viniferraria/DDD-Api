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
            try 
            {
                return _repo.GetAll();
            } catch (Exception e) 
            {
                Console.WriteLine(e);
                return new List<Zoo>();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Zoo>> GetAnimal(int id)
        {
            try
            {
                var animal = await _repo.GetById(id);
                if (animal != null)
                {
                    return animal;
                }
                return NotFound();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new BadRequestResult();
            }
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
            try
            {
                Zoo animal = await _repo.GetById(id);
                if (animal == null)
                {
                    return BadRequest();
                }
                await _repo.Remove(animal);
                return animal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("Read")]
        public async Task<ActionResult<string>> Read([FromForm] IFormFile file)
        {
            var dirName = "temp";
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), dirName);
            if(!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            if(file == null)
            {
                return BadRequest();
            }
            try
            {
                var filePath = Path.Combine(fullPath, $"{DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")}.txt");
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                string[] readFile = System.IO.File.ReadAllLines(filePath);
                for(int i = 0; i < readFile.Length; ++i)
                {
                    string[] formatedLine = readFile[i].Split(" ");
                    if (formatedLine.Length < 2)
                        continue;
                    await _repo.Add(new Zoo(formatedLine[0], formatedLine[1]));
                }
                // _repo.readFile(filePath);
                System.IO.File.Delete(filePath);
                return Ok(new { success = "File Read" });

            } catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }

        }
    }
}