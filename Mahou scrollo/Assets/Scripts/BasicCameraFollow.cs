using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if(objectToFollow != null)
        {
            transform.position = objectToFollow.position + offset;
        }
    }
}
