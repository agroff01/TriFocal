using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radial_Buttons : MonoBehaviour
{
    public ColorManager CM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RedButton()
    {
        CM.setColorToRed();
    }
    public void BlueButton()
    {
        CM.setColorToBlue();
    }
    public void GreenButton()
    {
        CM.setColorToGreen();
    }
    public void DefaultButton()
    {
        CM.setColorToNormal();
    }
}
