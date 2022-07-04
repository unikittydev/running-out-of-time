using System.Collections;
using UnityEngine;

namespace Game
{
    public class WalkToAction : SequenceAction
    {
        [SerializeField]
        private PlayerController control;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private bool faceRightOnEnd;
        [SerializeField]
        private bool releaseControlOnEnd = true;

        private const float decelerationFactor = 4f;

        public override IEnumerator Execute()
        {
            Transform playerTr = control.transform;

            control.enabled = false;
            float eps = .2f;
            float from = playerTr.position.x;
            float to = target.position.x;
            float smoothTime = Mathf.Abs(from - to) / decelerationFactor;

            float vel = 0f;
            while (Mathf.Abs(playerTr.position.x - to) > eps * eps)
            {
                control.h = (playerTr.position.x < to ? 1f : -1f) * Mathf.SmoothDamp(1f, 0f, ref vel, smoothTime, 1f, Time.deltaTime);
                print(control.h);
                yield return new WaitForFixedUpdate();
            }
            control.h = faceRightOnEnd ? 0.01f : -0.01f;
            yield return new WaitForFixedUpdate();
            control.h = 0f;
            control.enabled = releaseControlOnEnd;
        }
    }
}
