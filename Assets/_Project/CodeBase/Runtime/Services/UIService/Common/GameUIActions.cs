using System;

namespace _Project.CodeBase.Runtime.Services.UIService.Common
{
    public sealed class GameUIActions
    {
        public event Action OnPause;
        public event Action<string> OnDead;
        public event Action OnWin;
        public event Action<string> OnOpenScroll;
        public event Action<string> OnChangeHint;
        public Func<TimeSpan> GetTime;
        
        public void CallPause()
        {
            OnPause?.Invoke();
        }
        
        public void CallOpenScroll(string text)
        {
            OnOpenScroll?.Invoke(text);
        }
        
        public void CallChangeHint(string hint)
        {
            OnChangeHint?.Invoke(hint);
        }
        
        public void CallDead(string reason)
        {
            OnDead?.Invoke(reason);
        }

        public void CallWin()
        {
            OnWin?.Invoke();
        }
    }
}