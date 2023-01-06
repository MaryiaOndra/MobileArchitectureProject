using System;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        public bool IsAttacking { get; private set; }

        public void Attack() => animator.SetTrigger(AttackHash);
        
        private void Update()
        {
            animator.SetFloat(MoveHash, characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }
    }
}
