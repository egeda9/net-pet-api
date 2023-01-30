using Microsoft.AspNetCore.Mvc;
using Pet.Api.Model;
using Pet.Api.Service;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            this._petService = petService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/<PetController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var pets = await this._petService.GetAsync();

                if (!pets.Any())
                    return NoContent();

                return Ok(pets);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<PetController>/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var pet = await this._petService.GetAsync(id);

                if (pet == null)
                    return NoContent();

                return Ok(pet);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        // POST api/<PetController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Model.Pet pet)
        {
            try
            {
                await this._petService.CreateAsync(pet);

                return Ok(pet);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pet"></param>
        /// <returns></returns>
        // PUT api/<PetController>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Model.Pet pet)
        {
            try
            {
                var updatedPet = await this._petService.UpdateAsync(id, pet);

                if (updatedPet == null)
                    return BadRequest($"Could not update pet with id: '{id}'");

                return Ok(updatedPet);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<PetController>/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await this._petService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
