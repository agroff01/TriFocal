using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBox : MonoBehaviour
{
    public float windForce;
    public Transform windBase;
    private Transform Player;

    public void OnTriggerStay(Collider collid)
    {
        if (collid.attachedRigidbody && collid.tag == "Player")
        {
            collid.attachedRigidbody.velocity = windBase.up * windForce;
        }
    }
    public void OnTriggerExit(Collider collid)
    {
        if (collid.attachedRigidbody)
        {
            collid.attachedRigidbody.velocity = windBase.up * windForce * 0.25f;
        }
    }
    /*public void OnTriggerEnter(Collider collid)
    {
        if (collid.attachedRigidbody && collid.CompareTag("Player"))
        {
            
            collid.attachedRigidbody.AddForce(windBase.up * windForce * 2);
        }
    }*/
}
   

