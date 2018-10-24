using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using UnityEngine.EventSystems;

public partial class MainController : MonoBehaviour {

    // Mouse click selection 
    void LMBUp()
    {
        //Debug.Log(Input.GetMouseButtonUp(0));
        if(!EventSystem.current.IsPointerOverGameObject()) {
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, 1);
                if (hit)
                {
                    ManipulateLineEnds(hitInfo.transform.gameObject, hitInfo.point);
                    Debug.Log("gameobject :: " + hitInfo.transform.gameObject);
                }
            }
        }
    }
}
