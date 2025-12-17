using UnityEngine;
using UnityEngine.InputSystem;


public class CharacterAnimations : MonoBehaviour
{
    // Movement settings
    public float forwardSpeed = 5f;        // Auto-run forward speed
    public float strafeSpeed = 6f;         // Left/right speed
    public float jumpHeight = 2f;          // Jump height
    public float gravity = -9.81f;         // Gravity acceleration
    public float slideSpeed = 15f;         // Forward speed during slide
    public float slideDuration = 0.5f;     // Duration of slide

    // Components
    private CharacterController controller;
    private Animator characterAnimator;

    // Player input
    private float horizontalInput;

    // Vertical movement
    private Vector3 velocity;              // Vertical velocity
    private bool isGrounded;               // Ground check state

    // Ground checking
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    // Sliding state
    private bool isSliding = false;
    private float slideTimer = 0f;

    [Header("Fall Settings")]
    public string fallTriggerName = "FallBack"; // Animator trigger name
    private bool isFalling = false;            // Prevent multiple fall triggers

    private void Awake()
    {
        // Cache components
        controller = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused)
            return;

        // Check if player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset downward velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = Vector3.zero;

        // Only allow movement if not falling
        if (!isFalling)
        {
            if (isSliding)
            {
                // Slide forward + strafe
                move = transform.forward * slideSpeed + transform.right * horizontalInput * strafeSpeed;

                // Update slide timer
                slideTimer -= Time.deltaTime;
                if (slideTimer <= 0f)
                {
                    isSliding = false;
                    characterAnimator.SetBool("isSliding", false);
                    characterAnimator.SetBool("isRunning", true);
                }
            }
            else
            {
                // Normal running forward + strafe
                move = transform.forward * forwardSpeed + transform.right * horizontalInput * strafeSpeed;
                characterAnimator.SetBool("isRunning", true);
            }
        }

        // Move character controller
        controller.Move(move * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Clamp lateral movement within bounds
        Vector3 pos = transform.position;
        pos.z = Mathf.Clamp(pos.z, -4f, 4f);
        transform.position = pos;
    }

    // Called by input system for lateral movement and slide input
    public void OnMove(InputValue value)
    {
        if (GameManager.Instance.IsPaused || isFalling)
            return;

        Vector2 input = value.Get<Vector2>();
        horizontalInput = input.x;

        // Start slide if player presses down while grounded
        if (input.y < -0.1f && isGrounded && !isSliding)
        {
            StartSlide();
        }
    }

    // Called by input system for jump
    public void OnJump(InputValue value)
    {
        if (GameManager.Instance.IsPaused)
            return;

        if (!isFalling && value.isPressed && isGrounded && !isSliding)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Set vertical velocity
            characterAnimator.SetTrigger("Jump");                // Trigger jump animation
        }
    }

    // Start sliding
    private void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        characterAnimator.SetBool("isSliding", true);
        characterAnimator.SetBool("isRunning", false); // Stop running animation
    }

    // Detect collisions with obstacles or collectables
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Collision with obstacle
        if (!isFalling && hit.gameObject.CompareTag("Obstacle"))
        {
            StartFallBack();             // Trigger fall animation
            GameManager.Instance.EndGame(); // End game
        }
        // Collision with collectable
        else if (hit.gameObject.CompareTag("Collectable"))
        {
            Collider col = hit.gameObject.GetComponent<Collider>();
            if (col != null && col.enabled)
            {
                col.enabled = false;              // Prevent repeated collection
                hit.gameObject.SetActive(false);  // Hide collectable
                GameManager.Instance.HandlePickup(); // Update score
            }
        }
    }

    // Trigger fall animation and prevent further movement
    private void StartFallBack()
    {
        isFalling = true;
        characterAnimator.SetTrigger(fallTriggerName);
    }
}
