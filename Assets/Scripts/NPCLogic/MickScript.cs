using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MickScript : MonoBehaviour
{
    [Header ("Doors")]
    [SerializeField] Transform DepoDoor;
    [SerializeField] List<GameObject> WrongDoors;
    public float duration = 0.5f;
    private void Start()
    {
        OpenDoor(DepoDoor);
    }

    public void OpenDoor(Transform Door)
    {
        StartCoroutine(RotateDoor(Door));
    }

    private IEnumerator RotateDoor(Transform Door)
    {
        Quaternion startRot = Door.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(90f, 0f, 0f);

        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime / duration;
            Door.rotation = Quaternion.Slerp(startRot, endRot, time);
            yield return null;
        }

        Door.rotation = endRot;
    }
}
