using System.Collections;
using UnityEngine;

namespace Game
{
    public class PlayAnimationAction : SequenceAction
    {
        [SerializeField]
        private string flagName;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private bool applyRootMotion = true;

        public override IEnumerator Execute()
        {
            animator.applyRootMotion = applyRootMotion;
            animator.SetBool(flagName, true);

            while (animator.GetBool(flagName))
                yield return null;

            animator.applyRootMotion = true;
        }
    }
}
