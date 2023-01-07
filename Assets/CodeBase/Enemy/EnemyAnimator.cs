using System;
using CodeBase.Logic.Animations;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Attack = Animator.StringToHash("Attack_1");
        private static readonly int Win = Animator.StringToHash("Win");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        
        private  readonly int _idleStateHash = Animator.StringToHash("idle");
        private  readonly int _attackStateHash = Animator.StringToHash("attack01");
        private  readonly int _walkingStateHash = Animator.StringToHash("Move");
        private  readonly int _deathStateHash = Animator.StringToHash("die");

        private Animator animator;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        private void Awake() => animator = GetComponent<Animator>();

        public void PlayHit() => animator.SetTrigger(Hit);
        public void PlayDeath() => animator.SetTrigger(Die);
        public void PlayWin() => animator.SetTrigger(Win);
        public void PlayAttack() => animator.SetTrigger(Attack);
        public void StopMoving() => animator.SetBool(IsMoving, false);


        public void Move(float speed)
        {
            animator.SetBool(IsMoving, true);
            animator.SetFloat(Speed, speed);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (_idleStateHash == stateHash)
                state = AnimatorState.Idle;
            else if (_attackStateHash == stateHash)
                state = AnimatorState.Attack;
            else if (_walkingStateHash == stateHash)
                state = AnimatorState.Walking;
            else if (_deathStateHash == stateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
            return state;
        }
    }
}
