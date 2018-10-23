using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TheWorld : MonoBehaviour {

    public GameObject world;

    public GameObject room;
    public GameObject leftWall;
    public GameObject rightWall;

    public GameObject entry;
    public GameObject exit;

    public GameObject portal;

    private float timeToCoverPortal = 5;
    private float timeInterval = 2;
    private float time = 0;
    Vector3 portalVelocity = Vector3.zero;
    float portalLength = 0;

    void Start () {
        Debug.Assert(room != null);
        Debug.Assert(leftWall != null);
        Debug.Assert(rightWall != null);
        Debug.Assert(entry != null);
        Debug.Assert(exit != null);

        ComputePortalObjectVelocity();
        InitiatePortal();
    }

    private void Update()
    {
        if(time > timeInterval) {
            time = 0;
        } else {
            time += Time.deltaTime;
        }
        if(Math.Abs(time)<=0.01f) {
            InitiatePortalObjects();
        }
    }

    private void InitiatePortal() {
        Vector3 entryPosition = entry.transform.localPosition;
        Debug.Log(entryPosition);
        Vector3 exitPosition = exit.transform.localPosition;
        Debug.Log(exitPosition);

        portal.GetComponent<Renderer>().material.color = Color.black;

        Quaternion portalDirection = Quaternion.FromToRotation(portal.transform.up, portalVelocity);

        portal.transform.localScale = new Vector3(0.1f, portalLength-2f, 0.1f);
        portal.transform.localRotation = portalDirection* portal.transform.localRotation;
        portal.transform.localPosition = entryPosition + (exitPosition - entryPosition) * 0.5f;
    }

    private void InitiatePortalObjects() {
        GameObject portalObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        portalObject.GetComponent<Renderer>().material.color = Color.green;
        portalObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        portalObject.transform.localPosition = entry.transform.localPosition;
        TranslatePortalObjects(portalObject);
    }

    private void TranslatePortalObjects(GameObject portalObject) {
        portalObject.transform.localPosition += (portalLength / timeToCoverPortal) * portalVelocity;
    }

    private void ComputePortalObjectVelocity() {
        Vector3 entryPosition = entry.transform.localPosition;
        Vector3 exitPosition = exit.transform.localPosition;
        portalVelocity = exitPosition - entryPosition;
        portalLength = portalVelocity.magnitude;
        portalVelocity.Normalize();
    }

}
