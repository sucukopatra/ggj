using UnityEngine;

public class ApplyImpulseLogic : MonoBehaviour
{
    [Header("Object With Force")]
    [SerializeField] GameObject objectWithForce;

    [Header("Impulse Settings")]
    [SerializeField] private Vector3 impulse = new Vector3(0, 5f, 0);

    void OnEnable()
    {
        objectWithForce.GetComponent<Rigidbody>().AddForce(impulse, ForceMode.Impulse);
    }
}
