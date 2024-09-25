namespace _Project.CodeBase.Runtime.StateMachine.Common
{
    public struct TransitionResult
    {
        public bool IsTransitionValid { get; }
        
        public TransitionResult(bool isTransitionValid)
        {
            IsTransitionValid = isTransitionValid;
        }
        
        public static TransitionResult Valid()
        {
            return new TransitionResult(true);
        }
        
        public static TransitionResult Invalid()
        {
            return new TransitionResult(false);
        }
    }
}