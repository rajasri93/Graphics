using UnityEngine;
using System.Collections;

public class MovingSphere : MonoBehaviour
{
    public GameObject sphere;
    public Vector3 velocity;
    public float lifetime;
    public float speed;
    private float startTime;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= lifetime)
        {
            Object.Destroy(this.sphere);
        }

        sphere.transform.localPosition = sphere.transform.localPosition + (speed * Time.deltaTime * velocity);
    }
}
