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
        [SerializeField]
        private bool releaseControlOnEnd = true;

        public override IEnumerator Execute()
        {
            print("Walk started");
            Transform playerTr = control.transform;

            control.enabled = false;
            float eps = .1f;
            float from = playerTr.position.x;
            float to = target.position.x;

            while (Mathf.Abs(playerTr.position.x - to) > eps * eps)
            {
                control.h = playerTr.position.x < to ? 1f : -1f;
                print(Mathf.Abs(playerTr.position.x - to));
                yield return new WaitForFixedUpdate();
            }
            control.enabled = releaseControlOnEnd;
            print("Walk ended");
        }
    }
}
