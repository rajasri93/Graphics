using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MainController : MonoBehaviour {
    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log(EventSystem.current);
                Debug.Log("POINTER OVER GAMEOBJECT!");
            }

            RaycastHit hitInfo = new RaycastHit();

            bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, 1);

            if (hit)
            {
                Debug.Log("HIT!!");
                TheWorld.SelectObjectAt(hitInfo.transform.gameObject, hitInfo.point);
            }
            else
            {
                Debug.Log("No hit");
            }
            //xSlider.SetSliderValue(TheWorld.GetSelectedRadius());
        }
    }
}
