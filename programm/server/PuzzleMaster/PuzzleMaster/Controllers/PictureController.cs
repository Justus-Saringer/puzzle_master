using Microsoft.AspNetCore.Mvc;
using PuzzleMaster.model;
using PuzzleMaster.repository;
using System.Collections.Generic;

namespace PuzzleMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PictureController : ControllerBase
    {

        private readonly IPictureRepository pictureRepository;

        public PictureController(IPictureRepository pictureRepository)
        {
            this.pictureRepository = pictureRepository;
        }

        /** <summary>Add a Picture to the database, that can be retrieved later.</summary>
         *  <param name="base64">The picture that will be added to the database. Format as a Base64 string</param> */
        [HttpPost("add")]
        public IActionResult addPicture([FromBody] byte[] base64)
        {
            Picture picture = new Picture(base64);
            pictureRepository.addPicture(picture.picture);
            return Ok();
        }

        /** <summary>Get a specific picture from the server.</summary> */
        [HttpGet("get/{id}")]
        public IActionResult getPictureById(uint id)
        {
            Picture picture = pictureRepository.getPictureById(id);
            return new OkObjectResult(picture);
        }

        /** <summary>Get all picture from the server. The quality of the images is reduced.</summary> */
        [HttpGet("get/all")]
        public IActionResult getAllPictures()
        {
            List<Picture> pictures = pictureRepository.getAllPictures();
            foreach (Picture pic in pictures)
                pic.lowerPictureQuality();
            return new OkObjectResult(pictures);
        }

    }
}
