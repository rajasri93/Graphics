using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class DropDownCreate : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Dropdown mCreateMenu = null;
    public TheWorld TheWorld = null;
    
    // Use this for initialization
    void Start() {
        Debug.Assert(mCreateMenu != null);
        Debug.Assert(TheWorld != null);
    }

    PrimitiveType[] kLoadType = {
        PrimitiveType.Cube,     // this is used as menu label, not used
        PrimitiveType.Cube,
        PrimitiveType.Sphere,
        PrimitiveType.Cylinder };
        
}
