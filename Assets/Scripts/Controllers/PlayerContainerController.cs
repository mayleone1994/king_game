using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainerController : MonoBehaviour
{
    public int GetContainersCount => transform.childCount;

    public Transform[] GetContainers()
    {
        return transform.GetComponentsInChildren<Transform>();
    }

    public Transform GetContainerByIndex(int index)
    {
        return transform.GetChild(index);
    }
}
