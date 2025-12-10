////old simple script 
//using UnityEngine;
//using UnityEngine.InputSystem;

//[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(CharacterController))]
//public class CharacterAnimations : MonoBehaviour
//{
//    [Header("Movement Settings")]
//    public float forwardSpeed = 10f;     // Auto-run forward speed
//    public float strafeSpeed = 6f;       // Left/right speed

//    private CharacterController controller;
//    private Animator characterAnimator;
//    private float horizontalInput;

//    private void Awake()
//    {
//        controller = GetComponent<CharacterController>();
//        characterAnimator = GetComponent<Animator>();
//    }

//    private void Update()
//    {
//        // Automatic forward running
//        Vector3 move = transform.forward * forwardSpeed;

//        // Left/right movement
//        move += transform.right * horizontalInput * strafeSpeed;

//        controller.Move(move * Time.deltaTime);

//        // Always running
//        characterAnimator.SetBool("isRunning", true);
//    }

//    // Left/right input only
//    public void OnMove(InputValue value)
//    {
//        Vector2 input = value.Get<Vector2>();
//        horizontalInput = input.x;
//    }


//    public void OnJump(InputValue value)
//    {
//        if (value.isPressed)
//        {
//            characterAnimator.SetTrigger("Jump");
//        }
//    }

//}

////new with jump and grounded but not smooth
//using UnityEngine;
//using UnityEngine.InputSystem;

//[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(CharacterController))]
//public class CharacterAnimations : MonoBehaviour
//{
//    [Header("Movement Settings")]
//    public float forwardSpeed = 10f;     // Auto-run forward speed
//    public float strafeSpeed = 6f;       // Left/right speed
//    public float jumpHeight = 2f;        // Jump height
//    public float gravity = -9.81f;       // Gravity

//    private CharacterController controller;
//    private Animator characterAnimator;
//    private float horizontalInput;

//    private Vector3 velocity;             // Vertical velocity
//    private bool isGrounded;

//    [Header("Ground Check")]
//    public Transform groundCheck;        // Empty GameObject at feet
//    public float groundDistance = 0.2f;  // Sphere radius for ground check
//    public LayerMask groundMask;         // Layer considered as ground

//    private void Awake()
//    {
//        controller = GetComponent<CharacterController>();
//        characterAnimator = GetComponent<Animator>();
//    }

//    private void Update()
//    {
//        // Check if character is on the ground
//        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);



//        if (isGrounded && velocity.y < 0)
//        {
//            velocity.y = -2f; // Small downward force to stick to ground
//        }

//        // Automatic forward running + left/right
//        Vector3 move = transform.forward * forwardSpeed + transform.right * horizontalInput * strafeSpeed;
//        controller.Move(move * Time.deltaTime);

//        // Apply gravity
//        velocity.y += gravity * Time.deltaTime;
//        controller.Move(velocity * Time.deltaTime);

//        // Always running animation
//        characterAnimator.SetBool("isRunning", true);
//    }

//    public void OnMove(InputValue value)
//    {
//        Vector2 input = value.Get<Vector2>();
//        horizontalInput = input.x; // Only left/right
//    }

//    public void OnJump(InputValue value)
//    {
//        if (value.isPressed && isGrounded)
//        {
//            // Jump velocity based on desired jump height
//            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
//            characterAnimator.SetTrigger("Jump");
//        }
//    }
//}

//slide added too not so smooth
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterAnimations : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;        // Auto-run forward speed
    public float strafeSpeed = 6f;          // Left/right speed
    public float jumpHeight = 2f;           // Jump height
    public float gravity = -9.81f;          // Gravity
    public float slideSpeed = 15f;          // Forward speed during slide
    public float slideDuration = 0.5f;      // Duration of slide

    private CharacterController controller;
    private Animator characterAnimator;
    private float horizontalInput;

    private Vector3 velocity;                // Vertical velocity
    private bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    // Slide state
    private bool isSliding = false;
    private float slideTimer = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move;

        if (isSliding)
        {
            // Running slide: automatically moves forward, ignores forward/backward input
            move = transform.forward * slideSpeed + transform.right * horizontalInput * strafeSpeed;

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
            // Normal running
            move = transform.forward * forwardSpeed + transform.right * horizontalInput * strafeSpeed;
            characterAnimator.SetBool("isRunning", true);
        }

        controller.Move(move * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        horizontalInput = input.x;

        // Start running slide if down pressed while grounded
        if (input.y < -0.1f && isGrounded && !isSliding)
        {
            StartSlide();
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded && !isSliding)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            characterAnimator.SetTrigger("Jump");
        }
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        characterAnimator.SetBool("isSliding", true);
        characterAnimator.SetBool("isRunning", false); // stop running animation
    }
}
