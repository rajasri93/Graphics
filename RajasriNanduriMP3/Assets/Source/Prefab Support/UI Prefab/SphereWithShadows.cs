using UnityEngine;
using System.Collections;

public class SphereWithShadows : MonoBehaviour
{
    public GameObject parent;
    public GameObject sphere;
    public GameObject barrierShadowSphere;
    public GameObject cylinderShadowSphere;
    public Vector3 velocity;
    public float lifetime;
    public float speed;
    public GameObject barrierPlane;
    private float startTime;
    public TheWorld World { get; set; }

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        barrierShadowSphere.SetActive(false);
        cylinderShadowSphere.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        this.sphere.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= lifetime)
        {
            Object.Destroy(this.parent);
        }

        sphere.transform.position = sphere.transform.position + (speed * Time.deltaTime * velocity);
        Vector3 barrierShadowPosition = GetProjectionOnBarrier();
        if( this.IsInActiveRegion(barrierShadowPosition, sphere.transform.position) )
        {
            this.barrierShadowSphere.transform.position = barrierShadowPosition;
            this.barrierShadowSphere.transform.rotation = World.BarrierPlane.transform.localRotation;
            this.barrierShadowSphere.SetActive(true);
        }
        else
        {
            this.barrierShadowSphere.SetActive(false);
        }
        GetActualShadowOnCylinder(this.sphere.transform.position, GetProjectionOnCylinder());

    }

    public void castProjectedPoint() {
        GameObject shadowSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    }

    private Vector3 GetProjectionOnBarrier()
    {
        Vector3 n = -World.BarrierPlane.transform.forward;
        n = n.normalized;
        Vector3 center = World.BarrierPlane.transform.localPosition;

        float d = Vector3.Dot(n, center);

        float h = Vector3.Dot(this.sphere.transform.position, n) - d;
        var position = this.sphere.transform.position - (n * (h - 0.1f));
        return position;
    }

    private bool IsInActiveRegion(Vector3 projection, Vector3 actualPoint)
    {
        Vector3 center = World.BarrierPlane.transform.localPosition;
        Vector3 n = -World.BarrierPlane.transform.forward;
        float distance = Vector3.Distance(projection, center);
        if (distance > 6)
            return false;
        if (Vector3.Dot(n, actualPoint - center) < 0)
            return false;
        return true;

    }

    private Vector3 GetProjectionOnCylinder()
    {
        Vector3 n = -World.BigLine.transform.forward;
        n = n.normalized;
        Vector3 center = World.BigLine.transform.localPosition;

        float d = Vector3.Dot(n, center);

        float h = Vector3.Dot(this.sphere.transform.position, n) - d;
        var position = this.sphere.transform.position - (n * (h - 0.1f));
        return position;
    }

    private void GetActualShadowOnCylinder(Vector3 actualpoint, Vector3 baseProjection)
    {
        Vector3 radialVector = World.BackEndPt.transform.localPosition - World.FloorEndPt.transform.localPosition;
        radialVector.Normalize();

        float d = Vector3.Dot(radialVector, baseProjection);
        float h = Vector3.Dot(World.BackEndPt.transform.localPosition, radialVector) - d;
        var center = World.BackEndPt.transform.localPosition - (radialVector * (h));

        var distance = Vector3.Distance(center, baseProjection);
        if(distance < World.BigLine.transform.localScale.x * 0.5f)
        {
            var pythSquare = (World.BigLine.transform.localScale.x * 0.5f * World.BigLine.transform.localScale.x * 0.5f)
            - (distance * distance);
            var pyth = Mathf.Sqrt(pythSquare);

            var direction = actualpoint - baseProjection;
            direction.Normalize();
            this.cylinderShadowSphere.transform.position = baseProjection + (pyth * direction);
            this.cylinderShadowSphere.SetActive(true);
        }
        else
        {
            this.cylinderShadowSphere.SetActive(false);
        }


    }

}
