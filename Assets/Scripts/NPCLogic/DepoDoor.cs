using UnityEngine;
public class PlayerDoorInteraction : MonoBehaviour
{
    private bool hasKey;

    public void GiveKey()
    {
        hasKey = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!InputManager.Instance.Interact())
            return;

        if (other.CompareTag("DepoDoor") && hasKey)
        {
            gameObject.GetComponent<Animator>().Play("Opening");
            hasKey = false;
        }
    }
}
