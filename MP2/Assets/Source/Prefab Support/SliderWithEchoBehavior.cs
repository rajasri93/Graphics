using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderWithEchoBehavior : MonoBehaviour {

    public Slider xSlider = null;
    public Text xEcho = null;
    public Text xLabel = null;
    public Slider ySlider = null;
    public Text yEcho = null;
    public Text yLabel = null;
    public Slider zSlider = null;
    public Text zEcho = null;
    public Text zLabel = null;

    //public delegate void SliderDelegates(float change, String axis);
    //private SliderDelegates sliderDelegates = null;

    public delegate void SliderCallbackDelegate(float v);
    //public delegate void SliderCallbackDelegate(float v, String s);
    private SliderCallbackDelegate sliderCallBack = null;

    // Use this for initialization
    void Start () {
        Debug.Log("Asserting..");
        Debug.Assert(xSlider != null);
        Debug.Assert(ySlider != null);
        Debug.Assert(zSlider != null);
        xSlider.onValueChanged.AddListener(SliderValueChange);
        //xSlider.onValueChanged.AddListener(SliderValueChanged);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    String[] axes = { "x", "y", "z" };
    //void SliderValueChange(float v, String axis)
    public void SliderValueChange(float v){
        xEcho.text = v.ToString("0.0000");
        if (sliderCallBack != null)
            sliderCallBack(v);
    }

    public void SliderValueChanged(float change, String axis) {

    }

    public float GetSliderValue() { return xSlider.value; }
    public void SetSliderLabel(string l) { xLabel.text = l; }
    public void SetSliderValue(float v) { xSlider.value = v; SliderValueChange(v); }


    public void SetSliderWithAxis(string s, float v) {
        switch(s){
            case "x":
                xSlider.value = v;
                SliderValueChanged(v, s);
                break;
            case "y":
                ySlider.value = v;
                SliderValueChanged(v, s);
                break;
            case "z":
                zSlider.value = v;
                SliderValueChanged(v, s);
                break;
            default:
                break;
        }
    }

    public void InitSliderRange(float min, float max, float v)
    {
        xSlider.minValue = min;
        xSlider.maxValue = max;
        SetSliderValue(v);
    }

}
