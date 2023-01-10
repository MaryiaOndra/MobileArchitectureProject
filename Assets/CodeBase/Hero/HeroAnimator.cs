using System;
using CodeBase.Enemy;
using CodeBase.Logic.Animations;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Die = Animator.StringToHash("Die");
        
        private  readonly int _idleStateHash = Animator.StringToHash("idle");
        private  readonly int _attackStateHash = Animator.StringToHash("attack");
        private  readonly int _walkingStateHash = Animator.StringToHash("walking");
        private  readonly int _deathStateHash = Animator.StringToHash("die");
        
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;

        public AnimatorState State { get; private set; }
        public static bool IsAttacking { get; private set; }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        public void PlayHit() => animator.SetTrigger(Hit);
        public void PlayDeath() => animator.SetTrigger(Die);
        public void PlayAttack()
        {
            IsAttacking = true;
            animator.SetTrigger(Attack);
        }

        private void Update()
        {
            animator.SetFloat(MoveHash, characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }
        
        public void ExitedState(int stateHash)
        {
            CheckIfAttackFinished(stateHash);
            StateExited?.Invoke(StateFor(stateHash));
        }

        private void CheckIfAttackFinished(int stateHash)
        {
            if (stateHash == _attackStateHash)
                IsAttacking = false;
        }

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
