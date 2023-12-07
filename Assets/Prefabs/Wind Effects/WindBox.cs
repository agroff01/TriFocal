using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBox : MonoBehaviour
{
    public float windForce;
    public Transform windBase;
    /*public void OnTriggerStay(Collider collid)
    {
        if (collid.attachedRigidbody)
        {
            collid.attachedRigidbody.AddForce(windBase.up * windForce);
            Debug.DrawRay(playerPos.position, windBase.up, Color.red);
            Debug.Log("rayDisplay");
        }
    }*/

    public void OnTriggerEnter(Collider collid)
    {
        if (collid.attachedRigidbody && collid.CompareTag("Player"))
        {
            
            collid.attachedRigidbody.AddForce(windBase.up * windForce);
        }
    }
}
   

