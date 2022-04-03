using Microsoft.AspNetCore.Mvc;
using PuzzleMaster.model;
using PuzzleMaster.repository;
using System.Collections.Generic;

namespace PuzzleMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HighscoreController : ControllerBase
    {

        private readonly IHighscoreRepository highscoreRepository;

        public HighscoreController(IHighscoreRepository highscoreRepository)
        {
            this.highscoreRepository = highscoreRepository;
        }

        /** <summary>Add a highscore-entry to the database, that can be retrieved later.</summary>
         *  <param name="name">Name of the player, that archived this highscore.</param>
         *  <param name="moveCount">Number of moves that the player needed to end the game.</param> 
         *  <param name="fieldSize">One-dimensional lenght of the squared gamefield.</param> */
        [HttpGet("add")]
        public IActionResult add(string name, uint moveCount, uint fieldSize)
        {
            highscoreRepository.addHighscore(name, moveCount, fieldSize);
            return Ok();
        }

        /** <summary>Get all highscore-entrys, with a given size, from the server.</summary>
         *  <param name="size">One-dimensional lenght of the squared gamefield, for that you want to retrieve the highscore-entrys.</param> */
        [HttpGet("get/{size}")]
        public IActionResult getAllHighscoreBySize(uint size)
        {
            List<Highscore> list = highscoreRepository.getHighscoreBySize(size);
            return new OkObjectResult(list);
        }

        /** <summary>Get all highscore-entrys from the server.</summary> */
        [HttpGet("get/all")]
        public IActionResult getAllHighscore()
        {
            List<Highscore> list = highscoreRepository.getAllHighscores();
            return new OkObjectResult(list);
        }

    }
}
