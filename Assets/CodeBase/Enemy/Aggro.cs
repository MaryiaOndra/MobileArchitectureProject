using System.Collections;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : Follow
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private AgentMoveToPlayer follow;
        [SerializeField] private float cooldown;
        private Coroutine aggroCoroutine;
        private bool hasAggroTarget;

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;
            
            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (!hasAggroTarget)
            {
                hasAggroTarget = true;
                StopAggroCoroutine();
                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (hasAggroTarget)
            {
                aggroCoroutine = StartCoroutine(SwitchFollowOffAfretCooldown());
                hasAggroTarget = false;
            }
        }

        private void StopAggroCoroutine()
        {
            if (aggroCoroutine != null)
            {
                StopCoroutine(aggroCoroutine);
                aggroCoroutine = null;
            }
        }

        private IEnumerator SwitchFollowOffAfretCooldown()
        {
            yield return new WaitForSeconds(cooldown);
            SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            if (!follow) return;
            follow.enabled = true;
        }
        
        private void SwitchFollowOff()
        {
            if (!follow) return;
            follow.enabled = false;
        }
    }
}