using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    public MaskType CurrentMask { get; private set; } = MaskType.None;

    public void EquipMask(MaskType newMask)
    {
        CurrentMask = newMask;
    }
}
