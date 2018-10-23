using UnityEngine;
using System.Collections;

public partial class MainController : MonoBehaviour
{
    void setSphereInterval(float v)
    {
        mModel.Interval = v;
    }

    void setSphereSpeed(float v)
    {
        mModel.Speed = v;
    }

    void setSphereLifespan(float v)
    {
        mModel.LifeSpan = v;
    }
}
