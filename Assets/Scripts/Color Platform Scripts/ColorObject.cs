using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    [SerializeField]
    [Header("Normal Objects always Exist")]
    private bool Normal;
    [Header("This GameObject Will Exist In These Color Spaces")]
    [SerializeField]
    private bool Red;
    [SerializeField]
    private bool Green;
    [SerializeField]
    private bool Blue;

    ColorState currentState;
    BlueColorState blueState = new BlueColorState();
    GreenColorState greenState = new GreenColorState();
    RedColorState redState = new RedColorState();
    NormalColorState normalState = new NormalColorState();

    [HideInInspector]
    public Material greenVineGrowthMat;

    // Start is called before the first frame update
    void Start()
    {
        // Seed the FSM with the starting state
        currentState = normalState;

        gameObject.SetActive(Normal);

        currentState.enterState(this);

        ColorManager.Instance.existingColorObjects.Add(this);
        if (Green) {
            greenVineGrowthMat = Resources.Load("GrowthShader", typeof(Material)) as Material;
            MeshRenderer meshRen = gameObject.GetComponent<MeshRenderer>();
            List<Material> mats = new List<Material>();
            meshRen.GetMaterials(mats);
            greenVineGrowthMat.SetFloat("_Grow_Level", 0f);
            mats.Add(greenVineGrowthMat);
            meshRen.SetMaterials(mats);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void localColorUpdate() {
        gameObject.SetActive(true);
        // if the player is now on normal 
        switch (ColorManager.Instance.currentFilterState) {
            case ColorManager.FilterState.Normal:
                if (Normal) SwapLocalObjectState(normalState);
                else { if (currentState != null) currentState.exitState(this);  currentState = null;  gameObject.SetActive(false); }
                break;

            case ColorManager.FilterState.Blue:
                if (Blue) SwapLocalObjectState(blueState);
                else if (Normal) SwapLocalObjectState(normalState);
                else { if (currentState != null) currentState.exitState(this); currentState = null; gameObject.SetActive(false); }
                break;

            case ColorManager.FilterState.Green:
                if (Green) SwapLocalObjectState(greenState);
                else if (Normal) SwapLocalObjectState(normalState);
                else { if (currentState != null) currentState.exitState(this); currentState = null; gameObject.SetActive(false); }
                break;

            case ColorManager.FilterState.Red:
                if (Red) SwapLocalObjectState(redState);
                else if (Normal) SwapLocalObjectState(normalState);
                else { if (currentState != null) currentState.exitState(this); currentState = null; gameObject.SetActive(false); }
                break;

        }

        //Debug.Log("Manager: " + ColorManager.Instance.currentFilterState + "    Local: " + currentState);
    }

    void SwapLocalObjectState(ColorState s) {
        if (s == currentState || s == null) return;
        if (currentState != null) currentState.exitState(this);
        //Debug.Log("Leaving :" + currentState);
        currentState = s;
        currentState.enterState(this);
    }

    
    
}

// Base class for all our different color states
public abstract class ColorState
{

    public abstract void enterState(ColorObject o);
    public abstract void exitState(ColorObject o);
}








class GreenColorState : ColorState {
    public override void enterState(ColorObject o){


        Debug.Log(o.greenVineGrowthMat);
        VineGrowth.Instance.StartVineGrowthForMaterial(o.greenVineGrowthMat);
    }

    public override void exitState(ColorObject o) {
        Material mat = o.greenVineGrowthMat;
        Debug.Log(mat.ToString());
        mat.SetFloat("_Grow_Level", 0f);
    }
}
class RedColorState : ColorState
{
    public override void enterState(ColorObject o) { }
    public override void exitState(ColorObject o) { }
}
class BlueColorState : ColorState
{
    public override void enterState(ColorObject o) { }
    public override void exitState(ColorObject o) { }
}
class NormalColorState : ColorState
{
    public override void enterState(ColorObject o) { }
    public override void exitState(ColorObject o) { }
}