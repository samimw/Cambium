using MarsRover.Business;
using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Factory
{
    public class RobotCreator : IRobotCreator
    {
        public List<RoverResponse> CreateAndMoveRobots(List<RoverRequest> roverInfo, int[,] plateau)
        {            
            List<RoverResponse> newRoverInfo = new List<RoverResponse>();
            foreach (var rv in roverInfo)
            {
                Robot rover = new Rover();
                var roverResponse = rover.Move(rv.Instructions, plateau);
                roverResponse.Name = rv.Name;
                newRoverInfo.Add(roverResponse);
            }

            return newRoverInfo;
        }
    }
}
