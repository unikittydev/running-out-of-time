using System.Collections;
using UnityEngine;

namespace Game
{
    public class WalkToAction : SequenceAction
    {
        [SerializeField]
        private Platformer2DUserControl control;
        [SerializeField]
        private Transform target;

        public override IEnumerator Execute()
        {
            float eps = .1f;
            Vector3 to = target.position;

            while (Mathf.Abs(transform.position.x - to.x) > eps * eps)
            {
                control.h = (transform.position.x < to.x ? 1f : -1f) * Mathf.Min(1f, Mathf.Abs(transform.position.x - to.x));
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
