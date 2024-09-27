using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using InputService.Common;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    public class CharacterController : ICharacterController
    {
        public IPlayer Player { get; private set; }

        private Rigidbody _rigidbody;
        private readonly IInputProvider _inputProvider;
        private readonly IUpdate _update;

        public CharacterController(IInputProvider inputProvider, IUpdate update)
        {
            _inputProvider = inputProvider;
            _update = update;
        }

        public void SetPlayer(IPlayer player)
        {
            Player = player;

            if (player == null) return;
            _rigidbody = player.SceneObject.GetComponent<Rigidbody>();
            _update.OnUpdate += OnUpdate;
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

        private void OnUpdate()
        {
            Vector3 cameraLookVector = Player.Camera.transform.forward;
            cameraLookVector.y = 0;
            cameraLookVector.Normalize();

            Vector3 cameraRightVector = Player.Camera.transform.right;
            cameraRightVector.y = 0;
            cameraRightVector.Normalize();

            RawInput inputData = _inputProvider.GetInput();
            Vector3 movementInput = new Vector3(inputData.MovementDirection.x, 0, inputData.MovementDirection.y);
            movementInput.Normalize();
            
            float speed = inputData.IsRunning ? Player.Stats.RunSpeed : Player.Stats.Speed;
            movementInput *= speed;
            
            Vector3 movement = (cameraRightVector * movementInput.x) + (cameraLookVector * movementInput.z);
            
            Move(movement);
        }

        private void Move(Vector3 movement)
        {
            _rigidbody.AddForce(movement, ForceMode.Impulse);
        }
    }
}