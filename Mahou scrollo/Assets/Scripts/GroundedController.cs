using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : MonoBehaviour
{
    private bool isGrounded;

    [SerializeField] private float stopBeingGroundedDelay;
    [SerializeField] private string groundTag;
    private Coroutine delayStopBeingGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == groundTag)
            isGrounded = true;

        if(delayStopBeingGrounded != null)
        {
            StopCoroutine(delayStopBeingGrounded);
            delayStopBeingGrounded = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(delayStopBeingGrounded != null)
        {
            StopCoroutine(delayStopBeingGrounded);
        }

        delayStopBeingGrounded = StartCoroutine("DelayStopBeingGrounded");
    }

    private IEnumerator DelayStopBeingGrounded()
    {
        yield return new WaitForSeconds(stopBeingGroundedDelay);
        isGrounded = false;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public void ReactToPlayerJump()
    {
        this.isGrounded = false;

        if(delayStopBeingGrounded != null)
        {
            StopCoroutine(delayStopBeingGrounded);
            delayStopBeingGrounded = null;
        }
    }
}
