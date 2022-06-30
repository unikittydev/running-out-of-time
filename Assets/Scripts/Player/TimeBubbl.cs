using System.Collections;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Девайс для путешествия во времени.
    /// </summary>
    public class TimeBubbl : MonoBehaviour
    {
        [SerializeField]
        private float duration;
        [SerializeField]
        private float radius;

        private float _counter;

        [SerializeField]
        private Player player;

        private SpriteMask mask;

        [SerializeField]
        private LayerMask present;
        [SerializeField]
        private LayerMask past;
            
        private void Awake()
        {
            transform.localScale = Vector3.one * 2f * radius;
            mask = GetComponent<SpriteMask>();
        }

        private IEnumerator CreateBubble()
        {
            mask.enabled = true;
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
            mask.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKey(Hotkeys.timeTravel) && _counter == 0f)
                StartCoroutine(CreateBubble());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
