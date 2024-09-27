using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Runtime.Services.SceneService
{
    [RequireComponent(typeof(Image))]
    public class Curtain : MonoBehaviour, ICurtain
    {
        private Image _image;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            _image = GetComponent<Image>();
            _image.color = new Color(0f, 0f, 0f, 0f);
            _image.raycastTarget = false;
        }

        public async UniTask Open()
        {
            if (_image == null)
                return;
            if (_image.color.a == 0f)
                _image.color = new Color(0f, 0f, 0f, 1f);
            await _image.DOFade(0f, 0.5f).ToUniTask();
        }

        public async UniTask Close()
        {
            if (_image == null)
                return;
            if (_image.color.a == 1f)
                _image.color = new Color(0f, 0f, 0f, 0f);
            await _image.DOFade(1f, 0.5f).ToUniTask();
        }
    }
}