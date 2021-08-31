using HPCAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipsController : ControllerBase
    {
        private readonly IShipRepository _shipRepository;

        private readonly ILogger<ShipsController> _logger;

        public ShipsController(IShipRepository shipRepository, ILogger<ShipsController> logger)
        {
            this._shipRepository = shipRepository;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                return Ok(await _shipRepository.GetShips());
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Data retrival failed from the database. {e}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetShip(int id)
        {
            try
            {
                var result = await _shipRepository.GetShip(id);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ship {id} retrival failed from the database. {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateShip(Ship ship)
        {
            try
            {
                if (ship == null)
                    return BadRequest();

                var createdShip = await _shipRepository.AddShip(ship);
                return CreatedAtAction(nameof(GetShip), new { id = createdShip.ShipId }, createdShip);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create ship {ship.ShipId} record. {e}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ship>> UpdateShip(int id, Ship ship)
        {
            try
            {
                if (id != ship.ShipId)
                    return BadRequest();

                var shipToUpdate = await _shipRepository.GetShip(id);

                if (shipToUpdate == null)
                {
                    return NotFound($"Ship {id} is not found.");
                }

                return await _shipRepository.UpdateShip(ship);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update ship {id} record. {e}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShip(int id)
        {
            try
            {
                var shipToDelete = await _shipRepository.GetShip(id);

                if (shipToDelete == null)
                {
                    return NotFound($"Ship {id} is not found.");
                }

                await _shipRepository.DeleteShip(id);
                return Ok("{\"status\":\"Ship "+id+" has been deleted!\"}");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to delete ship {id} record. {e}");
            }
        }
    }
}
