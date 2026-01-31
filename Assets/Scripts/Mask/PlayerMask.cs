using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    public MaskType CurrentMaskType { get; private set; } = MaskType.None;
    private GameObject currentMaskObject;

    public void EquipMask(MaskType maskType, GameObject maskPrefab, Transform maskParent)
    {
        UnequipMask();

        CurrentMaskType = maskType;
        currentMaskObject = maskPrefab;

        // Attach to mask parent
        currentMaskObject.transform.SetParent(maskParent);
        currentMaskObject.transform.localPosition = Vector3.zero;
        currentMaskObject.transform.localRotation = Quaternion.identity;

        // Disable collider
        var col = currentMaskObject.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    public void UnequipMask()
    {
        if (currentMaskObject != null)
        {
            Destroy(currentMaskObject);
            currentMaskObject = null;
        }

        CurrentMaskType = MaskType.None;
    }
}
