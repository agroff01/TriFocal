using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAcend : MonoBehaviour
{
    public float BreakingFactor = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player"){
            other.attachedRigidbody.velocity = transform.forward + other.attachedRigidbody.velocity;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            other.attachedRigidbody.velocity = other.attachedRigidbody.velocity / BreakingFactor;
        }
    }
}
