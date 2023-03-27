using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameEngine
    {
        public uint CurrentGeneration { get; private set; }
        public byte[,] field;
        private readonly int rows;
        private readonly int cols;

        public GameEngine(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new byte[cols, rows];
            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = (byte)(random.Next(density) == 0 ? 1 : 0);

                }
            }
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = (byte)((field[x, y] & 1) + (((field[(x - 1 + cols) % cols, (y - 1 + rows) % rows] & 1) + (field[(x - 1 + cols) % cols,y] & 1) + (field[(x - 1 + cols) % cols, (y + 1 + rows) % rows] & 1) +
                        (field[x, (y - 1 + rows) % rows] & 1) + (field[x, (y + 1 + rows) % rows] & 1) +
                        (field[(x + 1 + cols) % cols, (y - 1 + rows) % rows] & 1) + (field[(x + 1 + cols) % cols, y] & 1) + (field[(x + 1 + cols) % cols, (y + 1 + rows) % rows] & 1)) << 1));
                }
            }
        }
        public void NextGeneration()
        {

            var newField = new byte[cols, rows];
            for (int x = 0; x < cols; x++)
            {

                for (int y = 0; y < rows; y++)
                {
                    if(field[x, y] != 0)
                    {
                        field[x, y] = (byte)((field[x, y] & 1) + (((field[(x - 1 + cols) % cols, (y - 1 + rows) % rows] & 1) + (field[(x - 1 + cols) % cols, y] & 1) + (field[(x - 1 + cols) % cols, (y + 1 + rows) % rows] & 1) +
                        (field[x, (y - 1 + rows) % rows] & 1) + (field[x, (y + 1 + rows) % rows] & 1) +
                        (field[(x + 1 + cols) % cols, (y - 1 + rows) % rows] & 1) + (field[(x + 1 + cols) % cols, y] & 1) + (field[(x + 1 + cols) % cols, (y + 1 + rows) % rows] & 1)) << 1));
                        var hasLife = field[x, y] & 1;
                        byte neighboursCount = (byte)(field[x, y] >> 1);
                        if (hasLife == 0 && neighboursCount == 3)
                        {
                            newField[x, y] = (byte)(field[x, y] + 1);
                            newField[(x - 1 + cols) % cols, (y - 1 + rows) % rows] += 2;
                            newField[(x - 1 + cols) % cols, y] += 2;
                            newField[(x - 1 + cols) % cols, (y + 1 + rows) % rows] += 2;
                            newField[x, (y - 1 + rows) % rows] += 2;
                            newField[x, (y + 1 + rows) % rows] += 2;
                            newField[(x + 1 + cols) % cols, (y - 1 + rows) % rows] += 2;
                            newField[(x + 1 + cols) % cols, y] += 2;
                            newField[(x + 1 + cols) % cols, (y + 1 + rows) % rows] += 2;

                        }
                        else if (hasLife == 1 && (neighboursCount < 2 || neighboursCount > 3))
                        {
                            newField[x, y] = (byte)(field[x, y] - 1);
                            newField[(x - 1 + cols) % cols, (y - 1 + rows) % rows] -= 2;
                            newField[(x - 1 + cols) % cols, y] -= 2;
                            newField[(x - 1 + cols) % cols, (y + 1 + rows) % rows] -= 2;
                            newField[x, (y - 1 + rows) % rows] -= 2;
                            newField[x, (y + 1 + rows) % rows] -= 2;
                            newField[(x + 1 + cols) % cols, (y - 1 + rows) % rows] -= 2;
                            newField[(x + 1 + cols) % cols, y] -= 2;
                            newField[(x + 1 + cols) % cols, (y + 1 + rows) % rows] -= 2;
                        }
                        else
                            newField[x, y] = field[x, y];
                    }
                    

                }
            }
            field = newField;
            CurrentGeneration++;
        }

        public void AddCell(int x, int y)
        {
            if(x >= 0 && y >= 0 && x < cols && y < rows)
            {
                field[x, y] = (byte)(field[x, y] - (field[x, y] & 1) + 1);
            }
        }
        public void RemoveCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < cols && y < rows)
            {
                field[x, y] = (byte)(field[x, y] - (field[x, y] & 1));
            }
        }
    }
}
