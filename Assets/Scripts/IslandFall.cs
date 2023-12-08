using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class IslandFall : MonoBehaviour
{
    public ColorManager ColorManager;
    private Rigidbody rb;

    public float minTime = 0;
    public float maxTime = 100000;

    // Update is called once per frame
    void Update()
    {
        //make sure doesn't start till all lenses are collected
        if(ColorManager.allLensesCollected)
        {
            Debug.Log("All lenses gone");
            StartCoroutine(Falling());
        }
    }

    IEnumerator Falling()
    {
        //space out when islands fall 
        float randomTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(randomTime);

        //island is falling
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        
    }
}
