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
        private bool disableControlOnSequence;
        [SerializeField]
        private bool disableSequenceOnEnd;

        public void Execute()
        {
            coroutine = StartCoroutine(ExecuteSequence());
        }

        private IEnumerator ExecuteSequence()
        {
            control.enabled = !disableControlOnSequence;
            foreach (var action in sequence)
                if (action.waitForCompletion)
                    yield return StartCoroutine(action.Execute());
                else
                    StartCoroutine(action.Execute());
            control.enabled = true;

            if (disableSequenceOnEnd)
                gameObject.SetActive(false);
        }
    }
}
