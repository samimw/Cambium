using MarsRover.Business;
using MarsRover.Controllers;
using MarsRover.Factory;
using MarsRover.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class TestShould
    {
        [TestMethod]
        public void Return_Success_Result_For_Given_Rover()
        {
            // Arrange
            List<RoverRequest> roverInfoReq = new List<RoverRequest>
            {
              new RoverRequest { Name="Rover1", Instructions= "MRM"}
            };

            List<RoverResponse> roverInfoResp = new List<RoverResponse>
            {
              new RoverResponse { Name="Rover1", NewPositions = new int[] { 2, 1 }, Message = "Success" }
            };

            var robotCreator = new Mock<IRobotCreator>();
            robotCreator.Setup(repo => repo.CreateAndMoveRobots(roverInfoReq, new int[5, 5])).Returns(roverInfoResp);

            var sut = new RoverController(robotCreator.Object);

            //Act
            var result = sut.Post(roverInfoReq);

            //Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public void Return_Positions_For_Given_Rover()
        {
            // Arrange
            List<RoverRequest> roverInfoReq = new List<RoverRequest>
            {
              new RoverRequest { Name="Rover1", Instructions= "MRM"}
            };


            var sut = new RobotCreator();

            //Act
            var result = sut.CreateAndMoveRobots(roverInfoReq, new int[5, 5]);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.First().NewPositions[0]);
            Assert.AreEqual(1, result.First().NewPositions[1]);
        }


        [TestMethod]
        public void Not_Move_To_Occupied_Position()
        {
            // Arrange
            List<RoverRequest> roverInfoReq = new List<RoverRequest>
            {
               new RoverRequest { Name="Rover1", Instructions= "MM"},
               new RoverRequest { Name="Rover2", Instructions= "MM"}
            };


            var sut = new RobotCreator();

            //Act
            var result = sut.CreateAndMoveRobots(roverInfoReq, new int[5, 5]);

            //Assert
            Assert.IsNotNull(result);

            //rover1
            Assert.AreEqual(2, result.First(z => z.Name == "Rover1").NewPositions[0]);
            Assert.AreEqual(0, result.First(z => z.Name == "Rover1").NewPositions[1]);

            //rover2
            Assert.IsTrue(result.First(z => z.Name == "Rover2").Message.ToLower().Contains("occupied by another rover"));
            Assert.AreEqual(1, result.First(z => z.Name == "Rover2").NewPositions[0]);
            Assert.AreEqual(0, result.First(z => z.Name == "Rover2").NewPositions[1]);
        }

        [TestMethod]
        public void Not_Position_When_Out_Of_Range_Of_Plateau()
        {
            // Arrange
            List<RoverRequest> roverInfoReq = new List<RoverRequest>
            {
               new RoverRequest { Name="Rover1", Instructions= "L"},
               new RoverRequest { Name="Rover2", Instructions= "MMMMM"},
               new RoverRequest { Name="Rover3", Instructions= "RRRRR"}
            };


            var sut = new RobotCreator();

            //Act
            var result = sut.CreateAndMoveRobots(roverInfoReq, new int[5, 5]);

            //Assert
            Assert.IsNotNull(result);

            //rover1
            Assert.IsTrue(result.First(z => z.Name == "Rover1").Message.ToLower().Contains("no plateau"));
            Assert.AreEqual(0, result.First(z => z.Name == "Rover1").NewPositions[0]);
            Assert.AreEqual(0, result.First(z => z.Name == "Rover1").NewPositions[1]);

            //rover2
            Assert.IsTrue(result.First(z => z.Name == "Rover2").Message.ToLower().Contains("no plateau"));
            Assert.AreEqual(0, result.First(z => z.Name == "Rover2").NewPositions[0]);
            Assert.AreEqual(0, result.First(z => z.Name == "Rover2").NewPositions[1]);

            //rover3
            Assert.IsTrue(result.First(z => z.Name == "Rover3").Message.ToLower().Contains("no plateau"));
            Assert.AreEqual(0, result.First(z => z.Name == "Rover3").NewPositions[0]);
            Assert.AreEqual(0, result.First(z => z.Name == "Rover3").NewPositions[1]);
        }
    }
}
