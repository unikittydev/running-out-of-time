using UnityEngine;

namespace Game
{
    /// <summary>
    /// Контроллер игрока.
    /// </summary>
	[RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour
    {
		private readonly Quaternion forwardDirection = Quaternion.identity;
		private readonly Quaternion backwardDirection = Quaternion.Euler(0f, 180f, 0f);

		[SerializeField] private float speed = 1f;
		[SerializeField] private float stepSize = 0.7f;

		[SerializeField]
		private Transform model;

		private Rigidbody2D rb;

		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			// TO DO:
			// Dynamic steps
			// Delete RigidBody ?

			float velocity = Input.GetAxis("Horizontal") * speed;
			if (velocity > 0f)
				model.rotation = forwardDirection;
			else
				model.rotation = backwardDirection;

			rb.MovePosition(rb.position + Vector2.right * velocity * Time.fixedDeltaTime);
		}
	}
}
