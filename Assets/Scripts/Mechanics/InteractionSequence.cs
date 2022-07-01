using System.Collections;
using UnityEngine;

namespace Game
{
    public class InteractionSequence : MonoBehaviour
    {
        private Coroutine coroutine;

        public SequenceAction[] sequence;

        public void Execute()
        {
            Debug.Log("Playing: " + name, this);
            coroutine = StartCoroutine(ExecuteSequence());
        }

        private IEnumerator ExecuteSequence()
        {
            foreach (var action in sequence)
                //if (action.waitForCompletion)
                    yield return StartCoroutine(action.Execute());
                //else
                    //StartCoroutine(action.Execute());
        }
    }
}
