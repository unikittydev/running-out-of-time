using System.Collections;
using UnityEngine;

namespace Game
{
    public abstract class SequenceAction : MonoBehaviour
    {
        [field: SerializeField]
        public bool waitForCompletion { get; set; } = true;

        public abstract IEnumerator Execute();
    }
}
