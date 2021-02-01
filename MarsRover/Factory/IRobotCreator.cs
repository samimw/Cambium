using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Factory
{
    public interface IRobotCreator
    {
       public List<RoverResponse> CreateAndMoveRobots(List<RoverRequest> roverInfo, int[,] plateau);
    }
}
