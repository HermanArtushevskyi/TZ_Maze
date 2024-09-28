using FMODUnity;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.AudioService.Common
{
    [CreateAssetMenu(fileName = "AudioName", menuName = "ScriptableObjects/AudioName")]
    public class AudioName : ScriptableObject
    {
        [Header("UI")] [SerializeField] private EventReference UIClick;

        [Header("Gameplay")] [SerializeField] private EventReference PlayerFootsteps;
        [SerializeField] private EventReference ScrollUnroll;
        [SerializeField] private EventReference SpikeTrap;
        [SerializeField] private EventReference OnDeath;

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
    }
}