using UnityEngine;

namespace Game
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        public float h { get; private set; }

        private void Update()
        {
            h = Input.GetAxis("Horizontal");
        }
    }
}
