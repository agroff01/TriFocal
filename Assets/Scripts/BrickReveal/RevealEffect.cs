using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealEffect : MonoBehaviour
{
    public static RevealEffect Instance;

    public List<MeshRenderer> revealRenderers;
    public float revealDuration = 2f;
    public float revealStartValue = 1f;
    public float revealEndValue = 0f;

    private List<Material> revealMaterials = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        if (RevealEffect.Instance == null)
        {
            RevealEffect.Instance = this;
        }
        else
        {
            Destroy(this);
        }

        for (int i = 0; i < revealRenderers.Count; i++)
        {
            for (int j = 0; j < revealRenderers[i].materials.Length; j++)
            {
                Material material = revealRenderers[i].materials[j];

                // Check if the material has the _ClipVal property
                if (material.HasProperty("_ClipVal"))
                {
                    material.SetFloat("_ClipVal", revealStartValue);
                    revealMaterials.Add(material);
                }
            }
        }
    }

    public void StartRevealEffect()
    {
        for (int i = 0; i < revealMaterials.Count; i++)
        {
            StartCoroutine(RevealMaterials(revealMaterials[i]));
        }
    }

    public void StartRevealEffectForMaterial(Material material)
    {
        StartCoroutine(RevealMaterials(material));
    }

    IEnumerator RevealMaterials(Material material)
    {
        float elapsedTime = 0f;

        while (elapsedTime < revealDuration)
        {
            float currentClipValue = Mathf.Lerp(revealStartValue, revealEndValue, elapsedTime / revealDuration);
            material.SetFloat("_ClipVal", currentClipValue);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        // Ensure the final value is set
        material.SetFloat("_ClipVal", revealEndValue);
    }
}
