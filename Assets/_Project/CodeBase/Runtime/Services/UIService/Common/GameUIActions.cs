using System;

namespace _Project.CodeBase.Runtime.Services.UIService.Common
{
    public class GameUIActions
    {
        public event Action OnPause;
        public event Action<string> OnDead;
        public event Action<string> OnOpenScroll;
        public event Action<string> OnChangeHint;
        
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
    }
}