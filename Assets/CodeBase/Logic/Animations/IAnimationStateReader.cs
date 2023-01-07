using System;

namespace CodeBase.Logic.Animations
{
    public interface IAnimationStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        AnimatorState State { get; }
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
    }
}