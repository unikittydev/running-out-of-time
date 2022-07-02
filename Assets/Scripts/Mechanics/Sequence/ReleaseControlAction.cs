using System.Collections;
using UnityEngine;

namespace Game
{
    public class ReleaseControlAction : SequenceAction
    {
        [SerializeField]
        private Platformer2DUserControl control;

        public override IEnumerator Execute()
        {
            control.enabled = true;
            yield break;
        }
    }
}
