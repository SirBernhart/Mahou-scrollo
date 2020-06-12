using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearSpawnpointDetector : MonoBehaviour
{
    public bool SomethingIsNear { get; private set; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        SomethingIsNear = true;    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SomethingIsNear = false;
    }
}
