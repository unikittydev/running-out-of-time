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

        public bool interact { get; set; }

        [SerializeField]
        private SwitchableTimeBubble timeBubble;

        private bool _locked = false;

        [SerializeField]
        private bool _canUseWatch;
        public bool canUseWatch
        {
            get => _canUseWatch;
            set => _canUseWatch = value;
        }

        public void Lock() => _locked = true;
        public void Unlock() => _locked = false;

        public void TeleportTo(Transform target)
        {
            transform.position = target.position;
        }

        private void Update()
        {
            interact = false;
            h = 0;

            if (_locked) return;

            h = Input.GetAxis("Horizontal");
            if (canUseWatch && Input.GetKey(Hotkeys.BUBBLE_ACTIVATE))
                timeBubble.Execute();
            interact = Input.GetKeyDown(Hotkeys.INTERACT);
        }

        private void OnDisable()
        {
            interact = false;
        }
    }
}
