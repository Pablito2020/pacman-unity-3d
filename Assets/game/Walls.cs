using System;
using System.Collections.Generic;
using System.Linq;
using board;

namespace game
{
    public class Walls
    {
        private readonly Board board;

        public Walls(Board board)
        {
            this.board = board;
        }

        public IEnumerable<Tuple<Position, Position>> GetRandomWalls(int numberOfWalls)
        {
            var random = new Random();
            var walls = new List<Tuple<Position, Position>>();
            while (walls.Count < numberOfWalls)
            {
                var row = random.Next(board.GetRows());
                var column = random.Next(board.GetColumns());
                var position = new Position(row, column);
                if (board.Get(position) == Cell.WALL) continue;
                if (walls.Any((tuple) => Equals(tuple.Item1, position) || Equals(tuple.Item2, position))) continue;
                var neighbours = board.GetNeighbours(position);
                var validPositions = neighbours.Where((pos) => board.Get(pos) != Cell.WALL).ToList();
                if (validPositions.Count == 0) continue;
                var neighbour = validPositions[random.Next(validPositions.Count)];
                if (walls.Any((tuple) => Equals(tuple.Item1, neighbour) || Equals(tuple.Item2, neighbour))) continue;
                walls.Add(new Tuple<Position, Position>(position, neighbour));
            }
            return walls;
        }
        
    }
}