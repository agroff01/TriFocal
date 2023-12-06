using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandFall : MonoBehaviour
{
    public ColorManager ColorManager;
    private Rigidbody rb;
   

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
        float randomTime = Random.Range(10f, 30f);

        //island not falling?
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        
        yield return new WaitForSeconds(randomTime);
    }
}
