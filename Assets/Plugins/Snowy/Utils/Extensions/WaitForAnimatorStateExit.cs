using UnityEngine;

namespace Core
{
    public class WaitForAnimatorStateExit : CustomYieldInstruction
    {
        private bool isStateEntered = false;
        private readonly Animator animator;
        private readonly string state;

        public WaitForAnimatorStateExit(Animator animator, string state)
        {
            this.animator = animator;
            this.state = state;
        }

        public override bool keepWaiting
        {
            get
            {
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
                bool isState = info.IsName(state);
                
                if (!isState && isStateEntered)
                    return false;
                
                if (isState)
                    isStateEntered = true;

                return true;
            }
        }
    }
}