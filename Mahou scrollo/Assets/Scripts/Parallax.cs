using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform imageLayer1;
    [SerializeField] private Transform imageLayer2;

    [SerializeField] private float layer1Multiplier;
    [SerializeField] private float layer2Multiplier;

    private Vector3 lastCameraTransform;

    private void Start()
    {
        lastCameraTransform = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position != lastCameraTransform)
        {
            imageLayer1.position -= (lastCameraTransform - transform.position)*layer1Multiplier;
            imageLayer2.position -= (lastCameraTransform - transform.position)*layer2Multiplier;
            lastCameraTransform = transform.position;
        }
    }
}
