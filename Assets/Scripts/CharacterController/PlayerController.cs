using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float jumpHeight = 1.5f;
    [SerializeField] float gravity = -9.81f;

    [SerializeField] float standingHeight = 1.8f;
    [SerializeField] float crouchHeight = 1.0f;
    [SerializeField] float crouchSmooth = 10f;

    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensitivity = 2.5f;

    CharacterController controller;
    Vector3 velocity;
    float xRotation;

    InputManager input;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = InputManager.Instance;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        Vector3 move = HandleMovementInput();
        HandleJump();
        HandleGravity();
        HandleCrouch();
        ApplyMovement(move);
    }

    void HandleMouseLook()
    {
        Vector2 mouse = input.GetMouseDelta() * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation - mouse.y, -89f, 89f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouse.x);
    }

    Vector3 HandleMovementInput()
    {
        Vector2 inputVec = input.GetPlayerMovement();
        Vector3 move = new Vector3(inputVec.x, 0, inputVec.y);
        move = Vector3.ClampMagnitude(move, 1f);
        return transform.TransformDirection(move);
    }

    void HandleJump()
    {
        if (!controller.isGrounded) return;

        if (velocity.y < 0)
            velocity.y = -2f;

        if (input.PlayerJumpedThisFrame())
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    void HandleCrouch()
    {
        float targetHeight = input.IsCrouching() ? crouchHeight : standingHeight;
        controller.height = Mathf.Lerp(
            controller.height,
            targetHeight,
            Time.deltaTime * crouchSmooth
        );
    }

    void ApplyMovement(Vector3 move)
    {
        controller.Move(
            (move * playerSpeed + Vector3.up * velocity.y) * Time.deltaTime
        );
    }
}
