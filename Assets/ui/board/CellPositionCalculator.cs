using System;
using board;
using UnityEngine;

namespace ui
{
    public static class CellPositionCalculator
    {
        private const int CellSize = 8;
        private const int CellHeight = 8;

        public static Vector3 From(Position position, Rectangle board)
        {
            var column = position.Column * CellSize + CellSize / 2;
            var row = position.Row * CellSize + CellSize / 2;
            return new Vector3(column, CellHeight / 2, row);
        }

        public static Vector3 FromFloor(Position position, Rectangle board)
        {
            var column = position.Column * CellSize + board.Height * CellSize / 2;
            var row = position.Row * CellSize + board.Width * CellSize / 2;
            return new Vector3(column, 0, row);
        }

        public static Vector3 getBoardSize(Rectangle board)
        {
            return new Vector3(board.Height * CellSize, 0, board.Width * CellSize);
        }


        public static Vector3 getBlockSize(Rectangle board)
        {
            return new Vector3(CellSize, 9, CellSize);
        }


        public static Vector3 GetCameraFrom(Position position, Rectangle board)
        {
            var column = position.Column * CellSize + CellSize / 2;
            var row = position.Row * CellSize + CellSize / 2;
            return new Vector3(column, 1, row);
        }

        public static Vector3 FromFruit(Position position, Rectangle boardSize)
        {
            var column = position.Column * CellSize + CellSize / 2;
            var row = position.Row * CellSize + CellSize / 2;
            return new Vector3(column, 2, row);
        }

        public static Vector3 CalculateWall(Position position, Position neighbour)
        {
                var column = position.Column * CellSize + CellSize / 2;
                var row = position.Row * CellSize + CellSize / 2;
                var neighbourColumn = neighbour.Column * CellSize + CellSize / 2;
                var neighbourRow = neighbour.Row * CellSize + CellSize / 2;
                var x = (column + neighbourColumn) / 2;
                var z = (row + neighbourRow) / 2; 
                return new Vector3(x, 4, z);
        }
    }
}