using System;
using board;
using game;
using UnityEngine;

namespace ui
{
    public class GameDrawer
    {
        private readonly BoardDrawer _board;
        private readonly Game _game;
        private readonly Transform player;

        public GameDrawer(Prefabs prefabs, Maze maze)
        {
            _game = new Game(maze.board);
            _board = new BoardDrawer(prefabs, _game);
            player = prefabs.Player;
        }

        public void Destroy()
        {
            _board.Destroy();
        }

        public void StartNewGame(GameObject gameObject)
        {
            _board.Draw(gameObject);
            Console.Error.Write("Player position: " + _game.GetPacmanPosition());
            player.position =
                CellPositionCalculator.GetCameraFrom(_game.GetPacmanPosition(), _game.GetBoard().GetRectangleOfBoard());
        }
    }
}