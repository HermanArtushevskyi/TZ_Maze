using FMODUnity;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.AudioService.Common
{
    [CreateAssetMenu(fileName = "AudioName", menuName = "ScriptableObjects/AudioName")]
    public class AudioName : ScriptableObject
    {
        [Header("FMOD")] [SerializeField] private EventReference UIClick;

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
    }
}