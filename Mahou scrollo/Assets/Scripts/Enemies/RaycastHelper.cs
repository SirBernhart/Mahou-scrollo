using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHelper : MonoBehaviour
{
    [SerializeField] private Transform raycastPoint;
    private Vector3 ledgeDetectionDirection;
    private int layerMask = 0;

    private void Start()
    {
        ledgeDetectionDirection = raycastPoint.position - new Vector3(
                                        raycastPoint.position.x,
                                        raycastPoint.position.y + 1,
                                        raycastPoint.position.z);

        layerMask = LayerMask.GetMask(LayerMask.LayerToName(transform.root.gameObject.layer));
        layerMask = ~layerMask;
        Debug.Log(layerMask);
    }

    public GameObject CheckForObjectHits()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, raycastPoint.localPosition, 2f, layerMask);

        if (hitInfo.transform != null)
        {
            return hitInfo.transform.gameObject;
        }
        else
            return null;
    }

    public bool CheckForLedges()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(raycastPoint.position, ledgeDetectionDirection, 2f, layerMask);

        if(hitInfo.transform == null)
        {
            return true;
        }
        else if (hitInfo.transform != null)
        {
            return hitInfo.transform.tag != "Ground";
        }
        else
            return false;
    }
}
