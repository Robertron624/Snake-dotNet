using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class GameState
    {
           
        public int Rows { get; set; }
        public int Columns { get; set; }
        public GridValue[,] Grid { get; set; }
        public Direction Direction { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
        public  readonly Random random = new Random();

        public GameState(int rows, int columns) { 
        
            Rows = rows;
            Columns = columns;
            Grid = new GridValue[rows,columns];
            Direction = Direction.Right;
        }

        public void AddSnake()
        {
            int r = Rows / 2;
        }
}
}