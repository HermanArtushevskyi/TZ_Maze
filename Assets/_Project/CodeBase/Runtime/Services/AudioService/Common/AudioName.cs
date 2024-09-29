using FMODUnity;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.AudioService.Common
{
    [CreateAssetMenu(fileName = "AudioName", menuName = "ScriptableObjects/AudioName")]
    public class AudioName : ScriptableObject
    {
        [SerializeField] private EventReference Ambient;
        
        [Header("UI")]
        [SerializeField] private EventReference UIClick;

        [Header("Gameplay")]
        [SerializeField] private EventReference PlayerFootsteps;
        [SerializeField] private EventReference ScrollUnroll;
        [SerializeField] private EventReference SpikeTrap;
        [SerializeField] private EventReference OnDeath;
        [SerializeField] private EventReference EnemyFootsteps;
        [SerializeField] private EventReference Breath;
        [SerializeField] private EventReference KeyFound;
        [SerializeField] private EventReference LockedDoor;
        [SerializeField] private EventReference UnlockedDoor;

        public string AmbientSound
        {
            get
            {
#if UNITY_EDITOR
                return Ambient.Path;
#else
                return Ambient.ToString();
#endif
            }
        }
        
        public string UIClickSound
        {
            get
            {
                // FMOD works awkward here, we need to use .ToString() in builds and .Path in editor
#if UNITY_EDITOR
                return UIClick.Path;
#else
                return UIClick.ToString();
#endif
            }
        }

        public string PlayerFootstepsSound
        {
            get
            {
#if UNITY_EDITOR
                return PlayerFootsteps.Path;
#else
                return PlayerFootsteps.ToString();
#endif
            }
        }

        public string ScrollUnrollSound
        {
            get
            {
#if UNITY_EDITOR
                return ScrollUnroll.Path;
#else
                return ScrollUnroll.ToString();
#endif
            }
        }

        public string SpikeTrapSound
        {
            get
            {
#if UNITY_EDITOR
                return SpikeTrap.Path;
#else
                return SpikeTrap.ToString();
#endif
            }
        }

        public string OnDeathSound
        {
            get
            {
#if UNITY_EDITOR
                return OnDeath.Path;
#else
                return OnDeath.ToString();
#endif
            }
        }

        public string EnemyFootstepsSound
        {
            get
            {
#if UNITY_EDITOR
                return EnemyFootsteps.Path;
#else
                return EnemyFootsteps.ToString();
#endif
            }
        }

        public string BreathSound
        {
            get
            {
#if UNITY_EDITOR
                return Breath.Path;
#else
                return Breath.ToString();
#endif
            }
        }

        public string KeyFoundSound
        {
            get
            {
#if UNITY_EDITOR
                return KeyFound.Path;
#else
                return KeyFound.ToString();
#endif
            }
        }

        public string LockedDoorSound
        {
            get
            {
#if UNITY_EDITOR
                return LockedDoor.Path;
#else
                return LockedDoor.ToString();
#endif
            }
        }
        
        public string UnlockedDoorSound
        {
            get
            {
#if UNITY_EDITOR
                return UnlockedDoor.Path;
#else
                return UnlockedDoor.ToString();
#endif
            }
        }
    }
}