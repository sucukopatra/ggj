using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    [SerializeField] GameObject mask;
    [SerializeField] Transform maskParent;

    private bool pickedUp;

    private void OnTriggerStay(Collider other)
    {
        if (pickedUp) return;
        if (!other.CompareTag("Player")) return;

        if (InputManager.Instance.Interact())
        {
            Equip();
        }
    }

    private void Equip()
    {
        pickedUp = true;

        mask.transform.SetParent(maskParent);
        mask.transform.localPosition = Vector3.zero;
        mask.transform.localRotation = Quaternion.identity;

        var col = mask.GetComponent<Collider>();
        if (col) col.enabled = false;
    }
}
