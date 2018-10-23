using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownCreate : MonoBehaviour {

    public Dropdown mCreateMenu = null;
    public TheWorld TheWorld = null;

    // Use this for initialization
    void Start () {
        Debug.Assert(mCreateMenu!=null);
        Debug.Assert(TheWorld != null);
        mCreateMenu.onValueChanged.AddListener(UserSelection);
    }

    String[] kLoadType = { "", "MyCube", "MySphere", "MyCylinder" };
    void UserSelection(int index)
    {
        if (index == 0)
            return;

        mCreateMenu.value = 0;
        TheWorld.ProcessUserSelection(kLoadType[index]);
    }
}
