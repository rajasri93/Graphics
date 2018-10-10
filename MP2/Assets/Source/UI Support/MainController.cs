using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MainController : MonoBehaviour {

    public Camera MainCamera = null;
    public TheWorld TheWorld = null;
    public Button ExitButton = null;
    public Toggle DeleteMode = null;

    // Use this for initialization
    void Start () {
        Debug.Assert(MainCamera != null);
        Debug.Assert(TheWorld != null);
        ExitButton.onClick.AddListener(TaskOnClick);
        DeleteMode.onValueChanged.AddListener(SwitchMode);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the exit button!");
        Application.Quit();
    }

    void SwitchMode(bool selected) {
        TheWorld.setDeletionMode(selected);
    }

    // Update is called once per frame
    void Update () {
        CheckMouseClick();
    }
}
