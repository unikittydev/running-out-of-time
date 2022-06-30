using UnityEngine;

namespace Game
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        public float h { get; private set; }

        [SerializeField]
        private ExecutableTimeBubble timeBubble;

        private void Update()
        {
            h = Input.GetAxis("Horizontal");
            if (Input.GetKey(Hotkeys.BUBBLE_ACTIVATE))
                timeBubble.Execute();
        }
    }
}
