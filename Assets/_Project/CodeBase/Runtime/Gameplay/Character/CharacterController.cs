using System;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Items.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Traps.Interfaces;
using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Common;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using InputService.Common;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    public class CharacterController : ICharacterController
    {
        public event Action<string> OnDeath;
        
        public IPlayer Player { get; private set; }

        private Rigidbody _rigidbody;
        private readonly IInputProvider _inputProvider;
        private readonly IFixedUpdate _fixedUpdate;
        private readonly GameUIActions _gameUIActions;
        private readonly IAudioProvider _audioProvider;
        private readonly AudioName _audioNames;
        private readonly LayerMask _interactableLayerMask = LayerMask.GetMask(InteractableLayerName);
        
        private IAudioAsset _footstepsAudio;
        private float _lastPitch = 1f;
        
        private const float WalkPitch = 1f;
        private const float RunPitch = 1.5f;
        private const string InteractableLayerName = "Interactable";

        public CharacterController(IInputProvider inputProvider, IFixedUpdate fixedUpdate, GameUIActions gameUIActions,
            IAudioProvider audioProvider, AudioName audioNames)
        {
            _inputProvider = inputProvider;
            _fixedUpdate = fixedUpdate;
            _gameUIActions = gameUIActions;
            _audioProvider = audioProvider;
            _audioNames = audioNames;
        }

        public void SetPlayer(IPlayer player)
        {
            Player = player;

            if (player == null) return;
            _rigidbody = player.SceneObject.GetComponent<Rigidbody>();
            _fixedUpdate.OnFixedUpdate += OnFixedUpdate;
            _footstepsAudio = _audioProvider.Play(_audioNames.PlayerFootstepsSound, player.SceneObject);
            _footstepsAudio.Pause();
            player.SceneObject.GetComponent<ITriggerable>().OnTriggered += OnPlayerTriggered;
        }


        private void OnPlayerTriggered(Collider obj)
        {
            #if UNITY_EDITOR
            Debug.Log($"Player entered trigger: {obj.name}");
            #endif
            
            if (obj.TryGetComponent(out ITrap trap))
            {
                trap.Trigger();
                OnDeath?.Invoke(trap.GetDeathMessage());
            }
        }

        private void OnFixedUpdate()
        {
            if (Player.SceneObject == null)
            {
                _fixedUpdate.OnFixedUpdate -= OnFixedUpdate;
                return;
            }
            
            RawInput input = _inputProvider.GetInput();
            Move(input);
            CheckInteractions(input);
        }

        private void Move(RawInput input)
        {
            Vector3 cameraLookVector = Player.Camera.transform.forward;
            cameraLookVector.y = 0;
            cameraLookVector.Normalize();

            Vector3 cameraRightVector = Player.Camera.transform.right;
            cameraRightVector.y = 0;
            cameraRightVector.Normalize();

            RawInput inputData = input;
            Vector3 movementInput = new Vector3(inputData.MovementDirection.x, 0, inputData.MovementDirection.y);
            movementInput.Normalize();
            
            float speed = inputData.IsRunning ? Player.Stats.RunSpeed : Player.Stats.Speed;
            movementInput *= speed;
            
            Vector3 movement = (cameraRightVector * movementInput.x) + (cameraLookVector * movementInput.z);

            if (movement.sqrMagnitude > 0)
            {
                _footstepsAudio.Unpause();
                if (inputData.IsRunning && _lastPitch != RunPitch)
                {
                    _lastPitch = RunPitch;
                    _footstepsAudio.SetParameter("Pitch", RunPitch);
                }
                else if (inputData.IsRunning == false && _lastPitch != WalkPitch)
                {
                    _lastPitch = WalkPitch;
                    _footstepsAudio.SetParameter("Pitch", WalkPitch);
                }
            }
            else
                _footstepsAudio.Pause();
            
            
            #if UNITY_EDITOR
            Debug.DrawRay(Player.SceneObject.transform.position, cameraLookVector, Color.green);
            Debug.DrawRay(Player.SceneObject.transform.position, movement, Color.red);
            Debug.Log($"Input: {inputData.MovementDirection}");
            Debug.Log($"Player movement: {movement}");
            #endif
            _rigidbody.AddForce(movement, ForceMode.Impulse);
        }

        private void CheckInteractions(RawInput input)
        {
            Ray ray = Player.Camera.ScreenPointToRay(input.MousePos);
            if (Physics.Raycast(ray, out RaycastHit hit, 3f, _interactableLayerMask))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    _gameUIActions.CallChangeHint(interactable.HintText);
                    if (input.IsActionClicked)
                        interactable.Interact();
                }
            }
            else
                _gameUIActions.CallChangeHint(string.Empty);
        }
    }
}