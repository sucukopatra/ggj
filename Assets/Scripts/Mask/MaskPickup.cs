using UnityEngine;

public enum MaskType
{
    None,
    Happy,
    Sad,
    Angry
}

public class MaskPickup : MonoBehaviour
{
    [SerializeField] private MaskType maskType;
    [SerializeField] private GameObject mask;
    [SerializeField] private Transform maskParent;

    private bool pickedUp;

    private void OnTriggerStay(Collider other)
    {
        if (pickedUp) return;
        if (!other.CompareTag("Player")) return;
        if (!InputManager.Instance.Interact()) return;

        var playerMask = other.GetComponent<PlayerMask>();
        if (playerMask == null) return;

        playerMask.EquipMask(maskType);
        EquipVisual();
    }

    private void EquipVisual()
    {
        pickedUp = true;

        mask.transform.SetParent(maskParent);
        mask.transform.localPosition = Vector3.zero;
        mask.transform.localRotation = Quaternion.identity;

        var col = mask.GetComponent<Collider>();
        if (col) col.enabled = false;
    }
}
