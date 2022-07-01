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

        private void Awake()
        {

        }

        public override IEnumerator Execute()
        {
            animator.enabled = true;
            animator.Play(clip.name);
            print(clip.name);
            yield return new WaitForSeconds(clip.length);
            animator.enabled = false;
        }
    }
}
