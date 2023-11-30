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

    // Start is called before the first frame update
    void Start()
    {
        // Seed the FSM with the starting state
        currentState = normalState;

        gameObject.SetActive(Normal);

        currentState.enterState(gameObject);

        ColorManager.Instance.inactiveObjects.Add(this);
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
                else gameObject.SetActive(false);
                break;

            case ColorManager.FilterState.Blue:
                if (Blue) SwapLocalObjectState(blueState);
                else if (Normal) SwapLocalObjectState(normalState);
                else gameObject.SetActive(false);
                break;

            case ColorManager.FilterState.Green:
                if (Green) SwapLocalObjectState(greenState);
                else if (Normal) SwapLocalObjectState(normalState);
                else gameObject.SetActive(false);
                break;

            case ColorManager.FilterState.Red:
                if (Red) SwapLocalObjectState(redState);
                else if (Normal) SwapLocalObjectState(normalState);
                else gameObject.SetActive(false);
                break;

        }
    }

    void SwapLocalObjectState(ColorState s) {
        if (s == currentState || s == null) return;
        currentState.exitState(gameObject);
        currentState = s;
        currentState.enterState(gameObject);
    }

    
    
}

// Base class for all our different color states
public abstract class ColorState
{

    public abstract void enterState(GameObject o);
    public abstract void exitState(GameObject o);
}








class GreenColorState : ColorState {
    public override void enterState(GameObject o){

        // Apply A slight green tint to the object to draw the player's eye
        Material newMat = Resources.Load("GreenTint", typeof(Material)) as Material;
        MeshRenderer meshRen = o.GetComponent<MeshRenderer>();
        List<Material> mats = new List<Material>();
        meshRen.GetMaterials(mats);
        mats.Add(newMat);
        meshRen.SetMaterials(mats);
    }
    public override void exitState(GameObject o) {
        MeshRenderer meshRen = o.GetComponent<MeshRenderer>();
        List<Material> mats = new List<Material>();
        meshRen.GetMaterials(mats);
        mats.RemoveAt(1);
        meshRen.SetMaterials(mats);
    }
}
class RedColorState : ColorState
{
    public override void enterState(GameObject o) { }
    public override void exitState(GameObject o) { }
}
class BlueColorState : ColorState
{
    public override void enterState(GameObject o) { }
    public override void exitState(GameObject o) { }
}
class NormalColorState : ColorState
{
    public override void enterState(GameObject o) { }
    public override void exitState(GameObject o) { }
}