using MarsRover.Business;
using MarsRover.Factory;
using MarsRover.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoverController : ControllerBase
    {
        private readonly IRobotCreator robotCreator;

        public RoverController(IRobotCreator robotCreator)
        {
            this.robotCreator = robotCreator;
        }

        [HttpPost]
        public IActionResult Post([FromBody]List<RoverRequest> roverInfo)
        {
            var plateau = new Plateau().PopulatePlateau(5, 5);

            List<RoverResponse> newRoverInfo = robotCreator.CreateAndMoveRobots(roverInfo, plateau);

            return Ok(newRoverInfo);
        }
    }
}
