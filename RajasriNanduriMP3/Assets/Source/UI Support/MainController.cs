using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Camera MainCamera = null;
    public TheWorld mModel = null;

    public SliderWithEcho mIntervalSlider;
    public SliderWithEcho mSpeedSlider;
    public SliderWithEcho mLifespanslider;

    /*public MovingSphere movingSphere;
    public GameObject barrierPlane;*/

    // Use this for initialization
    void Start() {
        Debug.Assert(MainCamera != null);
        Debug.Assert(mModel != null);

        mIntervalSlider.SetSliderListener(setSphereInterval);
        mSpeedSlider.SetSliderListener(setSphereSpeed);
        mLifespanslider.SetSliderListener(setSphereLifespan);
        mIntervalSlider.InitSliderRange(0.5f, 4, 1);
        mSpeedSlider.InitSliderRange(0.5f, 15, 15);
        mLifespanslider.InitSliderRange(1, 15, 10);

    }

    // Update is called once per frame
    void Update() {
        LMBUp();

        /*Vector3 barrierCenter = barrierPlane.transform.localPosition;
        Vector3 barrierNormal = -barrierPlane.transform.forward;
        movingSphere.castProjectedPoint();*/
    }

    void ManipulateLineEnds(GameObject gameObject, Vector3 clickPosition) {
        switch(gameObject.name) {
            case "LeftWall":
                mModel.AdjustLineEndPts(mModel.LeftLineEndPt, clickPosition);
                return;
            case "RightWall":
                mModel.AdjustLineEndPts(mModel.RightLineEndPt, clickPosition);
                return;
            case "BackWall":
                mModel.AdjustLineEndPts(mModel.BackEndPt, clickPosition);
                return;
            case "Floor":
                mModel.AdjustLineEndPts(mModel.FloorEndPt, clickPosition);
                return;
        }
    }

}
