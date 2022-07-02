using System.Collections;
using UnityEngine;

namespace Game
{
    public class PlayAnimationAction : SequenceAction
    {
        [SerializeField]
        private AnimationClip clip;

        [SerializeField]
        private Animator animator;

        public override IEnumerator Execute()
        {
            print("Anim started");
            bool animEnabled = animator.enabled;
            animator.enabled = true;
            animator.Play(clip.name);
            yield return new WaitForSeconds(clip.length);
            animator.enabled = animEnabled;

            print("Anim ended");
        }
    }
}
