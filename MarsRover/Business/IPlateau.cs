using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Business
{
    public interface IPlateau
    {
        public int[,] PopulatePlateau(int width, int height);
    }
}
