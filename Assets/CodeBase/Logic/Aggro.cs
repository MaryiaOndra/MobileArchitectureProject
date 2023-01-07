using System;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private AgentMoveToPlayer follow;

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;
            
            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj) => SwitchFollowOn();
        private void TriggerExit(Collider obj) => SwitchFollowOff();
        private void SwitchFollowOn() => follow.enabled = true;
        private void SwitchFollowOff() => follow.enabled = false;
    }
}