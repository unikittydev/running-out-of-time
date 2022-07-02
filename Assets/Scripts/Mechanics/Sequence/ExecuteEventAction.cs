using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ExecuteEventAction : SequenceAction
    {
        [SerializeField]
        private UnityEvent onExecute;

        public override IEnumerator Execute()
        {
            onExecute?.Invoke();
            yield break;
        }
    }
}
