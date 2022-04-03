using PuzzleMaster.model;
using System.Collections.Generic;

namespace PuzzleMaster.repository
{
    public interface IPictureRepository
    {

        public void addPicture(byte[] picture);

        public List<Picture> getAllPictures();

        public Picture getPictureById(uint pictureId);

    }
}
