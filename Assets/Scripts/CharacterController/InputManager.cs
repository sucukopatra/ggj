using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerInputs playerInputs;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoCreate()
    {
        if (Instance != null) return;

        var go = new GameObject("InputManager");
        DontDestroyOnLoad(go);
        go.AddComponent<InputManager>();
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
        playerInputs.Enable();
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
    public bool Interact()
    {
        return playerInputs.Player.Interact.triggered;
    }

}
