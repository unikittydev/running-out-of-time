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

        [Header("Legs")]
        [SerializeField]
        private float stepDistance = 1f;
        [SerializeField]
        private float stepHeight;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float stepOffset = 0f;
        [SerializeField]
        private Transform leftFoot;
        [SerializeField]
        private Transform rightFoot;
        [SerializeField]
        private AnimationCurve walkCurve;
        [SerializeField]
        private Transform rayOrigin;

        private Vector3 leftStep;
        private Vector3 rightStep;

        [Header("Body")]
        [SerializeField]
        private float wobbleAmplitude;
        [SerializeField]
        private Transform bodyBone;

        [Header("Hands")]
        [SerializeField]
        private float wavingAmplitude;
        [SerializeField]
        private Transform leftHand;
        [SerializeField]
        private Transform rightHand;
        [SerializeField]
        private AnimationCurve waveCurve;

        [Header("Raycasting")]
        [SerializeField]
        private LayerMask ground;
        [SerializeField]
        private float maxRayDistance = 4f;

        private bool leftStepIsLast;

        private float dotLeft, dotRight;

        private RaycastHit2D[] hits = new RaycastHit2D[2];

        private PlatformerCharacter2D character;

        private void Awake()
        {
            tr = transform;
            character = GetComponent<PlatformerCharacter2D>();
        }

        private void Start()
        {
            leftStep = rightStep = tr.position;
        }

        private void Update()
        {
            dotRight = Vector3.Dot(tr.right, (leftStep - tr.position).normalized);
            dotLeft = Vector3.Dot(tr.right, (rightStep - tr.position).normalized);

            if (character.isGrounded)
            {
                rightFoot.position = rightStep;
                leftFoot.position = leftStep;

                Vector3 leftHandPos = leftHand.position;
                leftHandPos.x = rightFoot.position.x;
                leftHand.position = leftHandPos;

                Vector3 rightHandPos = rightHand.position;
                rightHandPos.x = leftFoot.position.x;
                rightHand.position = rightHandPos;
            }
            else
            {
                rightStep = TryFindFootPosition(rightFoot.position);
                leftStep = TryFindFootPosition(leftFoot.position);
            }

            if (leftStepIsLast)
            {
                if (dotRight <= 0f && (tr.position - rightStep).sqrMagnitude > 2 * stepDistance * stepDistance)
                {
                    Vector3 from = rightStep;
                    rightStep = TryFindFootPosition(rayOrigin.position + tr.right * (stepDistance + stepOffset));
                    leftStepIsLast = false;
                    StartCoroutine(ChangeStep(from, rightStep, rightFoot, leftHand));
                }
            }
            else
            {
                if (dotLeft <= 0f && (tr.position - leftStep).sqrMagnitude > 2 * stepDistance * stepDistance)
                {
                    Vector3 from = leftStep;
                    leftStep = TryFindFootPosition(rayOrigin.position + tr.right * (stepDistance + stepOffset));
                    leftFoot.position = leftStep;
                    leftStepIsLast = true;
                    StartCoroutine(ChangeStep(from, leftStep, leftFoot, rightHand));
                }
            }
        }

        private IEnumerator ChangeStep(Vector3 from, Vector3 to, Transform targetFoot, Transform targetHand)
        {
            float counter = 0f;

            Vector3 bodyStartPos = bodyBone.localPosition;
            float handHeight = targetHand.position.y;

            while (counter < speed)
            {
                float time = counter / speed;
                float curve = walkCurve.Evaluate(time);
                // Изменяем позицию ног.
                targetFoot.position = Vector3.Lerp(from, to, time) + new Vector3(0f, curve * stepHeight, 0f);
                // Изменяем позицию тела
                bodyBone.localPosition = bodyStartPos + new Vector3(0f, curve * wobbleAmplitude, 0f);
                // Изменяем позицию рук
                targetHand.position = new Vector3(targetFoot.position.x, handHeight + waveCurve.Evaluate(time) * wavingAmplitude, 0f);

                counter += Time.deltaTime;
                yield return null;
            }
        }

        private Vector2 TryFindFootPosition(Vector2 position)
        {
            int hitCount = Physics2D.RaycastNonAlloc(position, Vector2.down, hits, maxRayDistance, ground);
            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i].collider.CompareTag(Tags.player))
                    continue;

                return hits[i].point;
            }
            return position;
        }

        public void Flip()
        {
            leftStepIsLast = !leftStepIsLast;
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
