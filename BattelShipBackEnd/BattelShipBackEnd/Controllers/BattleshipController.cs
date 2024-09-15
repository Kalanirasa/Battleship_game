using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattelShipBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleshipController : ControllerBase
    {
        private readonly IBattleshipService _battleshipService;

        public BattleshipController(IBattleshipService battleshipService)
        {
            _battleshipService = battleshipService;
        }
        [HttpPost("start")]
        public IActionResult StartNewGame()
        {
            var game = _battleshipService.StartNewGame();
            return Ok(game);
        }

        [HttpPost("turn")]
        public IActionResult TakeTurn([FromBody] MoveRequest request)
        {
            var result = _battleshipService.TakeTurn( request);
            return Ok(result);
        }
    }
}
