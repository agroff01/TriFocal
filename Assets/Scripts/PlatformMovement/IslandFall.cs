using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class IslandFall : MonoBehaviour
{
    public ColorManager ColorManager;
    private Rigidbody rb;

    public float minSeconds = 0;
    public float maxSeconds = 100;

    private float distance;

    // Update is called once per frame
    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //make sure doesn't start till all lenses are collected
        if(ColorManager.allLensesCollected)
        {
            StartCoroutine(Falling());
        }
        if(gameObject.transform.position.y < distance - 20)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Falling()
    {
        //space out when islands fall 
        float randomTime = Random.Range(minSeconds, maxSeconds);
        yield return new WaitForSecondsRealtime(randomTime);

        float distance = gameObject.transform.position.y;

        //island is falling
        rb.isKinematic = false;


    }
}
