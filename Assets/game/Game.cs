using System.Linq;
using board;
using game.initializers;

namespace game
{
    public class Game
    {
        private readonly GameState _state;
        private Direction _currentDirection;

        public Game(Board board) : this(board, new PacmanRandomInitializer())
        {
        }

        private Game(Board board, IPacmanInitializer initializer)
        {
            var initialPosition = initializer.GetInitialPosition(board);
            var boardWithFood = GameBoardCreator.FillWithFood(board, initialPosition);
            var boardWithFoodAndBigFood = GameBoardCreator.FillWithBigFoods(boardWithFood, initialPosition);
            _state = new GameState(boardWithFoodAndBigFood, initialPosition);
            _currentDirection = initializer.GetInitialDirection(_state);
        }

        public void SetDirection(Direction direction)
        {
            _currentDirection = direction;
        }

        public bool Move()
        {
            if (!_state.CanApply(_currentDirection)) return false;
            _state.Move(_currentDirection);
            return true;
        }

        public bool HasFinished()
        {
            return _state.HasFinished();
        }

        public Position GetPacmanPosition()
        {
            return _state.GetPacmanPosition();
        }

        public Board GetBoard()
        {
            return _state.GetBoard();
        }

        public int GetFood()
        {
            return _state.GetBoard().GetCells().SelectMany(row => row).Count(cell => cell == Cell.FOOD);
        }

        public int GetBigFood()
        {
            return _state.GetBoard().GetCells().SelectMany(row => row).Count(cell => cell == Cell.BIG_FOOD);
        }

        public Direction GetCurrentDirection()
        {
            return _currentDirection;
        }
    }
}