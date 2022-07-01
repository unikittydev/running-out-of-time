using System.Collections;
using UnityEngine;

namespace Game
{
    public abstract class SequenceAction : MonoBehaviour
    {
        public abstract IEnumerator Execute();
    }
}
