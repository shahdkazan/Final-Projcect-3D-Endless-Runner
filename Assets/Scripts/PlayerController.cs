using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 5f;
    public float laneDistance = 3f; // distance between left, center, right lanes
    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right

    [Header("Jump")]
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    private bool isGrounded;

    private Rigidbody rb;
    private Animator anim;

    // Input variables
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool slidePressed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            jumpPressed = true;
    }

    public void OnSlide(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            slidePressed = true;
    }

    private void Update()
    {
        HandleGroundCheck();
        HandleForwardMovement();
        HandleLaneMovement();
        HandleJump();
        HandleSlide();
    }

    void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        anim.SetBool("isGrounded", isGrounded);
    }

    void HandleForwardMovement()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        anim.SetBool("isRunning", true);
    }

    void HandleLaneMovement()
    {
        if (moveInput.x > 0.5f && currentLane < 2)
            currentLane++;

        if (moveInput.x < -0.5f && currentLane > 0)
            currentLane--;

        // Move to lane smoothly
        Vector3 targetPos = transform.position;
        targetPos.x = (currentLane - 1) * laneDistance;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
    }

    void HandleJump()
    {
        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
        jumpPressed = false;
    }

    void HandleSlide()
    {
        if (slidePressed && isGrounded)
        {
            anim.SetTrigger("Slide");
        }
        slidePressed = false;
    }
}
