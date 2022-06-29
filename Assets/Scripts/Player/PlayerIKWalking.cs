using System.Collections;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Скрипт для процедурной ходьбы игрока.
    /// </summary>
    public class PlayerIKWalking : MonoBehaviour
    {
        private Transform tr;

        [SerializeField]
        private bool allowRunning;

        [SerializeField]
        private float stepDistance = 1f;
        [SerializeField]
        private float stepHeight;
        [SerializeField]
        private float speed;

        [SerializeField]
        private LayerMask layers;
        [SerializeField]
        private float maxRayDistance = 4f;

        private Vector3 leftStep;
        private Vector3 rightStep;

        private bool leftStepIsLast;

        private float dotLeft, dotRight;

        private RaycastHit2D[] hits = new RaycastHit2D[2];

        [SerializeField]
        private Transform leftFoot;
        [SerializeField]
        private Transform rightFoot;

        [SerializeField]
        private AnimationCurve walkCurve;

        private void Awake()
        {
            tr = transform;
        }

        private void Start()
        {
            leftStep = rightStep = tr.position;
        }

        private void FixedUpdate()
        {
            dotRight = Vector3.Dot(tr.right, (leftStep - tr.position).normalized);
            dotLeft = Vector3.Dot(tr.right, (rightStep - tr.position).normalized);
            if (leftStepIsLast)
            {
                if (dotRight <= 0f && (tr.position - rightStep).sqrMagnitude > 2 * stepDistance * stepDistance)
                {
                    Vector3 from = rightStep;
                    rightStep = TryFindFootPosition(tr.position + tr.right * stepDistance);
                    rightFoot.position = rightStep;
                    leftStepIsLast = false;
                    StartCoroutine(ChangeStep(from, rightStep, rightFoot));
                }
            }
            else
            {
                if (dotLeft <= 0f && (tr.position - leftStep).sqrMagnitude > 2 * stepDistance * stepDistance)
                {
                    Vector3 from = leftStep;
                    leftStep = TryFindFootPosition(tr.position + tr.right * stepDistance);
                    leftFoot.position = leftStep;
                    leftStepIsLast = true;
                    StartCoroutine(ChangeStep(from, leftStep, leftFoot));
                }
            }
        }

        private IEnumerator ChangeStep(Vector3 from, Vector3 to, Transform target)
        {
            float counter = 0f;

            while (counter < speed)
            {
                target.position = Vector3.Lerp(from, to, counter / speed) + walkCurve.Evaluate(counter / speed) * stepHeight * Vector3.up;

                counter += Time.deltaTime;
                yield return null;
            }
        }

        private Vector2 TryFindFootPosition(Vector2 position)
        {
            int hitCount = Physics2D.RaycastNonAlloc(position, Vector2.down, hits, maxRayDistance, layers);
            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i].collider.CompareTag(Tags.player))
                    continue;

                return hits[i].point;
            }
            return position;
        }

        private void OnDrawGizmos()
        {
            if (tr == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(tr.position, leftStep);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(tr.position, rightStep);
        }
    }
}
