using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Camera MainCamera = null;
    public XfromControl mXform = null;
    public DropDownCreate mCreateMenu = null;
    public TheWorld mModel = null;
    public SliderWithEcho mIntervalSlider;
    public SliderWithEcho mSpeedSlider;
    public SliderWithEcho mLifespanslider;

    // Use this for initialization
    void Start() {
        Debug.Assert(MainCamera != null);
        Debug.Assert(mXform != null);
        Debug.Assert(mModel != null);
        Debug.Assert(mCreateMenu != null);
        Debug.Assert(mIntervalSlider != null);
        Debug.Assert(mSpeedSlider != null);
        Debug.Assert(mLifespanslider != null);
        mIntervalSlider.SetSliderListener(setSphereInterval);
        mSpeedSlider.SetSliderListener(setSphereSpeed);
        mLifespanslider.SetSliderListener(setSphereLifespan);
        mIntervalSlider.InitSliderRange(0.5f, 4, 1);
        mSpeedSlider.InitSliderRange(0.5f, 15, 15);
        mLifespanslider.InitSliderRange(1, 15, 10);
    }

    // Update is called once per frame
    void Update() {
        LMBSelect();
    }

}
