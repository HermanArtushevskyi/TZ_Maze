using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Common;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;

namespace _Project.CodeBase.Runtime.Services.UIService.Game
{
    public class GamePresenter
    {
        private readonly GameView _gameView;
        private readonly GameUIActions _gameUIActions;
        private readonly IPlayer _player;

        public GamePresenter(IView gameView, GameUIActions gameUIActions, IPlayer player)
        {
            _gameView = (GameView) gameView;
            _gameUIActions = gameUIActions;
            _player = player;
        }

        public void BindView()
        {
        }
    }
}