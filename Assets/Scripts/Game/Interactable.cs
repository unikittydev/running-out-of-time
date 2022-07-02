using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    /// <summary>
    /// ��������� ��� ������������� ��������.
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        private Collider2D trigger;

        [SerializeField]
        private TimeEpoch interactWhen;

        [SerializeField]
        private bool startAutomatically;

        [SerializeField]
        private bool destroyOnInteract;

        [SerializeField]
        private UnityEvent onInteract;

        private Player player;

        private void Start()
        {
            trigger.isTrigger = true;
        }

        private void Update()
        {
            if (player != null && interactWhen.HasFlag(player.playerEpoch) && (startAutomatically || Input.GetKeyDown(Hotkeys.INTERACT)))
            {
                onInteract.Invoke();
                if (destroyOnInteract)
                    Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.PLAYER))
                player = collision.GetComponent<Player>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.PLAYER))
                player = null;
        }

        public void TestInteraction(string msg)
        {
            Debug.Log(msg);
        }
    }
}
