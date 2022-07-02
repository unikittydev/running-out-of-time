using UnityEngine;

namespace Game
{
    [RequireComponent(typeof (PlayerCharacter2D))]
    public class PlayerController : MonoBehaviour
    {
        private float _h;
        public float h
        {
            get => _h;
            set => _h = Mathf.Clamp(value, -1f, 1f);
        }

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