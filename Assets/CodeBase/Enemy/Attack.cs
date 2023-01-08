using System;
using System.Linq;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private float attackCooldown = 3f;
        [SerializeField] private float cleavage = 0.5f;
        [SerializeField] private float effectiveDistance = 0.5f;

        private IGameFactory factory;
        private Transform heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMAsk;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            factory = AllServices.Container.Single<IGameFactory>();
            _layerMAsk = 1 << LayerMask.NameToLayer("Player");
            factory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();
            if(CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
                PhysicsDebug.DrawDebug(StartPoint(), cleavage, 1f);
        }

        private bool Hit(out Collider hit)
        {
           int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), cleavage, _hits, _layerMAsk);
           hit = _hits.FirstOrDefault();
           return hitCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z)
            + transform.forward * effectiveDistance;

        private void OnAttackEnded()
        {
            _attackCooldown = attackCooldown;
            _isAttacking = false;
        }

        public void DisableAttack() =>
            _attackIsActive = false;
        public void EnableAttack() =>
            _attackIsActive = true;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private void StartAttack()
        {
            transform.LookAt(heroTransform);
            animator.PlayAttack();
            _isAttacking = true;
        }

        private bool CooldownIsUp()
            => _attackCooldown <= 0;

        private void OnHeroCreated() =>
            heroTransform = factory.HeroGameObject.transform;

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownIsUp();
    }
}
