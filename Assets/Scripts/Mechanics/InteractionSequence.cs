using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class InteractionSequence : MonoBehaviour
    {
        private Coroutine coroutine;

        public SequenceAction[] sequence;

        public void Execute()
        {
            coroutine = StartCoroutine(ExecuteSequence());
        }

        private IEnumerator ExecuteSequence()
        {
            foreach (var action in sequence)
                yield return action.Execute();
        }
    }
}
