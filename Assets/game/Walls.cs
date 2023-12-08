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
                if (walls.Any((tuple) => Equals(tuple.Item2, position) || Equals(tuple.Item1, position))) continue;
                if (board.Get(position) == Cell.WALL) continue;
                var neighbours = board.GetNeighbours(position);
                var neighboursAreWalls = neighbours.Count((pos) => board.Get(pos) == Cell.WALL);
                if (neighboursAreWalls != 2) continue;
                var w = neighbours.Where((pos) => board.Get(pos) == Cell.WALL).ToList();
                foreach (var neighbour in w)
                {
                    var wallsExceptThis =  w.Where((pos) => !Equals(pos, neighbour)).ToList();
                    if (wallsExceptThis.Any((pos) => pos.Column == neighbour.Column || pos.Row == neighbour.Row))
                    {
                        var other = wallsExceptThis.Find((pos) => pos.Column == neighbour.Column || pos.Row == neighbour.Row);
                        walls.Add(new Tuple<Position, Position>(neighbour, other));
                        break;
                    }
                }
            }

            return walls;
        }
    }
}