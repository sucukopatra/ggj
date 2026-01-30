using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputs playerInputs;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoCreate()
    {
        if (Instance != null) return;

        GameObject go = new GameObject("InputManager");
        go.AddComponent<InputManager>();
        DontDestroyOnLoad(go);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs?.Enable();
    }

    private void OnDisable()
    {
        playerInputs?.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerInputs.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerInputs.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame()
    {
        return playerInputs.Player.Jump.triggered;
    }
    public bool IsCrouching()
    {
        return playerInputs.Player.Crouch.IsPressed();
    }

}
