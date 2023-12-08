using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineGrowth : MonoBehaviour
{
    public static VineGrowth Instance;

    public List<MeshRenderer> growVineRenderers;
    public float timeToGrow = 3;
    public float updateRate = .05f;
    [Range(0, 1)]
    public float minGrow = .2f;
    [Range(0, 1)]
    public float maxGrow = .97f;

    private List<Material> growVineMaterials = new List<Material>();
    private bool fullyGrown;


    // Start is called before the first frame update
    void Start()
    {
        if (VineGrowth.Instance == null)
        {
            VineGrowth.Instance = this;
        }
        else
        {
            Destroy(this);
        }

        for (int i = 0; i < growVineRenderers.Count; i++) {
            for (int j = 0; j < growVineRenderers[i].materials.Length; j++) {
                if (growVineRenderers[i].materials[j].HasProperty("_Grow_Level"))
                {
                    growVineRenderers[i].materials[j].SetFloat("_Grow_Level", minGrow);
                    growVineMaterials.Add(growVineRenderers[i].materials[j]);
                }
            }
        }
    }

    public void StartGrowVines(){
        for (int i = 0; i < growVineMaterials.Count; i++) {
            StartCoroutine(GrowVines(growVineMaterials[i]));
        }
    }

    public void StartVineGrowthForMaterial(Material m){
        StartCoroutine(GrowVines(m));
    }   
    

    IEnumerator GrowVines (Material material){
        float growValue = material.GetFloat("_Grow_Level");
        while (growValue < maxGrow)
        {
            growValue += 1 / (timeToGrow / updateRate);
            material.SetFloat("_Grow_Level", growValue);

            yield return new WaitForSeconds(updateRate);
        }
        
    }
}
