using UnityEngine;

public enum MaskType { None, Happy, Sad, Angry }

public class MaskPickup : MonoBehaviour
{
    [SerializeField] private MaskType maskType;
    [SerializeField] private Transform maskParent;

    private bool pickedUp;

    private void OnTriggerStay(Collider other)
    {
        if (pickedUp) return;
        if (!other.CompareTag("Player")) return;
        if (!InputManager.Instance.Interact()) return;

        var playerMask = other.GetComponent<PlayerMask>();
        if (playerMask == null) return;

        // Equip mask on player
        playerMask.EquipMask(maskType, gameObject, maskParent);
        pickedUp = true;
    }
}
