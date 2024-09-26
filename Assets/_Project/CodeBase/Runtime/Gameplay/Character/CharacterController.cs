using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    public class CharacterController : ICharacterController
    {
        public IPlayer Player { get; private set; }
        
        private Rigidbody _rigidbody;

        public CharacterController(IPlayer player, IInputProvider inputProvider)
        {
            SetPlayer(player);
        }

        public void SetPlayer(IPlayer player)
        {
            Player = player;
        }

        public void Attack()
        {
            
        }

        public void Action()
        {
            
        }

        public void Hurt(int damage)
        {
        }

        public void Kill()
        {
        }
    }
}