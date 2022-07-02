using System.Collections;
using UnityEngine;

namespace Game
{
    public class ReleaseControlAction : SequenceAction
    {
        [SerializeField]
        private PlayerController control;

        public override IEnumerator Execute()
        {
            control.enabled = true;
            yield break;
        }
    }
}
