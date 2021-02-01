using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Business
{
    public enum Robots { ROVER = 1 }

    public abstract class Robot
    {
        public Robots Type { get; set; }
        public string Instructions { get; set; }
        public abstract RoverResponse Move(string instructions, int[,] plateau);
    }
}
