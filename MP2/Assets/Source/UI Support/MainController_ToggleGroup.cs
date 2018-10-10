using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public partial class MainController : MonoBehaviour
{

    public ToggleGroup toggleGroup = null;
    public Toggle translateToggle = null;
    public Toggle rotateToggle = null;
    public Toggle scaleToggle = null;


    // Use this for initialization
    void StartToggles()
    {
        Debug.Assert(toggleGroup != null);
        Debug.Assert(translateToggle != null);
        Debug.Assert(rotateToggle != null);
        Debug.Assert(scaleToggle != null);
        translateToggle.onValueChanged.AddListener(onTranslateToggle);
        rotateToggle.onValueChanged.AddListener(onRotateToggle);
        scaleToggle.onValueChanged.AddListener(onScaleToggle);
        // toggleGroup.to
    }

    void onTranslateToggle(Boolean isSelected)
    {
        if (isSelected)
        {
            this.selectedToggle = ToggleType.Translate;
            UpdateSliderValues();
        }
    }

    void onRotateToggle(Boolean isSelected)
    {
        if (isSelected)
        {
            selectedToggle = ToggleType.Rotate;
            UpdateSliderValues();
        }
    }


    void onScaleToggle(Boolean isSelected)
    {
        if (isSelected)
        {
            selectedToggle = ToggleType.Scale;
            UpdateSliderValues();
        }
    }

    void UpdateSliderValues()
    {
        switch (selectedToggle)
        {
            case ToggleType.Translate:
                if (mSelected)
                    setSliderValues(mSelected.transform.position, new Vector2(-10, 10));
                else
                    setSliderValues(new Vector3(0, 0, 0), new Vector2(-10, 10));
                break;
            case ToggleType.Rotate:
                if (mSelected)
                    setSliderValues(mSelected.transform.localRotation.eulerAngles, new Vector2(-180, 180));
                else
                    setSliderValues(new Vector3(0, 0, 0), new Vector2(-180, 180));
                break;
            case ToggleType.Scale:
                if (mSelected)
                    setSliderValues(mSelected.transform.localScale, new Vector2(1, 5));
                else
                    setSliderValues(new Vector3(1, 1, 1), new Vector2(1, 5));
                break;
            default:
                break;
        }
    }

    void setSliderValues(Vector3 p, Vector2 range)
    {
        this.xSliderComponent.InitSliderRange(range.x, range.y, p.x);
        this.ySliderComponent.InitSliderRange(range.x, range.y, p.y);
        this.zSliderComponent.InitSliderRange(range.x, range.y, p.z);
    }
}
