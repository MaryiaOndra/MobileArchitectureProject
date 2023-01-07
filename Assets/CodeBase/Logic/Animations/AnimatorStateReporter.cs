using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Logic.Animations
{
    public class AnimatorStateReporter : StateMachineBehaviour
    {
        private IAnimationStateReader stateReader;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            FindReader(animator);
            stateReader.EnteredState(stateInfo.shortNameHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            FindReader(animator);
            stateReader.ExitedState(stateInfo.shortNameHash);
        }

        private void FindReader(Animator animator)
        {
            if(stateReader != null)
                return;
            stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
        }
    }
}