using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconScript : MonoBehaviour
{
    public ColorManager ColorManager;
    private Renderer mesh;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<Renderer>();
        mesh.enabled = !mesh.enabled;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ColorManager.allLensesCollected && !flag)
        {
            mesh.enabled = !mesh.enabled;
            flag = true;
        }
    }
}
