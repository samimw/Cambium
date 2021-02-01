using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Business
{
    public class Rover : Robot
    {
        private const string MOVE = "M";
        private const string LEFT = "L";
        private const string RIGHT = "R";

        int move = 0;
        int sides = 0;
        private RoverResponse response;

        public Rover()
        {
            Type = Robots.ROVER;
            response = new RoverResponse();
            response.NewPositions = new[] { 0, 0 };
        }

        public override RoverResponse Move(string instructions, int[,] plateau)
        {
            Instructions = instructions;

            for (int i = 0; i < instructions.Length; i++)
            {
                string instruction = instructions[i].ToString();

                if (instruction == MOVE)
                {
                    move++;
                }
                else if (instruction == LEFT)
                {
                    if (sides > 0)
                    {
                        sides--;
                    }
                    else // when moving left and there is no plateau
                    {
                        response.NewPositions = new int[] { 0, 0 };
                        response.Message = "Cannot move left, no plateau!";
                        return response;
                    }
                }
                else if (instruction == RIGHT)
                {
                    sides++;
                }
            }

            return PlaceRoverOnPlateau(ref plateau, move, sides);
        }

        private RoverResponse PlaceRoverOnPlateau(ref int[,] plateau, int rowPosition, int colPosition)
        {
            if ((plateau.GetLength(0) <= rowPosition || plateau.GetLength(1) <= colPosition))
            {
                response.NewPositions = new int[] { 0, 0 };
                response.Message = "Cannot move, no plateau!";
                return response;
            }

            if (plateau[rowPosition, colPosition] != (int)Type)
            {
                plateau[rowPosition, colPosition] = (int)Type;
                response.NewPositions = new int[] { rowPosition, colPosition };
                response.Message = "Success";
            }
            else
            {
                GetLastPositionIfOccupied(ref plateau);
                response.Message = "Final position is already occupied by another rover!";
            }

            return response;
        }

        private void GetLastPositionIfOccupied(ref int[,] plateau)
        {
            var lastMoveRemoved = Instructions.Remove(Instructions.Length - 1);
            move = 0;
            sides = 0;
            Move(lastMoveRemoved, plateau);
        }
    }
}
