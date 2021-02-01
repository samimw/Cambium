using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Model
{
    public class RoverResponse
    {
        public string Name { get; set; }
        public int[] NewPositions { get; set; }
        public string Message { get; set; }
    }
}
