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
        
        private readonly LinkedList<Direction> dirChanges = new LinkedList<Direction>();
        public readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
        public  readonly Random random = new Random();

        public GameState(int rows, int columns) { 
        
            Rows = rows;
            Columns = columns;
            Grid = new GridValue[rows,columns];
            Direction = Direction.Right;

            AddSnake();
            AddFood();
        }

        public void AddSnake()
        {
            int r = Rows / 2;

            for(int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;
                snakePositions.AddFirst(new Position(r, c));
            }
        }

        private IEnumerable<Position> EmptyPositions()
        { 
            for(int r = 0; r < Rows; r++)
            { 
                for(int c = 0; c < Columns; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositions());

            if (empty.Count == 0)
            {
                return;
            }

            Position position = empty[random.Next(empty.Count)];
            Grid[position.Row, position.Column] = GridValue.Food;
        }

        public Position HeadPosition()
        {
            return snakePositions.First.Value;
        }

        public Position TailPosition()
        {
            return snakePositions.Last.Value;
        }

        public IEnumerable<Position> SnakePosition()
        {
            return snakePositions;
        }

        private void AddHead(Position position)
        {
            snakePositions.AddFirst(position);
            Grid[position.Row, position.Column] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = snakePositions.Last.Value;
            Grid[tail.Row, tail.Column] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        private Direction GetLastDirection()
        {
            return dirChanges.Count == 0 ? Direction : dirChanges.Last.Value;
        }

        private bool canChangeDirection(Direction newDir)
        {
            if(dirChanges.Count == 2)
            {
                return false;
            }

            Direction lastDirection = GetLastDirection();

            return newDir != lastDirection && newDir != lastDirection.Opposite();
        }

        public void ChangeDirection(Direction direction)
        {
            // Check if can change direction
            if (canChangeDirection(direction))
            {
                dirChanges.AddLast(direction);
            }
        }

        private bool IsOutsideGrid(Position position)
        {
            return position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns;
        }

        private GridValue WillHit(Position newHeadPosition)
        {
            if (IsOutsideGrid(newHeadPosition))
            {
                return GridValue.Outside;
            }

            if(newHeadPosition == TailPosition())
            {
                return GridValue.Empty;
            }

            return Grid[newHeadPosition.Row, newHeadPosition.Column];
        }

        public void Move()
        {

            if(dirChanges.Count > 0)
            {
                Direction = dirChanges.First.Value;
                dirChanges.RemoveFirst();
            }

            Position newHeadPosition = HeadPosition().Translate(Direction);
            GridValue hit = WillHit(newHeadPosition);

            if (hit == GridValue.Outside || hit == GridValue.Snake) 
            {
                GameOver = true;
            }
            else if(hit == GridValue.Empty) 
            {
                RemoveTail();
                AddHead(newHeadPosition);
            }
            else if(hit == GridValue.Food) 
            {
                AddHead(newHeadPosition);
                Score++;
                AddFood();
            }
        }
    }
}