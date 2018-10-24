using UnityEngine;
using System.Collections;

public class MovingSphere : MonoBehaviour
{
    public GameObject sphere;
    public Vector3 velocity;
    public float lifetime;
    public float speed;
    public GameObject barrierPlane;
    private float startTime;
    public delegate void MovingSpherePositionChangeCallbackDelegate(Vector3 position);      // defined a new data type
    private MovingSpherePositionChangeCallbackDelegate mPositionCallBack = null;

    public delegate void MovingSphereOnDeleteCallbackDelegate();      // defined a new data type
    private MovingSphereOnDeleteCallbackDelegate mOnDeleteCallBack = null;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
    }

    void SetPositionChangeCallback(MovingSpherePositionChangeCallbackDelegate movingDelegate)
    {
        mPositionCallBack = movingDelegate;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= lifetime)
        {
            Object.Destroy(this.sphere);
        }

        sphere.transform.localPosition = sphere.transform.localPosition + (speed * Time.deltaTime * velocity);
        if(mPositionCallBack != null)
        {
            mPositionCallBack(sphere.transform.localPosition);
        }

        
    }

    public void castProjectedPoint() {
        GameObject shadowSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    }

}
