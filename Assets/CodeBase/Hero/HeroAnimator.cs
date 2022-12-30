using System;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;

        private void Update()
        {
            animator.SetFloat(MoveHash, characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }
    }
}
