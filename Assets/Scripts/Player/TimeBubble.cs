using System.Collections;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Девайс для путешествия во времени.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class TimeBubble : MonoBehaviour
    {
        [SerializeField]
        private float duration;
        [SerializeField]
        private float radius;

        private float _counter;

        [SerializeField]
        private PlayerController player;

        private SpriteMask mask;
        private CircleCollider2D trigger;

        [SerializeField]
        private LayerMask present;
        [SerializeField]
        private LayerMask past;

        private void Awake()
        {
            transform.localScale = Vector3.one * radius;
            mask = GetComponent<SpriteMask>();
            trigger = GetComponent<CircleCollider2D>();
        }

        private IEnumerator CreateBubble()
        {
            mask.enabled = trigger.enabled = true;
            player.gameObject.layer = Utils.GetLayerId(past);
            player.playerEpoch = TimeEpoch.Past;

            while (_counter < duration)
            {
                _counter += Time.deltaTime;
                yield return null;
            }

            _counter = 0f;

            player.gameObject.layer = Utils.GetLayerId(present);
            player.playerEpoch = TimeEpoch.Present;
            mask.enabled = trigger.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKey(Hotkeys.timeTravel) && _counter == 0f)
                StartCoroutine(CreateBubble());
        }
    }
}
