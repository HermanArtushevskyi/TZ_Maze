using _Project.CodeBase.Runtime.Services.UIService.Common;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.Items
{
    public class InteractableScroll : InteractableBase
    {
        [SerializeField] [TextArea] private string _text;
        private GameUIActions _gameUIActions;

        [Inject]
        private void Construct(GameUIActions gameUIActions)
        {
            _gameUIActions = gameUIActions;
        }
        
        public override void Interact()
        {
            _gameUIActions.CallOpenScroll(_text);
        }
    }
}