using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MainController : MonoBehaviour {

    public Camera MainCamera = null;
    public TheWorld TheWorld = null;
    public Button ExitButton = null;
    public Toggle DeleteMode = null;
    public SliderWithEchoBehavior xSliderComponent = null;
    public SliderWithEchoBehavior ySliderComponent = null;
    public SliderWithEchoBehavior zSliderComponent = null;
    GameObject mSelected;
    public enum ToggleType
    {
        Translate,
        Rotate,
        Scale
    }
    private ToggleType selectedToggle = ToggleType.Translate;

    // Use this for initialization
    void Start () {
        Debug.Assert(MainCamera != null);
        Debug.Assert(TheWorld != null);
        Debug.Assert(xSliderComponent != null);
        Debug.Assert(ySliderComponent != null);
        Debug.Assert(zSliderComponent != null);
        StartToggles();
        xSliderComponent.InitSliderRange(-10, 10, 0);
        ySliderComponent.InitSliderRange(-10, 10, 0);
        zSliderComponent.InitSliderRange(-10, 10, 0);
        xSliderComponent.SetSliderListener(OnSliderValuesChanged);
        ySliderComponent.SetSliderListener(OnSliderValuesChanged);
        zSliderComponent.SetSliderListener(OnSliderValuesChanged);
        ExitButton.onClick.AddListener(TaskOnClick);
        DeleteMode.onValueChanged.AddListener(SwitchMode);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the exit button!");
        Application.Quit();
    }

    void SwitchMode(bool selected) {
        TheWorld.setDeletionMode(selected);
    }

    // Update is called once per frame
    void Update () {
        CheckMouseClick();
    }

    public void OnSliderValuesChanged(float v)
    {
        Vector3 p;
        p.x = xSliderComponent.GetSliderValue();
        p.y = ySliderComponent.GetSliderValue();
        p.z = zSliderComponent.GetSliderValue();
        switch (selectedToggle)
        {
            case ToggleType.Translate:
                TraslateObject(p);
                break;
            case ToggleType.Rotate:
                RotateObject(p);
                break;
            case ToggleType.Scale:
                ScaleObject(p);
                break;
            default:
                break;
        }
    }

    void TraslateObject(Vector3 p)
    {
        TheWorld.SetSelelectedPosition(p);
    }

    void RotateObject(Vector3 p)
    {
        TheWorld.SetSelelectedRotation(p);
    }
    void ScaleObject(Vector3 p)
    {
        TheWorld.SetSelelectedScale(p);
    }
}
