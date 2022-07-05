using System.Collections;
using UnityEngine;

namespace Game
{
    public class WaitForSecondsAction : SequenceAction
    {
        [SerializeField]
        private float seconds;

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(seconds);
        }
    }
}
