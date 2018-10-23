using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TheWorld : MonoBehaviour {

    public GameObject world;

    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject floor;
    public GameObject BackWall;

    public GameObject LeftLineEndPt;
    public GameObject RightLineEndPt;

    public GameObject AimLine;

    private float timeToCoverPortal = 5;
    private float timeInterval = 2;
    private float time = 0;
    Vector3 portalVelocity = Vector3.zero;
    float portalLength = 0;

    void Start () {
        Debug.Assert(leftWall != null);
        Debug.Assert(rightWall != null);
        Debug.Assert(floor != null);
        Debug.Assert(BackWall != null);
        Debug.Assert(RightLineEndPt != null);
        Debug.Assert(LeftLineEndPt != null);
        Debug.Assert(AimLine != null);

        ComputePortalObjectVelocity();
    }

    private void Update()
    {
        InitiatePortal();
        if (time > timeInterval) {
            time = 0;
        } else {
            time += Time.deltaTime;
        }
        if(Math.Abs(time)<=0.01f) {
            InitiatePortalObjects();
        }
    }

    private void InitiatePortal() {
        Vector3 leftPosition = LeftLineEndPt.transform.localPosition;
        Debug.Log(leftPosition);
        Vector3 rightPosition = RightLineEndPt.transform.localPosition;
        Debug.Log(rightPosition);

        AimLine.GetComponent<Renderer>().material.color = Color.black;

        Quaternion portalDirection = Quaternion.FromToRotation(AimLine.transform.up, portalVelocity);

        AimLine.transform.localScale = new Vector3(0.1f, portalLength-2f, 0.1f);
        AimLine.transform.localRotation = portalDirection * AimLine.transform.localRotation;
        AimLine.transform.localPosition = leftPosition + (rightPosition - leftPosition) * 0.5f;
    }

    private void InitiatePortalObjects() {
        GameObject portalObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        portalObject.GetComponent<Renderer>().material.color = Color.green;
        portalObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        portalObject.transform.localPosition = LeftLineEndPt.transform.localPosition;
        TranslatePortalObjects(portalObject);
    }

    private void TranslatePortalObjects(GameObject portalObject) {
        portalObject.transform.localPosition += (portalLength / timeToCoverPortal) * portalVelocity;
    }

    private void ComputePortalObjectVelocity() {
        Vector3 entryPosition = LeftLineEndPt.transform.localPosition;
        Vector3 exitPosition = RightLineEndPt.transform.localPosition;
        portalVelocity = exitPosition - entryPosition;
        portalLength = portalVelocity.magnitude;
        portalVelocity.Normalize();
    }

}
