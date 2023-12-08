using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class brickDespawn : MonoBehaviour
{
    public ColorManager ColorManager;

    public float minSeconds = 0;
    public float maxSeconds = 100;

    private float distance;

    // Update is called once per frame
    void Update()
    {
        //make sure doesn't start till all lenses are collected
        if(ColorManager.allLensesCollected)
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
