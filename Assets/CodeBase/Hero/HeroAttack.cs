using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator animator;
        [SerializeField] private CharacterController controller;

        private IInputService input;
        private static int layerMask;
        private float radius;
        private Collider[] hits = new Collider[3];
        private Stats stats;

        private void Awake()
        {
            input = AllServices.Container.Single<IInputService>();
            layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (input.IsAttackButtonUp() && !HeroAnimator.IsAttacking)
                animator.PlayAttack();
        }

        public void OnAttack()
        {
            for (int i = 0; i < HIt(); i++)
            {
                hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(stats.damage);
            }
        }

        public void LoadProgress(PlayerProgress progress) => 
            stats = progress.heroStats;

        private int HIt() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, stats.damageRadius, hits, layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, controller.center.y / 2f, transform.position.z);
    }
}