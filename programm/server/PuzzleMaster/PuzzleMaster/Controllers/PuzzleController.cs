using Microsoft.AspNetCore.Mvc;
using PuzzleMaster.model;
using PuzzleMaster.repository;
using System.Drawing;

namespace PuzzleMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PuzzleController : ControllerBase
    {

        private readonly IPictureRepository pictureRepository;

        public PuzzleController(IPictureRepository pictureRepository)
        {
            this.pictureRepository = pictureRepository;
        }


        /** <summary>Calculate and retrieve the puzzle pieces for the given settings.</summary>
         *  <param name="size">One-dimensional lenght of the squared gamefield.</param>
         *  <param name="pictureId">Unique identifier from a picture that is stored on the server. This picture will be used to create the puzzle pieces</param> 
         *  <param name="freeField">X/Y position of the puzzle piece which does not contain a picture. X/Y count starts at 0.</param> */
        [HttpGet("get")]
        public IActionResult getPuzzle(uint size, uint pictureId, int x, int y)
        {
            Picture picture = pictureRepository.getPictureById(pictureId);
            if (picture == null)
                return NoContent();

            Puzzlepiece[][] puzzle = PuzzleBuilder.createPuzzle(size, picture, new Point(x,y));
            return new OkObjectResult(puzzle);
        }

    }
}
