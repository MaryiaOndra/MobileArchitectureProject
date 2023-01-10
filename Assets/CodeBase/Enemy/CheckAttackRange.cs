using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private TriggerObserver triggerObserver;

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;
            enemyAttack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            enemyAttack.EnableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            enemyAttack.DisableAttack();
        }
    }
}