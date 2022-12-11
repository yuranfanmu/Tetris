using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Shape
    {
        bool[,] shapeMatrix;
        int x, y;
        Color color;
        public Shape(int xPosition)
        {
            x = xPosition;
            y = 0;
            Random r = new Random();
            switch (r.Next(1,8))
            {
                case 1: // квадрат
                    shapeMatrix = new bool[,] { { true, true }, { true, true } };
                    color = Color.Red;
                    Console.WriteLine(1);
                    break;
                case 2: // палка
                    shapeMatrix = new bool[,] { { true, true, true, true }};
                    color = Color.Magenta;
                    break;
                case 3: // Г
                    shapeMatrix = new bool[,] { { true, true }, { true, false }, { true, false } };
                    color = Color.Blue;
                    break;
                case 4: // Г обратная
                    shapeMatrix = new bool[,] { { true, true }, { false, true }, { false, true } };
                    color = Color.Blue;
                    break;
                case 5: // Z
                    shapeMatrix = new bool[,] { { true, false }, { true, true }, { false, true } };
                    color = Color.DarkGreen;
                    break;
                case 6: // Z обратная
                    shapeMatrix = new bool[,] { { false, true }, { true, true }, { true, false } };
                    color = Color.DarkGreen;
                    break;
                case 7: // пирамида
                    shapeMatrix = new bool[,] { { true, false }, { true, true }, { true, false } };
                    color = Color.Brown;
                    break;
            }
        }
        public bool[,] getMatrix()
        {
            return shapeMatrix;
        }
        public void moveDown()
        {
            y++;
        }

        public void moveRight()
        {
            x++;
        }
        public void moveLeft()
        {
            x--;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public Color getColor()
        {
            return color;
        }

        public void turnShape()
        {
            bool[,] temp = new bool[shapeMatrix.GetLength(1), shapeMatrix.GetLength(0)];
            //bool temp = shapeMatrix[0, 0];
            for (int i = 0; i < shapeMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < shapeMatrix.GetLength(0); j++)
                {
                    temp[i, j] = shapeMatrix[j, shapeMatrix.GetLength(1) - i - 1];
                }
            }
            shapeMatrix = temp;
        }
    }
}
