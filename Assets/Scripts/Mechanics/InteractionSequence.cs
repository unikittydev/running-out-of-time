using System.Collections;
using UnityEngine;

namespace Game
{
    public class InteractionSequence : MonoBehaviour
    {
        private Coroutine coroutine;

        public SequenceAction[] sequence;

        [SerializeField]
        private PlayerController control;
        [SerializeField]
        private bool disableOnSequence;

        public void Execute()
        {
            coroutine = StartCoroutine(ExecuteSequence());
        }

        private IEnumerator ExecuteSequence()
        {
            control.enabled = !disableOnSequence;
            foreach (var action in sequence)
                if (action.waitForCompletion)
                    yield return StartCoroutine(action.Execute());
                else
                    StartCoroutine(action.Execute());
            control.enabled = true;
        }
    }
}
