using UnityEngine;

namespace Game
{
    public class PlayerCharacter2D : MonoBehaviour
    {

        private readonly Quaternion forwardDirection = Quaternion.identity;
        private readonly Quaternion backwardDirection = Quaternion.Euler(0f, 180f, 0f);

        [SerializeField]
        private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField]
        private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField]
        private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.          // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        public bool facingRight => m_FacingRight;

        private PlayerIKWalking ikWalker;

        private PlayerController control;

        private Animator animator;

        public bool isGrounded => m_Grounded;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            ikWalker = GetComponent<PlayerIKWalking>();
            control = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                }
            }

            Move(control.h);
        }


        private void Move(float move)
        {
            animator.SetBool("Ground", m_Grounded);
            if (m_Grounded)
                animator.SetFloat("Walk", Mathf.Abs(move));
            else
                animator.SetFloat("Walk", 0f);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Move the character

                float target = move * m_MaxSpeed;
                float velocity = target;
                //float velocity = Mathf.SmoothDamp(m_Rigidbody2D.velocity.x, target, ref currVelocity, Mathf.Abs(move) * acceleration + (1f - Mathf.Abs(move)) * deceleration, m_MaxSpeed, Time.fixedDeltaTime);
                m_Rigidbody2D.velocity = new Vector2(velocity, m_Rigidbody2D.velocity.y);

                TryFlip(move);
            }
        }

        private void TryFlip(float move)
        {
            if (move > 0 && !m_FacingRight)
                Flip();
            else if (move < 0 && m_FacingRight)
                Flip();
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            transform.rotation = m_FacingRight ? forwardDirection : backwardDirection;
            ikWalker?.Flip();
        }
    }
}
