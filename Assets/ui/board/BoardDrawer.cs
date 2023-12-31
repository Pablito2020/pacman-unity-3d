#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using board;
using game;
using UnityEngine;

namespace ui
{
    public class BoardDrawer
    {
        private const int numberOfWalls = 3;
        private readonly Board board;
        private readonly List<List<GameObject>> cells = new();
        private readonly Prefabs prefabs;
        private readonly List<GameObject> walls = new();

        public BoardDrawer(Prefabs prefabs, Game game)
        {
            this.prefabs = prefabs;
            board = game.GetBoard();
        }

        public void Draw(GameObject gameObject)
        {
            AddFloor(gameObject);
            AddBreakableWalls(gameObject);
            AddBoard(gameObject);
        }

        private void AddFloor(GameObject gameObject)
        {
            var boardSize = board.GetRectangleOfBoard();
            prefabs.Corridor.Translate(CellPositionCalculator.FromFloor(new Position(0, 0), boardSize));
        }

        private void AddBreakableWalls(GameObject gameObject)
        {
            var walls = new Walls(board);
            var randomWalls = walls.GetRandomWalls(numberOfWalls);
            foreach (var (position, neighbour) in randomWalls)
            {
                var wall = prefabs.Instantiate(prefabs.BreakableWall);
                wall.SetActive(true);
                wall.transform.position = CellPositionCalculator.CalculateWall(position, neighbour);
                if (position.Column == neighbour.Column)
                    wall.transform.Rotate(0, 90, 0);
                wall.transform.parent = gameObject.transform;
                this.walls.Add(wall);
            }
        }

        private void AddBoard(GameObject gameObject)
        {
            var boardSize = board.GetRectangleOfBoard();
            var boardPrimitives = board.GetCells();
            foreach (var (rowOfCells, rowIndex) in boardPrimitives.Select((value, i) => (value, i)))
            {
                var row = new List<GameObject>();
                foreach (var (cell, columnIndex) in rowOfCells.Select((value, i) => (value, i)))
                {
                    var position = new Position(rowIndex, columnIndex);
                    var prefab = GetPrefab(cell, position, boardSize, gameObject);
                    if (prefab != null) row.Add(prefab);
                }

                cells.Add(row);
            }
        }


        private GameObject? GetPrefab(Cell cell, Position position, Rectangle boardSize, GameObject gameObjectAttached)
        {
            var prefabsFromCell = GetPrefab(cell);
            if (prefabsFromCell == null) return null;
            prefabsFromCell.SetActive(true);
            var prefabPosition = cell == Cell.WALL
                ? CellPositionCalculator.From(position, boardSize)
                : CellPositionCalculator.FromFruit(position, boardSize);
            prefabsFromCell.transform.position = prefabPosition;
            if (cell == Cell.WALL)
                prefabsFromCell.transform.localScale = CellPositionCalculator.getBlockSize(boardSize);
            prefabsFromCell.transform.parent = gameObjectAttached.transform;
            return prefabsFromCell;
        }

        private GameObject? GetPrefab(Cell cell)
        {
            return cell switch
            {
                Cell.PATH => null,
                Cell.WALL => prefabs.Instantiate(prefabs.Wall),
                Cell.FOOD => prefabs.Instantiate(prefabs.Food),
                Cell.BIG_FOOD => prefabs.Instantiate(prefabs.BigFood),
                _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, null)
            };
        }

        public void Destroy()
        {
            cells.ForEach(row => row.ForEach(cell => prefabs.Destroy(cell)));
            cells.Clear();
            walls.ForEach(wall => prefabs.Destroy(wall));
            walls.Clear();
        }
    }
}