using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleMaster
{
    class Configuration
    {
        public string location = "http://localhost:5000/";
        string LoadValue()
        {
            return location;
        }

        void SaveValues(string key)
        {
            location = key;
        }

        void CreateConfigIfNotExist()
        {

        }
    }
}
