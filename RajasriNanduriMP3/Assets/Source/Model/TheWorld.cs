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

    public GameObject BackEndPt;
    public GameObject FloorEndPt;

    public GameObject AimLine;
    public GameObject BigLine;

    public GameObject BarrierPlane;
    public GameObject ActiveSphere;
    public GameObject PlaneNormal;

    //public float NormalLength = 5f; 

    [SerializeField]
    public MovingSphere movingSpherePrefab;

    private float time = 0;
    Vector3 aimLineVelocity = Vector3.zero;
    float aimLineLength = 0;
    Vector3 bigLineVelocity = Vector3.zero;
    float bigLineLength = 0;
    Vector3 planeNormalVelocity = Vector3.zero;
    float planeNormalLength = 5f;

    float kSize = 3f;
    float shadawForDistance = 3f;

    public float Speed { set; get; }
    public float Interval { set; get; }
    public float LifeSpan { set; get; }

    void Start () {
        Debug.Assert(leftWall != null);
        Debug.Assert(rightWall != null);
        Debug.Assert(floor != null);
        Debug.Assert(BackWall != null);
        Debug.Assert(RightLineEndPt != null);
        Debug.Assert(LeftLineEndPt != null);
        Debug.Assert(AimLine != null);
        Debug.Assert(BackEndPt != null);
        Debug.Assert(FloorEndPt != null);
        Debug.Assert(BigLine != null);
        Debug.Assert(BarrierPlane != null);
        Debug.Assert(ActiveSphere != null);
        Debug.Assert(PlaneNormal != null);

        //Turning off Shadow casting for gameobjects
        PlaneNormal.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        //Enabled Shadows only for AimLine and BigLine
        AimLine.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        BigLine.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        ComputePortalObjectVelocity();
        InitiatePortal();
        SetNormal();
    }

    private void Update()
    {
        InitiatePortal();
        if (time > Interval)
        {
            time = 0;
            CreateMovingSpehere();
        }
        else
        {
            time += Time.deltaTime;
        }
        //projectsShadows();
    }

    public void SetNormal() {
        Vector3 barrierCenter = BarrierPlane.transform.localPosition;
        Vector3 barrierNormalDirection = -BarrierPlane.transform.forward;
        Vector3 normalPt = barrierCenter + planeNormalLength * barrierNormalDirection;

        Quaternion normalDirection = Quaternion.FromToRotation(PlaneNormal.transform.up, barrierNormalDirection);

        PlaneNormal.transform.localScale = new Vector3(0.1f, planeNormalLength * 0.5f, 0.1f);
        PlaneNormal.transform.localRotation = normalDirection * PlaneNormal.transform.localRotation;
        PlaneNormal.transform.localPosition = barrierCenter + (normalPt - barrierCenter) * 0.5f;
    }

    private void InitiatePortal() {
        Vector3 leftPosition = LeftLineEndPt.transform.localPosition;
        //Debug.Log(leftPosition);
        Vector3 rightPosition = RightLineEndPt.transform.localPosition;
        //Debug.Log(rightPosition);

        AimLine.GetComponent<Renderer>().material.color = Color.black;

        Quaternion portalDirection = Quaternion.FromToRotation(AimLine.transform.up, aimLineVelocity);

        AimLine.transform.localScale = new Vector3(0.1f, aimLineLength*0.5f, 0.1f);
        AimLine.transform.localRotation = portalDirection * AimLine.transform.localRotation;
        AimLine.transform.localPosition = leftPosition + (rightPosition - leftPosition) * 0.5f;

        Vector3 backPosition = BackEndPt.transform.localPosition;
        Vector3 floorPosition = FloorEndPt.transform.localPosition;

        Quaternion bigLineDirection = Quaternion.FromToRotation(BigLine.transform.up, bigLineVelocity);

        BigLine.transform.localScale = new Vector3(BigLine.transform.localScale.x, bigLineLength*0.5f, BigLine.transform.localScale.z);
        BigLine.transform.localRotation = bigLineDirection * BigLine.transform.localRotation;
        BigLine.transform.localPosition = backPosition + (floorPosition - backPosition) * 0.5f;

    }

    private void CreateMovingSpehere()
    {
        MovingSphere portalObject = GameObject.Instantiate(movingSpherePrefab, LeftLineEndPt.transform.localPosition, new Quaternion(0, 0, 0, 0));
        portalObject.velocity = aimLineVelocity;
        portalObject.speed = Speed;
        portalObject.lifetime = LifeSpan;
    }

    private void ComputePortalObjectVelocity() {
        Vector3 entryPosition = LeftLineEndPt.transform.localPosition;
        Vector3 exitPosition = RightLineEndPt.transform.localPosition;

        Vector3 backPosition = BackEndPt.transform.localPosition;
        Vector3 floorPosition = FloorEndPt.transform.localPosition;

        aimLineVelocity = exitPosition - entryPosition;
        aimLineLength = aimLineVelocity.magnitude;
        aimLineVelocity.Normalize();

        bigLineVelocity = backPosition - floorPosition;
        bigLineLength = bigLineVelocity.magnitude;
        bigLineVelocity.Normalize();

    }

    public void AdjustLineEndPts(GameObject gameObject, Vector3 pos) {
        gameObject.transform.localPosition = pos;
        ComputePortalObjectVelocity();
        InitiatePortal();
    }

    public void getSphereProjection(GameObject gameObject) {
        Vector3 spherePosition = gameObject.transform.localPosition;
        Vector3 n = -BarrierPlane.transform.forward;

        Vector3 planeCenter = BarrierPlane.transform.localPosition;
        float d = Vector3.Dot(n, planeCenter);

        Vector3 projectedPoint = planeCenter + kSize * n;

        Debug.DrawLine(planeCenter, projectedPoint, Color.red);

    }

    public void projectsShadows() {

        Vector3 barrierCenter = BarrierPlane.transform.localPosition;
        Vector3 barrierNormalDirection = -BarrierPlane.transform.forward;
        float d = Vector3.Dot(barrierNormalDirection, barrierCenter);
        //Vector3 normalPt = barrierCenter + planeNormalLength * barrierNormalDirection;

        Vector3 spherePosition = Vector3.zero;
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "MovingSphere(Clone)")
            {
                Debug.Log("gameObject::::::::: " + gameObject);
                spherePosition = gameObj.transform.localPosition;
                Debug.Log(spherePosition);
                GameObject shadowSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                shadowSphere.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
                shadowSphere.GetComponent<Renderer>().material.color = Color.red;

                Quaternion shadowDirection = Quaternion.FromToRotation(shadowSphere.transform.up, barrierNormalDirection);
                PlaneNormal.transform.localRotation = shadowDirection * shadowSphere.transform.localRotation;

                float h = Vector3.Dot(spherePosition, barrierNormalDirection) - d;

                shadowSphere.transform.localPosition = spherePosition - (barrierNormalDirection * h);

                float size = h / 2;

                Debug.Log("Gameobject:: " + gameObj);
                Debug.Log("spherePosition :: " + spherePosition);
                Debug.Log("shadowSphere Position:: " + shadowSphere.transform.localPosition);

            }
        }
    }

    /*private void setBarrierPlaneNormal()
    {
        Vector3 barrierCenter = BarrierPlane.transform.localPosition;
        /*Vector3 barrierNormalDirection = -BarrierPlane.transform.forward;
        barrierNormalDirection.Normalize();
        Vector3 normalPt = barrierCenter + planeNormalLength * barrierNormalDirection;

        Vector3 distance = normalPt - barrierCenter;
        Debug.Log(distance.magnitude);

        Debug.Log("barrierCenter :: " + barrierCenter);
        Debug.Log("barrierNormalDirection :: " + barrierNormalDirection);
        Debug.Log("normalPt :: " + normalPt);

        Quaternion planeNormalDirection = Quaternion.FromToRotation(PlaneNormal.transform.up, barrierNormalDirection);*/

    /* Vector3 n = -BarrierPlane.transform.forward;
     Quaternion q = BarrierPlane.transform.localRotation;
     Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);
     n = -m.GetColumn(2);

     float d = Vector3.Dot(n, barrierCenter);
     Vector3 normalPt = barrierCenter + planeNormalLength * n;

     planeNormalVelocity = normalPt - barrierCenter;
     Debug.Log(planeNormalVelocity.magnitude);
     planeNormalVelocity.Normalize();

     PlaneNormal.transform.localScale = new Vector3(PlaneNormal.transform.localScale.x / BarrierPlane.transform.localScale.x, planeNormalLength / BarrierPlane.transform.localScale.y, PlaneNormal.transform.localScale.z / BarrierPlane.transform.localScale.z);
     PlaneNormal.transform.localRotation = q * PlaneNormal.transform.rotation;
     PlaneNormal.transform.position = barrierCenter + (normalPt - barrierCenter) * 0.5f;
 }*/

}
