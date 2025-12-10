using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;
    public float strafeSpeed = 5f;
    public float jumpHeight = 2f;
    public float slideDuration = 1f;

    private CharacterController controller;
    private Animator PlayerAnimator;
    private Vector2 moveInput; // Left/right input
    private bool isSliding = false;
    private float verticalVelocity = 0f;
    private float gravity = -9.81f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        PlayerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Ground check
        bool isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity < 0)
            verticalVelocity = -1f; // small downward force to stick to ground

        // Horizontal movement (left/right)
        Vector3 strafe = transform.right * moveInput.x * strafeSpeed;

        // Forward automatic movement
        Vector3 forward = transform.forward * forwardSpeed;

        // Vertical movement (jump/gravity)
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 vertical = Vector3.up * verticalVelocity;

        // Apply movement
        controller.Move((forward + strafe + vertical) * Time.deltaTime);

        // Animator parameters
        PlayerAnimator.SetBool("isRunning", true); // always running
        PlayerAnimator.SetBool("isSliding", isSliding);

        // Debug
        // Debug.Log("Move Input: " + moveInput);
    }

    // Left/Right movement input
    public void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }

    // Jump input
    public void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            PlayerAnimator.SetTrigger("Jump");
        }
    }

    // Slide input
    public void OnSlide(InputValue value)
    {
        if (value.isPressed && controller.isGrounded && !isSliding)
        {
            StartCoroutine(DoSlide());
        }
    }

    private System.Collections.IEnumerator DoSlide()
    {
        isSliding = true;
        PlayerAnimator.SetBool("isSliding", true);

        // Optional: Lower character collider
        float originalHeight = controller.height;
        controller.height = originalHeight / 2f;

        yield return new WaitForSeconds(slideDuration);

        controller.height = originalHeight;
        isSliding = false;
        PlayerAnimator.SetBool("isSliding", false);
    }
}

