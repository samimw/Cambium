using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Business
{
    public class Plateau : IPlateau
    {
        private int[,] _plateau;

        public int[,] PopulatePlateau(int width, int height)
        {
            _plateau = new int[width, height];
            return _plateau;
        }
    }
}
