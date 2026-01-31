using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravityValue = -9.81f;

    [Header("Crouch")]
    [SerializeField] private float standingHeight = 1.8f;
    [SerializeField] private float crouchHeight = 1.0f;
     private float crouchSmooth = 10f;

    [Header("Mouse Look")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 2.5f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private float xRotation = 0f;

    private InputManager inputManager;

    void Start()
    {
        PauseMenu.IsPaused = false;
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (PauseMenu.IsPaused)
            return;
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < -2f)
            playerVelocity.y = -2f;

        // ===== MOUSE LOOK =====
        Vector2 mouseDelta = inputManager.GetMouseDelta() * mouseSensitivity;

        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseDelta.x);

        // ===== MOVEMENT =====
        Vector2 input = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        // Move relative to where the player is looking
        move = transform.TransformDirection(move);

        // ===== JUMP =====
        if (groundedPlayer && inputManager.PlayerJumpedThisFrame())
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        // ===== GRAVITY =====
        playerVelocity.y += gravityValue * Time.deltaTime;

        // ===== CROUCH ======
        
         bool crouched = inputManager.IsCrouching();

         float targetHeight = crouched ? crouchHeight : standingHeight;

         controller.height = Mathf.Lerp(
             controller.height,
             targetHeight,
             Time.deltaTime * crouchSmooth
         );
             Vector3 finalMove = move * playerSpeed + Vector3.up * playerVelocity.y;
             controller.Move(finalMove * Time.deltaTime);
         }
}
