using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float jumpForce = 365f;
    [SerializeField] private float mCrouchSpeed = 0.5f;
    [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] private bool grounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Collider2D crouchDisableCollider;

    private bool wasCrouching = false;
    private float groundedRadius = 0.2f;
    private float ceilingRadius = 0.2f;
    private bool facingRight = true;
    private Rigidbody2D rb2d;
    private Vector3 velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        if (!crouch && grounded)
        {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;
            }
        }

        if (crouch)
        {
            if (!wasCrouching)
            {
                wasCrouching = true;
                OnCrouchEvent.Invoke(true);
            }

            move *= mCrouchSpeed;

            if (crouchDisableCollider != null)
                crouchDisableCollider.enabled = false;
        }
        else
        {

            if (crouchDisableCollider != null)
                crouchDisableCollider.enabled = true;

            if (wasCrouching)
            {
                wasCrouching = false;
                OnCrouchEvent.Invoke(false);
            }
        }

        Vector3 targetVelocity = new Vector2(move * 10f, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        if (grounded && jump)
        {
            grounded = false;
            rb2d.AddForce(new Vector2(0f, jumpForce));
        }
    }


    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
