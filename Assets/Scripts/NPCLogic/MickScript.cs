using UnityEngine;

public class MickScript : MonoBehaviour
{
    [Header ("Doors")]
    [SerializeField] GameObject drawer;
    [SerializeField] GameObject DepoDoor;
    [SerializeField] private Transform maskParent;
    private GameObject keyObject;
    private bool pickedUp = false;

    private void OnEnable()
    {
            drawer.GetComponent<Animator>().Play("Opening 1");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && InputManager.Instance.Interact() && !pickedUp)
        {
            // Equip Key on player
            EquipKey(gameObject, maskParent);
            pickedUp = true;
            DepoDoor.GetComponent<PlayerDoorInteraction>().GiveKey();
        }
    }

    public void EquipKey(GameObject keyPrefab, Transform maskParent)
    {
        keyObject = keyPrefab;

        // Attach to mask parent
        keyObject.transform.SetParent(maskParent);
        keyObject.transform.localPosition = Vector3.zero;
        keyObject.transform.localRotation = Quaternion.identity;
    }

    public void UnequipKey()
    {
        if (keyObject != null)
        {
            Destroy(keyObject);
            keyObject = null;
        }
    }
    
}
